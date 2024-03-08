using Microsoft.EntityFrameworkCore;
using RinhAPI.Models;

namespace RinhAPI.Db;

public class RinhaDbContext(DbContextOptions<RinhaDbContext> options) : DbContext(options)
{
    public virtual DbSet<Cliente> Cliente => Set<Cliente>();
    public virtual DbSet<Transacao> Transacao => Set<Transacao>();
}
