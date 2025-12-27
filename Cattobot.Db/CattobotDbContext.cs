using Cattobot.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Cattobot.Db;

public class CattobotDbContext : DbContext
{
    public CattobotDbContext(DbContextOptions<CattobotDbContext> options) : base(options)
    {
    }
    
    public DbSet<FilmDb> Films { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql("Host=localhost;Database=cattobot;Username=postgres;Password=A1qwert");
    }
}