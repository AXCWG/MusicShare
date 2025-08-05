using System;
using System.Collections.Generic;

namespace MusicShare.Backend.Models;

public partial class Post
{
    public string Uuid { get; set; } = null!;

    public string Title { get; set; } = null!;

    public DateTime Pubtime { get; set; }

    public string Content { get; set; } = null!;

    public string? Mod { get; set; }

    public long Vote { get; set; }
}
