using Cattobot.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Cattobot.Db;

public class CattobotDbContext(DbContextOptions<CattobotDbContext> options) : DbContext(options)
{
    public DbSet<FilmDb> Films { get; set; }
    
    public DbSet<FilmGuildDb> FilmGuilds { get; set; }
    
    public DbSet<FilmGuildMemberDb> FilmGuildMembers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<FilmDb>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();

            b.HasIndex(x => x.KinopoiskId)
                .HasFilter($"\"{nameof(FilmDb.KinopoiskId)}\" IS NOT NULL")
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