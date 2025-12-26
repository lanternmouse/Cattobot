using Cattobot.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Cattobot.Db;

public class CattobotContext : DbContext
{
    public DbSet<Film> Films { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=cattobot;Username=postgres;Password=postgres");
}