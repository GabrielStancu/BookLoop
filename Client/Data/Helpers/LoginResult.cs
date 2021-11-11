using Data.Models;

namespace Data.Helpers
{
    public class LoginResult
    {
        public BookUser User { get; set; }
        public bool Successful { get; set; }
    }
}
