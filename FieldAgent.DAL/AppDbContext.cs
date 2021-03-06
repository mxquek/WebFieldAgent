using System.Diagnostics;
using FieldAgent.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FieldAgent.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<AgencyAgent> AgencyAgents { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Alias> Aliases { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<MissionAgent> MissionAgents { get; set; }

        public DbSet<SecurityClearance> SecurityClearances { get; set; }


        public AppDbContext() : base()
        {

        }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(message => Debug.WriteLine(message), LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AgencyAgent>()
                .HasKey(aa => new {aa.AgencyId, aa.AgentId});
            builder.Entity<MissionAgent>()
                .HasKey(ma => new { ma.MissionId, ma.AgentId });
        }
    }
}
