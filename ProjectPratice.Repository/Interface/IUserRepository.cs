using ProjectPratice.Repository.Dtos.Condition;
using ProjectPratice.Repository.Dtos.DataModel;


namespace ProjectPratice.Repository.Interface
{
    public interface IUserRepository
    {
        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        UserDataModel Create(UserCondition condition);


        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        UserDataModel Get(string email);
       

    }
}
