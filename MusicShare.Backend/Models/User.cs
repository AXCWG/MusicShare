using System;
using System.Collections.Generic;

namespace MusicShare.Backend.Models;

public partial class User
{
    public string Uuid { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public string? Mod { get; set; }

    public DateTime Regtime { get; set; }
}
