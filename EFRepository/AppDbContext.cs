using Microsoft.EntityFrameworkCore;

namespace SampleAPI.EFRepository
{


    public class AppDbContext : DbContext
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options">
        ///     You can enable context pooling if you pass a pooled context
        ///     see: https://learn.microsoft.com/en-us/ef/core/performance/advanced-performance-topics?tabs=with-di%2Cexpression-api-with-constant#dbcontext-pooling
        /// </param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<AppGroup> AppGroups { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<AppUser>()
            //    .HasMany(x=>x.AdminOfGroups)
            //    .WithOne(x=> x.Admin)
            //    .HasForeignKey(x=>x.AdminId)
            //    .IsRequired();

            modelBuilder.Entity<AppGroup>()
                .HasOne(x=>x.Admin)
                .WithMany(x=> x.AdminOfGroups)
                .HasForeignKey(x=>x.AdminId)
                .IsRequired();

            modelBuilder.Entity<AppUser>()
                .HasMany(x => x.Groups)
                .WithMany(x => x.Users)
                .UsingEntity<AppUsersInGroups>(
                    r => r.HasOne<AppGroup>().WithMany().HasForeignKey(x => x.GroupId),
                    l => l.HasOne<AppUser>().WithMany().HasForeignKey(x => x.UserId)
                    );

            base.OnModelCreating(modelBuilder);
        }

    }

}
