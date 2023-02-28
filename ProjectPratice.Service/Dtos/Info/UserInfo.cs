using ProjectPratice.Common;

namespace ProjectPratice.Service.Dtos.Info
{
    public class UserInfo
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
    }
}
