using FirstMvcApp.Data;
using FirstMvcApp.Data.Entities;

namespace FirstMvcApp.DataAccess;

public class RefreshTokenRepository : Repository<RefreshToken>
{
    public RefreshTokenRepository(NewsAggregatorContext context) : base(context)
    {
    }
}