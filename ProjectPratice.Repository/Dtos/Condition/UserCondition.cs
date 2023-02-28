using ProjectPratice.Common;

namespace ProjectPratice.Repository.Dtos.Condition
{
    public class UserCondition
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
    }
}
