using System.ComponentModel.DataAnnotations;

namespace GraphQLPratice.Parameters
{
    public class LoginParameter
    {
        [Required(ErrorMessage = "請輸入email")]
        [EmailAddress(ErrorMessage = "請輸入正確email格式")]
        public string Email { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "密碼須包含一個英文字母與一個數字,且最短長度為8")]
        public string Password { get; set; }
    }
}
