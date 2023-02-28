using ProjectPratice.Repository.Interface;
using ProjectPratice.Repository.Dtos.Condition;
using ProjectPratice.Repository.Dtos.DataModel;
using System.Data.SqlClient;
using Dapper;

namespace ProjectPratice.Repository.Implement
{
    public class UserRepository : IUserRepository
    {

        private readonly string _connectString;

        public UserRepository (string connectString)
        {
            this._connectString = connectString;
        }

        /// <summary>
        /// 新增一個使用者至Database
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public UserDataModel Create(UserCondition condition) {

            var sql =
                @"
                INSERT INTO PraticeUser 
                (
                   [email]
                  ,[password]
                  ,[name]
                  ,[role]
                )
                OUTPUT INSERTED.Id, INSERTED.email, INSERTED.name, INSERTED.role
                VALUES 
                (
                    @Email
                   ,@Password
                   ,@Name
                   ,@Role
                );            
            ";

            using (var conn = new SqlConnection(_connectString))
            {
                UserDataModel result = conn.QuerySingle<UserDataModel>(sql, condition);
                return result;
            }
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public UserDataModel Get(string email)
        {
            var sql =
            @"		
                SELECT Id, email, password, name, role 
                FROM PraticeUser 
                Where email = @email
            ";

            var parameters = new DynamicParameters();
            parameters.Add("email", email, System.Data.DbType.String);

            using (var conn = new SqlConnection(_connectString))
            {
                var result = conn.QueryFirstOrDefault<UserDataModel>(sql, parameters);
                return result;
            }
        }



    }
}
