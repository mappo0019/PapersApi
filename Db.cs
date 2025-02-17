using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

class PapersDb : DbContext
{
    public PapersDb(DbContextOptions<PapersDb> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();

    public DbSet<Project> Projects => Set<Project>();

}

