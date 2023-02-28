using GraphQLPratice.GraphQL.Types;
using AutoMapper;
using GraphQLPratice.Mappings;
using GraphQLPratice.Parameters;
using ProjectPratice.Service.Interface;
using ProjectPratice.Service.Dtos.Info;
using ProjectPratice.Service.Dtos.ResultModel;


namespace GraphQLPratice.GraphQL
{
    public class Query
    {
        private readonly IMapper _mapper;
        private readonly ICardService _cardService;

        public Query(ICardService cardService)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<GraphqlTypeMappings>());
            this._mapper = config.CreateMapper();
            this._cardService = cardService;
        }

        /// <summary>
        /// 回傳卡片列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Card> Cards(CardSearchParameter parameter)
        {
            var info = this._mapper.Map<CardSearchParameter, CardSearchInfo>(parameter);

            var cards = this._cardService.GetList(info);

            var result = this._mapper.Map<IEnumerable<CardResultModel>, IEnumerable<Card>>(cards);

            return result;
        }


        /// <summary>
        /// 回傳一張卡片
        /// </summary>
        /// <returns></returns>
        public Card Card( int id )
        {   
            var card = this._cardService.Get(id);

            if (card == null) throw new Exception("Can not find a card!");

            var result = this._mapper.Map<CardResultModel,Card>(card);

            return result;
        }

    }
}
