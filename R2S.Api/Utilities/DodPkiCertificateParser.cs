namespace R2S.Api.Utilities
{
    using System;
    using System.Security;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Web;

    /// <summary>
    /// Parses (extracts) an EDIPI (electronic data interchange personal identifier) from
    ///   a valid DoD EMAIL PKI Certificate.
    /// </summary>
    /// <remarks>
    /// Implementation attempts first to located client certificate information via the
    ///   HTTP Headers where the information is commonly found when a web server
    ///   is being accessed behind a reverse proxy.  If the certificate information
    ///   is not found within the HTTP Headers, than the parsers checks for a physical
    ///   ClientCertificate on the HttpRequest directly.
    /// </remarks>
    /// <example>
    /// <code>
    /// string currentUsersEdipi = 
    ///             DodPkiCertificateParser.GetEdipiFromContext(HttpContext.Current)
    ///    </code>
    /// </example>
    public class DodPkiCertificateParser
    {
        /// <summary>
        /// Holds an instance of the electronic data interchange personal identifier that has been extracted.
        /// </summary>
        private string edipi;

        /// <summary>
        /// Certificate subject.
        /// </summary>
        private string subject;

        /// <summary>
        /// Initializes a new instance of the <see cref="DodPkiCertificateParser"/> class.
        /// </summary>
        protected DodPkiCertificateParser()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DodPkiCertificateParser"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        protected DodPkiCertificateParser(HttpContext context)
        {
            this.ValidateDoDCertificate(context.Request);
            this.ExtractEdipi(context.Request);
        }

        /// <summary>
        /// Gets the electronic data interchange personal identifier from a HttpContext.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The electronic data interchange personal identifier for the client associated with the request.
        /// </returns>
        public static string GetEdipiFromContext(HttpContext context)
        {
            var parser = new DodPkiCertificateParser(context);
            return parser.edipi;
        }

        /// <summary>
        /// Gets the subject from context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static string GetSubjectFromContext(HttpContext context)
        {
            var parser = new DodPkiCertificateParser(context);
            return parser.subject;
        }

        /// <summary>
        /// Extracts the electronic data interchange personal identifier from the <see cref="System.Web.HttpRequest"/>.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        private void ExtractEdipi(HttpRequest request)
        {
            try
            {
                if (request.Headers["Proxy-Cert-Issuer"] != null && request.Headers["Proxy-Cert-Subject"] != null)
                {
                    this.ExtractEdipiFromHttpHeaders(request);
                }
                else
                {
                    this.ExtractEdipiFromCertificate(request);
                }
            }
            catch (Exception)
            {
                throw new SecurityException(
                    "Unable to parse your DoD certificate.  Please ensure that your DoD CAC is inserted, close your browser, and try again.");
            }
        }

        /// <summary>
        /// Extracts the electronic data interchange personal identifier from the <see cref="p:request"/>.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        private void ExtractEdipiFromCertificate(HttpRequest request)
        {
            this.subject = request.ClientCertificate["SUBJECTCN"];

            if (request.ClientCertificate.Issuer.ToUpper().Contains("EMAIL"))
            {
                var x509Cert = new X509Certificate2(request.ClientCertificate.Certificate);
                var certExtensions = x509Cert.Extensions;
                var emailExtension = certExtensions["Subject Alternative Name"];
                var asndata = new AsnEncodedData(emailExtension.Oid, emailExtension.RawData);
                var subjectAlternativeName = asndata.Format(false);

                foreach (var s in subjectAlternativeName.Split(','))
                {
                    if (s.Contains("="))
                    {
                        var keyValue = s.Split('=');
                        var key = keyValue[0];
                        if (key.ToUpper().Contains("PRINCIPAL NAME"))
                        {
                            this.edipi = keyValue[1].Split('@')[0];
                        }
                    }
                }
            }
            else
            {
                this.edipi = this.ExtractEdipiFromCertificateSubject(this.subject);
            }
        }

        /// <summary>
        /// Extracts the electronic data interchange personal identifier from certificate subject.
        /// </summary>
        /// <param name="subject">
        /// The subject.
        /// </param>
        /// <returns>
        /// The electronic data interchange personal identifier.
        /// </returns>
        private string ExtractEdipiFromCertificateSubject(string subject)
        {
            var subjectParts = subject.Split('.');
            return subjectParts[subjectParts.Length - 1];
        }

        /// <summary>
        /// Extracts the electronic data interchange personal identifier from HTTP headers within the <see cref="p:request"/>.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        private void ExtractEdipiFromHttpHeaders(HttpRequest request)
        {
            this.subject = request.Headers["Proxy-Cert-Subject"];
            this.edipi = this.ExtractEdipiFromCertificateSubject(this.subject);
        }

        /// <summary>
        /// Determines whether the current <see cref="System.Web.HttpRequest"/> contains
        ///   a physical certificate.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// True if the request contains a valid client certificate; otherwise, False.
        /// </returns>
        private bool IsValidCertificate(HttpRequest request)
        {
            return request.ClientCertificate.IsPresent
                   && request.ClientCertificate.IsValid;
        }

        /// <summary>
        /// Determines whether the current <see cref="System.Web.HttpRequest"/> contains
        ///   HTTP Headers that include the client certificate information.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// True if the request contains HTTP Headers with certification information; otherwise, False.
        /// </returns>
        private bool IsValidProxyHeaders(HttpRequest request)
        {
            if (request.Headers["Proxy-Cert-Subject"] != null
                && request.Headers["Proxy-Cert-Issuer"].ToUpper().Contains("EMAIL"))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Validates the presence of either the appropriate HTTP Headers
        ///   or a physical certificate attached to the HttpContext.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        private void ValidateDoDCertificate(HttpRequest request)
        {
            // check validity of proxy-provided certificate information
            if (!this.IsValidProxyHeaders(request))
            {
                // check validity of physical certificate on the request object
                if (!this.IsValidCertificate(request))
                {
                    throw new SecurityException(
                        "Unable to validate your DoD certificate.  Please ensure that your DoD CAC is inserted, close your browser, and try again.");
                }
            }
        }
    }
}