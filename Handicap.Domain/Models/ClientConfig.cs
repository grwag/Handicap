using System;

namespace Handicap.Domain.Models
{
    public class ClientConfig
    {
        public string ClientId { get; set; }
        public string Issuer { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
        public string PostLogoutRedirectUri { get; set; }
        public string ResponseType { get; set; }
        public string RedirectUri { get; set; }
    }
}