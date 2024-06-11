using System;
using System.Collections.Generic;

namespace tugtog_an.data.Repo;

public partial class Playlist
{
    public Guid Playlistid { get; set; }

    public Guid Userid { get; set; }

    public string Playlistname { get; set; } = null!;

    public DateTime Createdat { get; set; }

    public virtual ICollection<Playlistsong> Playlistsongs { get; set; } = new List<Playlistsong>();

    public virtual User User { get; set; } = null!;
}
