using Microsoft.AspNetCore.Identity;

namespace WebAPI_v3.Models.DTO
{
    public class LoginResponseDTO
    {
        public string JwtToken { set; get; }
    }
}
