using ETL_web_project.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETL_web_project.Data.Context
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<DimDate> DimDates { get; set; }
        public DbSet<DimStore> DimStores { get; set; }
        public DbSet<DimProduct> DimProducts { get; set; }
        public DbSet<DimCustomer> DimCustomers { get; set; }

       
        public DbSet<FactSales> FactSales { get; set; }

        public DbSet<SalesRaw> SalesRaws { get; set; }

        
        public DbSet<EtlJob> EtlJobs { get; set; }
        public DbSet<EtlRun> EtlRuns { get; set; }
        public DbSet<EtlLog> EtlLogs { get; set; }

     
        public DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<DimDate>().ToTable("DimDate", schema: "dw");
            modelBuilder.Entity<DimStore>().ToTable("DimStore", schema: "dw");
            modelBuilder.Entity<DimProduct>().ToTable("DimProduct", schema: "dw");
            modelBuilder.Entity<DimCustomer>().ToTable("DimCustomer", schema: "dw");

            modelBuilder.Entity<FactSales>().ToTable("FactSales", schema: "dw");

            modelBuilder.Entity<SalesRaw>().ToTable("SalesRaw", schema: "stg");

            modelBuilder.Entity<EtlJob>().ToTable("EtlJob", schema: "etl");
            modelBuilder.Entity<EtlRun>().ToTable("EtlRun", schema: "etl");
            modelBuilder.Entity<EtlLog>().ToTable("EtlLog", schema: "etl");

            modelBuilder.Entity<UserAccount>().ToTable("UserAccount", schema: "auth");

            modelBuilder.Entity<UserAccount>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<UserAccount>()
                .HasIndex(u => u.Email)
                .IsUnique();


            base.OnModelCreating(modelBuilder);
        }
    }
}
