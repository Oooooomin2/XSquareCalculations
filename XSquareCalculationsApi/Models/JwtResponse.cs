using System;
namespace XSquareCalculationsApi.Models
{
    public sealed class JwtResponse
    {
        public int UserId { get; set; }
        public string IdToken { get; set; }
        public DateTime ExpiredDateTime { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
