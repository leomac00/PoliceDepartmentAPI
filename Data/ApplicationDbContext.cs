using DesafioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Arrest> Arrests { get; set; }
        public DbSet<Autopsy> Autopsies { get; set; }
        public DbSet<Coroner> Coroners { get; set; }
        public DbSet<Crime> Crimes { get; set; }
        public DbSet<Deputy> Deputies { get; set; }
        public DbSet<Perpetrator> Perpetrators { get; set; }
        public DbSet<PoliceDepartment> PoliceDepartments { get; set; }
        public DbSet<PoliceOfficer> PoliceOfficers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Victim> Victims { get; set; }
    }
}