using ProjectPratice.Common;

namespace ProjectPratice.Service.Dtos.ResultModel
{
    public class LoginResultModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
    }
}
