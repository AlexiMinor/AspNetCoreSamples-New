using System;
using System.Text.Json.Serialization;

namespace WebApiFirstAppSample.Models.Responses
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string[] RoleNames { get; set; }
        public string JwtToken { get; set; }
        
        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}
