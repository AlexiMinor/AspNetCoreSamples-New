using FirstMvcApp.Data;
using FirstMvcApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstMvcApp.DataAccess;

public class RoleRepository : Repository<Role>
{
    public RoleRepository(NewsAggregatorContext context) : base(context)
    {
    }
}