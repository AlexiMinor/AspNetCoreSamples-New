using FirstMvcApp.Data;
using FirstMvcApp.Data.Entities;

namespace FirstMvcApp.DataAccess;

public class UserRoleRepository : Repository<UserRole>
{
    public UserRoleRepository(NewsAggregatorContext context) : base(context)
    {
    }
}