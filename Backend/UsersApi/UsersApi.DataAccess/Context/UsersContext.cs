using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Users.Entities.Entities.Abstract;
using Users.Entities.Entities.Concreate;

namespace Users.DataAccess.Context
{
    public class UsersContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /* Usesr.API katmanındakijson dosyasından connection stringi almak için aşağıdaki paketler yüklenmeli
            Microsoft.Extensions.Configuration.FileExtensions
            Microsoft.Extensions.Configuration.Json*/

            ConfigurationManager configurationManager = new ConfigurationManager();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Users.API"));
            configurationManager.AddJsonFile("appsettings.json");
            optionsBuilder.UseSqlServer(configurationManager.GetConnectionString("DefaultConnection"));
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*Bu metot fluent api işlemlerinin yapıldığı yerdir.
             EntityTypeConfiguration ile her entity e ait özellikler başka bir class ta yazılıp buraya alınabilir
            */
        }

        /* Interceptor(interseptır) işlemiiçin kullanıldı.
         * Interceptor request ile response arsına girme işlemidir.
         * Her entity de bulunan CreatedOn ve UpdatedOn property lerini set etmek için kullanıldı.
         * ChangeTracker entit ler üzerinde yapılan değişikliklerin yakalandığı property dir.
         */
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities=ChangeTracker.Entries<IEntity>();
            foreach (var entity in entities)
            {
                if (entity.State==EntityState.Added)
                {
                    entity.Entity.CreatedOn = DateTime.Now;
                }
                else if (entity.State==EntityState.Modified)
                {
                    entity.Entity.UpdatedOn = DateTime.Now;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
    }
}
