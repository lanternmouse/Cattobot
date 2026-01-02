using Cattobot.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Cattobot.Db;

public class CattobotDbContext : DbContext
{
    public CattobotDbContext(DbContextOptions<CattobotDbContext> options) : base(options) { }
    
    public DbSet<FilmDb> Films { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}