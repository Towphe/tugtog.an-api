using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace tugtog_an.data.Repo;

public partial class TugtoganContext : DbContext
{
    public TugtoganContext()
    {
    }

    public TugtoganContext(DbContextOptions<TugtoganContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Collection> Collections { get; set; }

    public virtual DbSet<Collectionsong> Collectionsongs { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }

    public virtual DbSet<Playlistsong> Playlistsongs { get; set; }

    public virtual DbSet<User> Users { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseNpgsql("Host=localhost;Database=tugtogan;Username=tope;Password=pingu");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Collection>(entity =>
        {
            entity.HasKey(e => e.Collectionid).HasName("collections_pkey");

            entity.ToTable("collections");

            entity.Property(e => e.Collectionid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("collectionid");
            entity.Property(e => e.Collectionname)
                .HasMaxLength(120)
                .HasColumnName("collectionname");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Collections)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user");
        });

        modelBuilder.Entity<Collectionsong>(entity =>
        {
            entity.HasKey(e => e.Collectionsongid).HasName("collectionsongs_pkey");

            entity.ToTable("collectionsongs");

            entity.Property(e => e.Collectionsongid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("collectionsongid");
            entity.Property(e => e.Albumid)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("albumid");
            entity.Property(e => e.Albumname)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("albumname");
            entity.Property(e => e.Artists)
                .HasColumnType("jsonb")
                .HasColumnName("artists");
            entity.Property(e => e.Collectionid).HasColumnName("collectionid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Epid)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("epid");
            entity.Property(e => e.Epname)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("epname");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Spotifyid)
                .HasColumnType("character varying")
                .HasColumnName("spotifyid");

            entity.HasOne(d => d.Collection).WithMany(p => p.Collectionsongs)
                .HasForeignKey(d => d.Collectionid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_collection");
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.Playlistid).HasName("playlists_pkey");

            entity.ToTable("playlists");

            entity.Property(e => e.Playlistid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("playlistid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Playlistname)
                .HasMaxLength(120)
                .HasColumnName("playlistname");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Playlists)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user");
        });

        modelBuilder.Entity<Playlistsong>(entity =>
        {
            entity.HasKey(e => e.Playlistsongid).HasName("playlistsongs_pkey");

            entity.ToTable("playlistsongs");

            entity.Property(e => e.Playlistsongid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("playlistsongid");
            entity.Property(e => e.Albumid)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("albumid");
            entity.Property(e => e.Albumname)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("albumname");
            entity.Property(e => e.Artists)
                .HasColumnType("jsonb")
                .HasColumnName("artists");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Epid)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("epid");
            entity.Property(e => e.Epname)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("epname");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Playlistid).HasColumnName("playlistid");
            entity.Property(e => e.Spotifyid)
                .HasColumnType("character varying")
                .HasColumnName("spotifyid");

            entity.HasOne(d => d.Playlist).WithMany(p => p.Playlistsongs)
                .HasForeignKey(d => d.Playlistid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_playlist");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Userid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("userid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Playlistcount)
                .HasDefaultValue(0)
                .HasColumnName("playlistcount");
            entity.Property(e => e.Spotifyuserid)
                .HasMaxLength(255)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("spotifyuserid");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
