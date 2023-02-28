using ProjectPratice.Service.Dtos.Info;
using ProjectPratice.Service.Dtos.ResultModel;

namespace ProjectPratice.Service.Interface
{
    public interface IUserService
    {
        /// <summary>
        /// 創建一名使用者
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        UserResultModel Create(UserInfo info);

        /// <summary>
        /// 使用者登入
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        LoginResultModel Login(LoginInfo info);

    }
}
