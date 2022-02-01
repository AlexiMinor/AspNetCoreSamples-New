using FirstMvcApp.Data;
using Microsoft.EntityFrameworkCore;

namespace FirstMvcApp.DataAccess;

public class UserRepository : Repository<User>
{
    public UserRepository(NewsAggregatorContext context) : base(context)
    {
    }
}