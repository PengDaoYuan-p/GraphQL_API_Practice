using ProjectPratice.Common;

namespace GraphQLPratice.GraphQL.Types
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }

        public Role Role { get; set; }

    }
}
