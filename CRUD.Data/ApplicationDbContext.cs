using System.Runtime.InteropServices;
using CRUD.Model;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
    {
        
    }
    public DbSet<Mahsol> Mahsols { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}