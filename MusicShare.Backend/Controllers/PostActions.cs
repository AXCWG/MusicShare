using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicShare.Backend.Context;
using MusicShare.Backend.Models;

namespace MusicShare.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class PostActions : Controller
{
    public class PostPost
    {
        public string Title { get; set; } = null!;


        public string Content { get; set; } = null!;
    }

    public ActionResult<object> Get()
    {
        using var context = new MusicShareDbContext();
        return Json(context.Posts.OrderByDescending(i => i.Pubtime).Take(5).ToList());
    }

    [HttpPost]
    public IActionResult Post([FromBody] PostPost post)
    {
        try
        {
            using var context = new MusicShareDbContext();
            if (HttpContext.Session.GetString("uuid") is null)
            {
                return Unauthorized("You must be logged in to post.\n您必须先登录。");
            }
            context.Posts.Add(new()
            {
                Uuid = Guid.NewGuid().ToString(), 
                Title = post.Title,
                Content = post.Content,
                Pubtime = DateTime.Now, 
                Vote = 0
            });
            context.SaveChanges();
            return Ok();
        }
        catch (DbUpdateException)
        {
            return BadRequest("Please contact server admin. 请联系网站管理员。");
        }
    }
}