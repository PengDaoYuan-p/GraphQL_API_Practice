namespace GraphQLPratice.Filter
{
    public class GraphQLErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            if(error.Exception != null)
            {
                //Return custom error message
                return error
                    .WithMessage(error.Exception.Message)
                    .RemoveLocations()
                    .RemovePath()
                    .RemoveExtensions();
            }
            else
            {
                return error;
            }
            
        }

    }
}
