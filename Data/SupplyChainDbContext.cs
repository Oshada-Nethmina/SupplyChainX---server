using Microsoft.EntityFrameworkCore;
using SupplyChainX.Models;

namespace SupplyChainX.Data;

public class SupplyChainDbContext : DbContext
{
     public SupplyChainDbContext(DbContextOptions<SupplyChainDbContext> options) : base(options) { }

    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
}