using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicShare.Backend.Context;
using MusicShare.Backend.Extensions;
using MusicShare.Backend.Models;

namespace MusicShare.Backend.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserActions : Controller
{
    [HttpPost]
    public IActionResult Login([FromBody]UserPost user)
    {
        using var context = new MusicShareDbContext();
        var r = context.Users.FirstOrDefault(i => (i.Username == user.Username) &&
                                          (i.Password == user.Password.Sha256HexHashString()));
        if (r is null) return Unauthorized("Invalid username or password.\n用户名或密码错误。");
        HttpContext.Session.SetString("uuid", r.Uuid);
        return Ok();

    }

    public class UserPost
    {
        public required string Username { get; set; }
        public required string Password { get; set;  }
        public string? Email { get; set;  }
    }
    [HttpPost]
    public IActionResult Register([FromBody] UserPost user)
    {
        try
        {
            var u = Guid.NewGuid().ToString();
            using var context = new MusicShareDbContext();
            if (context.Users.Any(i => i.Username == user.Username))
            {
                return BadRequest("Username already exists.\n用户名已存在。");
            }
            context.Users.Add(new()
            {
                Uuid = u,
                Username = user.Username,
                Password = user.Password.Sha256HexHashString(),
                Email = user.Email,
                Regtime = DateTime.Now
            });
            context.SaveChanges();
            HttpContext.Session.SetString("uuid", u);
            return Ok();
        }
        catch (DbUpdateException)
        {
            return BadRequest("Please contact server admin. 请联系网站管理员。");
        }
        
    }
}