using FirstMvcApp.Data;
using FirstMvcApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstMvcApp.DataAccess;

public class CommentsRepository : Repository<Comment>
{
    public CommentsRepository(NewsAggregatorContext context) : base(context)
    {
    }
}