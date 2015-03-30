namespace R2S.Api.Models
{
    using System;
    using System.Linq;

    /// <summary>
    ///  Encapsulates data related to a client-certificate.
    /// </summary>
    public class CertificateModel
    {
        /// <summary>
        /// Gets or sets the EDIPI found within the DoD Certificate.
        /// </summary>
        /// <value>The EDIPI.</value>
        public string EDIPI { get; set; }

        /// <summary>
        /// Gets or sets the certificate's subject.
        /// </summary>
        /// <value>The subject found on the client certificate.</value>
        public string Subject { get; set; }
    }
}