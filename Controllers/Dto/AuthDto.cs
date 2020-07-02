namespace CORE.API.Controllers.Dto
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SocialLoginDto
    {
        public string Email { get; set; }
    }
}