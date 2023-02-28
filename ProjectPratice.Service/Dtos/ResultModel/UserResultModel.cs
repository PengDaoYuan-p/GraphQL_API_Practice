using ProjectPratice.Common;

namespace ProjectPratice.Service.Dtos.ResultModel
{
    public class UserResultModel
    {
        public int Id { get; set; } 

        public string Email { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
    }
}
