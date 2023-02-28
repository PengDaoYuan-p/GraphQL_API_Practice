using AutoMapper;
using GraphQLPratice.GraphQL.Types;
using GraphQLPratice.Parameters;
using ProjectPratice.Service.Dtos.Info;
using ProjectPratice.Service.Dtos.ResultModel;

namespace GraphQLPratice.Mappings
{
    public class GraphqlTypeMappings : Profile
    {
        public GraphqlTypeMappings()
        {
            //Parameter -> info
            this.CreateMap<CardParameter, CardInfo>();
            this.CreateMap<CardSearchParameter, CardSearchInfo>();
            this.CreateMap<UserParameter, UserInfo>();
            this.CreateMap<LoginParameter, LoginInfo>();


            //ResultModel -> graphQL type
            this.CreateMap<CardResultModel, Card>();
            this.CreateMap<UserResultModel, User>();
        }
    }
}
