using Microsoft.EntityFrameworkCore;
using WeGout.Entities;

namespace WeGout
{
    public class WGContext : DbContext
    {
        public WGContext(DbContextOptions<WGContext> options) : base(options) { }

        public WGContext()
        {
            Database.Migrate();
        }

        public DbSet<CL_Gender> CL_Gender { get; set; }
        public DbSet<FileStorage> FileStorage { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Place> Place { get; set; }
        public DbSet<Owner> Owner { get; set; }
        //public DbSet<Menu> Menu { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CL_Gender>().HasData(
               new CL_Gender() { Name = "None", Id = 1 },
               new CL_Gender() { Name = "Female", Id = 2 },
               new CL_Gender() { Name = "Male", Id = 3 }
           );
            modelBuilder.Entity<FileStorage>().HasData(
               new FileStorage() { Name = "none", Extension = "jpg", Path = "none", Id = 1 }
           );
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, Name = "Ugurcan", Surname = "Bagriyanik", Email = "ugur@wegout.com", GenderId = 1, PhoneNumber = "05398478481", Password = "12345", ProfilePhotoId = 1 }
                );

        }

        public void Reset()
        {
            var entries = this.ChangeTracker
                                           .Entries()
                                           .Where(e => e.State != EntityState.Unchanged)
                                           .ToList();
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }




    }
}
