using GraphQLPratice.Parameters;
using AutoMapper;
using GraphQLPratice.Mappings;
using GraphQLPratice.GraphQL.Types;
using ProjectPratice.Service.Interface;
using ProjectPratice.Service.Dtos.Info;
using ProjectPratice.Service.Dtos.ResultModel;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using HotChocolate.AspNetCore.Authorization;

namespace GraphQLPratice.GraphQL
{
    public class Mutation
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly ICardService _cardService;
        private readonly IUserService _userService;

        public Mutation(
            ICardService cardService, 
            IUserService userService,
            IConfiguration configuration,
            ILogger<Mutation> logger)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<GraphqlTypeMappings>());
            this._mapper = config.CreateMapper();
            this._configuration = configuration;
            this._cardService = cardService;
            this._userService = userService;
            this._logger = logger;
        }


        /// <summary>
        /// 新增使用者
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>     
        public User CreateUser( UserParameter parameter) 
        {
            
            var info = this._mapper.Map<UserParameter,UserInfo>(parameter);

            var resultModel = this._userService.Create(info);

            var result = this._mapper.Map<UserResultModel, User>(resultModel);

            return result;

        }


        /// <summary>
        /// 使用者登入
        /// </summary>
        /// <param name="tokenSettings"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public string UserLogin( LoginParameter parameter)
        {

            var info = this._mapper.Map<LoginParameter, LoginInfo>(parameter);

            var resultModel = this._userService.Login(info);

            //Get payload and sign jwt token
            var claims = new Claim[] {
                new Claim("Role", resultModel.Role.ToString()),
                new Claim("Name", resultModel.Name),
                new Claim("Id", resultModel.Id.ToString())
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTtokenSetting:Key"]));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["JWTtokenSetting:Issuer"],
                audience: _configuration["JWTtokenSetting:Audience"],
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials,
                claims: claims
            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;

        }


        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [Authorize(Policy = "Admin-policy")]
        public bool CreateCard( ClaimsPrincipal claimsPrincipal ,CardParameter parameter) {

            //For get user info and log 
            var userID = claimsPrincipal.FindFirstValue("Id");
            _logger.LogInformation("User id {@userId} create card", userID);

            var info = this._mapper.Map<CardParameter, CardInfo>(parameter);

            var isInsertSuccess = this._cardService.Insert(info);

            return isInsertSuccess;
        }


        /// <summary>
        /// 更新卡片
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [Authorize(Policy = "Admin-policy")]
        public bool UpdateCard(int id, CardParameter parameter) {

            //Find card
            var targetCard = this._cardService.Get(id);
            if (targetCard == null) return false;

            var info = this._mapper.Map<CardParameter, CardInfo>(parameter);

            var isUpdateSuccess = this._cardService.Update(id, info);

            return isUpdateSuccess;

        }


        /// <summary>
        /// 刪除卡片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Policy = "Admin-policy")]
        public bool DeleteCard(int id)
        {
            this._cardService.Delete(id);
            return true;
        }


    }
}
