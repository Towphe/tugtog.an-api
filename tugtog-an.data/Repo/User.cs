using System;
using System.Collections.Generic;

namespace tugtog_an.data.Repo;

public partial class User
{
    public Guid Userid { get; set; }

    public string? Spotifyuserid { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Playlistcount { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime Createdat { get; set; }

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();

    public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
}
