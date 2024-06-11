﻿using System;
using System.Collections.Generic;

namespace tugtog_an.data.Repo;

public partial class Collectionsong
{
    public Guid Collectionsongid { get; set; }

    public Guid Collectionid { get; set; }

    public string Spotifyid { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Artists { get; set; } = null!;

    public string? Albumname { get; set; }

    public string? Albumid { get; set; }

    public string? Epname { get; set; }

    public string? Epid { get; set; }

    public DateTime Createdat { get; set; }

    public virtual Collection Collection { get; set; } = null!;
}
