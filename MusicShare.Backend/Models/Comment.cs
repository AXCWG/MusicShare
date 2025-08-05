using System;
using System.Collections.Generic;

namespace MusicShare.Backend.Models;

public partial class Comment
{
    public string Uuid { get; set; } = null!;

    public string? Master { get; set; }

    public string? Content { get; set; }

    public DateTime? Pubtime { get; set; }

    public long? Vote { get; set; }

    public string? Mod { get; set; }
}
