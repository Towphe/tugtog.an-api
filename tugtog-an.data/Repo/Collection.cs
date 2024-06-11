using System;
using System.Collections.Generic;

namespace tugtog_an.data.Repo;

public partial class Collection
{
    public Guid Collectionid { get; set; }

    public Guid Userid { get; set; }

    public string Collectionname { get; set; } = null!;

    public DateTime Createdat { get; set; }

    public virtual ICollection<Collectionsong> Collectionsongs { get; set; } = new List<Collectionsong>();

    public virtual User User { get; set; } = null!;
}
