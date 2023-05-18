using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace woodgroveapi.Models
{

    public class AuthenticationContext
    {
        public string correlationId { get; set; }
        public Client client { get; set; }
        public string protocol { get; set; }
        public ClientServicePrincipal clientServicePrincipal { get; set; }
        public ResourceServicePrincipal resourceServicePrincipal { get; set; }
        public User? user { get; set; }
    }

    public class Client
    {
        public string ip { get; set; }
        public string locale { get; set; }
        public string market { get; set; }
    }

    public class ClientServicePrincipal
    {
        public string id { get; set; }
        public string appId { get; set; }
        public string appDisplayName { get; set; }
        public string displayName { get; set; }
    }


    public class ResourceServicePrincipal
    {
        public string id { get; set; }
        public string appId { get; set; }
        public string appDisplayName { get; set; }
        public string displayName { get; set; }
    }

    public class User
    {
        // Display name
        [StringLength(50, ErrorMessage = "DisplayName length can't be more than 50.")]
        public string? displayName { get; set; }

        // Give name
        [StringLength(50, ErrorMessage = "GivenName length can't be more than 50.")]
        public string? givenName { get; set; }

        // User object ID
        [StringLength(50, ErrorMessage = "ID length can't be more than 50.")]
        public string? id { get; set; }

        // Mail address
        [StringLength(50, ErrorMessage = "Mail length can't be more than 50.")]
        public string? mail { get; set; }

        // User peferred language
        [StringLength(50, ErrorMessage = "PeferredLanguage length can't be more than 50.")]
        public string? preferredLanguage { get; set; }

        // Surname
        [StringLength(50, ErrorMessage = "Surname length can't be more than 50.")]
        public string? surname { get; set; }

        // UPN
        [StringLength(50, ErrorMessage = "Surname length can't be more than 50.")]
        public string? userPrincipalName { get; set; }

        // User type
        [StringLength(50, ErrorMessage = "UserType length can't be more than 50.")]
        public string? userType { get; set; }

        // Country
        [StringLength(50, ErrorMessage = "Country length can't be more than 50.")]
        public string? country { get; set; }

        // City
        [StringLength(50, ErrorMessage = "City length can't be more than 50.")]
        public string? city { get; set; }
    }
}