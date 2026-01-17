using Cattobot.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Cattobot.Db;

public class CattobotDbContext(DbContextOptions<CattobotDbContext> options) : DbContext(options)
{
    public DbSet<FilmDb> Films { get; set; }
    
    public DbSet<FilmGuildDb> FilmGuilds { get; set; }
    
    public DbSet<FilmGuildMemberDb> FilmGuildMembers { get; set; }
    
    public DbSet<TrackDb> TrackDb { get; set; }
    public DbSet<TrackQueueDb> TrackQueueDb { get; set; }
    public DbSet<TrackQueueItemDb> TrackQueueItemDb { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasPostgresExtension("pg_trgm");

        builder.Entity<TrackDb>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.ExternalUrl).IsUnique(true);
        });

        builder.Entity<TrackQueueItemDb>(b =>
        {
            b.HasKey(x => x.Id);
            
            b.HasOne(x => x.NextItem).WithOne().HasForeignKey<TrackQueueItemDb>(x => x.NextItemId);
            b.HasOne(x => x.PrevItem).WithOne().HasForeignKey<TrackQueueItemDb>(x => x.PrevItemId);

            b.HasOne(x => x.Track).WithMany().HasForeignKey(x => x.TrackId);
            b.HasOne(x => x.Queue).WithMany().HasForeignKey(x => x.QueueId);

            b.Property(x => x.AddedOn).HasDefaultValueSql("now()");
        });

        builder.Entity<TrackQueueDb>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.GuildId).IsUnique(true);
            b.HasOne(x => x.CurrentTrack).WithOne().HasForeignKey<TrackQueueDb>(x => x.CurrentTrackId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<FilmDb>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();

            b.HasIndex(x => x.SearchIndex)
                .HasMethod("gin")
                .HasOperators("gin_trgm_ops");

            b.HasIndex(x => x.TmdbId)
                .HasFilter($"\"{nameof(FilmDb.TmdbId)}\" IS NOT NULL")
                .IsUnique(true);
        });

        builder.Entity<FilmGuildDb>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();

            b.HasIndex(x => x.GuildId).IsUnique(false);
            b.HasIndex(x => new { x.GuildId, x.FilmId }).IsUnique(true);
            
            b.HasOne(x => x.Film).WithMany(x => x.Guilds).HasForeignKey(x => x.FilmId);
            b.HasMany(x => x.Members).WithOne().HasForeignKey(x => x.FilmGuildId);
            
            b.Property(x => x.StatusOn).HasDefaultValueSql("now()");
        });
        
        builder.Entity<FilmGuildMemberDb>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();
            
            b.HasIndex(x => x.UserId).IsUnique(false);
            
            b.Property(x => x.AddedOn).HasDefaultValueSql("now()");
        });
        
        base.OnModelCreating(builder);
    }
}