using System.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WeGout.Entities;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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
               new CL_Gender() {Id = 1, Name = "None"  },
               new CL_Gender() {Id = 2, Name = "Female"  },
               new CL_Gender() {  Id = 3, Name = "Male"}
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
        
        public List<T> ExecSQL<T>(string query)
        {
            using (var command = Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                Database.OpenConnection();

                List<T> list = new List<T>();
                using (var result = command.ExecuteReader())
                {
                    T obj = default(T);
                    while (result.Read())
                    {
                        obj = Activator.CreateInstance<T>();
                        foreach (PropertyInfo prop in obj.GetType().GetProperties())
                        {
                            if (!object.Equals(result[prop.Name], DBNull.Value))
                            {
                                prop.SetValue(obj, result[prop.Name], null);
                            }
                        }
                        list.Add(obj);
                    }
                }
                Database.CloseConnection();
                return list;
            }
        }
        



    }
}
