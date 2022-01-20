using FirstMvcApp.Data;

namespace FirstMvcApp.DataAccess;

public class UserRepository : Repository<User>
{
    public UserRepository(NewsAggregatorContext context) : base(context)
    {
    }
}