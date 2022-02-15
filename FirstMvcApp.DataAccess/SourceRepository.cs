using FirstMvcApp.Data;
using FirstMvcApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstMvcApp.DataAccess;

public class SourceRepository : Repository<Source>
{
    public SourceRepository(NewsAggregatorContext context) : base(context)
    {
    }
}