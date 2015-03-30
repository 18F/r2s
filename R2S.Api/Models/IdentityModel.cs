namespace R2S.Api.Models
{
    using System;
    using System.Linq;

    /// <summary>
    ///  Encapsulates data related to a client/user identity.
    /// </summary>
    public class IdentityModel
    {
        /// <summary>
        /// Gets or sets the client certificate.
        /// </summary>
        /// <value>The client certificate.</value>
        public CertificateModel ClientCertificate { get; set; }
    }
}