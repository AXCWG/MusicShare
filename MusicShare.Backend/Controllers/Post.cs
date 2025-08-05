using Microsoft.AspNetCore.Mvc;
using MusicShare.Backend.Context;

namespace MusicShare.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class Post : Controller
{
    public ActionResult<object> Get()
    {
        using var context = new MusicShareDbContext(); 
        context.Posts.
    }

    [HttpPost]
    public IActionResult Post([FromBody] Post post)
    {
        
    }
}