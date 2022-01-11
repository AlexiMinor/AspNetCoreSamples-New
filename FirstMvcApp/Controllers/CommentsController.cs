using FirstMvcApp.Data;
using FirstMvcApp.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FirstMvcApp.Controllers;

public class CommentsController : Controller
{
    private readonly NewsAggregatorContext _db;

    public CommentsController(NewsAggregatorContext db)
    {
        _db = db;
    }


    [HttpGet]
    public IActionResult Create()
    {
        var model = new CreateCommentRequestModel();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCommentRequestModel? request)
    {
        if (request == null) 
            return BadRequest();

        var comment = new Comment()
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            ArticleId = request.ArticleId,
            Text = request.Text,
            CreationDateTime = DateTime.Now
        };
        await _db.Comments.AddAsync(comment);
        await _db.SaveChangesAsync();

        return RedirectToAction("Details", 
            "Articles", 
            new { id = request.ArticleId });

    }
}