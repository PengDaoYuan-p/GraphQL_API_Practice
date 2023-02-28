using ProjectPratice.Service.Interface;
using ProjectPratice.Service.Dtos.Info;
using ProjectPratice.Service.Dtos.ResultModel;
using ProjectPratice.Service.Mappings;
using ProjectPratice.Repository.Dtos.Condition;
using ProjectPratice.Repository.Dtos.DataModel;
using ProjectPratice.Repository.Interface;
using ProjectPratice.Common;
using AutoMapper;
using System.Security.Cryptography;
using System.Text;

namespace ProjectPratice.Service.Implement
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// 建構式
        /// </summary>
        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;

            var config = new MapperConfiguration(cfg => cfg.AddProfile<ServiceMappings>());

            this._mapper = config.CreateMapper();
        }


        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public UserResultModel Create(UserInfo info)
        {
            //check user email existed or not
            var existed = _userRepository.Get(info.Email);
            if (existed != null) throw new Exception("User Existed!");

            //hash password
            info.Password = GetSHA512Hash(info.Password);

            //set Role
            info.Role = Role.general;

            //save to database and return
            var condititon = this._mapper.Map<UserInfo, UserCondition>(info);
            var data = this._userRepository.Create(condititon);
            var result = this._mapper.Map<UserDataModel, UserResultModel>(data);

            return result;
        }


        /// <summary>
        /// User login
        /// </summary>
        /// <param name="info"></param>
        /// <returns>LoginResultModel that can assign to jwt payload</returns>
        /// <exception cref="Exception"></exception>
        public LoginResultModel Login( LoginInfo info)
        {
            //Find user exist or not
            var user = _userRepository.Get(info.Email);
            if (user == null) throw new Exception("Can not find user!");

            //Check password
            info.Password = GetSHA512Hash(info.Password);
            if (user.Password != info.Password) throw new Exception("Password incorrect!");

            //return 
            var result = this._mapper.Map<UserDataModel, LoginResultModel>(user);

            return result;
        }


        /// <summary>
        /// For hash password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private string GetSHA512Hash(string password)
        {
            SHA512 SHA512Haser = SHA512.Create();
            Byte[] data = SHA512Haser.ComputeHash(Encoding.Default.GetBytes(password));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

    }
}
