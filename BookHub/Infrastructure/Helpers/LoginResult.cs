using Infrastructure.DTOs;

namespace Infrastructure.Helpers
{
    public class LoginResult
    {
        public BookUserDTO User { get; set; }
        public bool Successful { get; set; }
    }
}
