

using AutoMapper;
using ProjectPratice.Service.Dtos.Info;
using ProjectPratice.Service.Dtos.ResultModel;
using ProjectPratice.Repository.Dtos.Condition;
using ProjectPratice.Repository.Dtos.DataModel;


namespace ProjectPratice.Service.Mappings
{
    public class ServiceMappings : Profile
    {
        public ServiceMappings()
        {
            // Info -> Condition
            this.CreateMap<CardInfo, CardCondition>();
            this.CreateMap<CardSearchInfo, CardSearchCondition>();
            this.CreateMap<UserInfo, UserCondition>();

            // DataModel -> ResultModel
            this.CreateMap<CardDataModel, CardResultModel>();
            this.CreateMap<UserDataModel, UserResultModel>();
            this.CreateMap<UserDataModel, LoginResultModel>();
        }
    }
}
