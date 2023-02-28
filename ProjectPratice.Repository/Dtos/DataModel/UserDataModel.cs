using ProjectPratice.Common;

namespace ProjectPratice.Repository.Dtos.DataModel
{
    public class UserDataModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public  string? Password { get; set; }   

        public string Name { get; set; }

        public Role Role { get; set; }
    }
}
