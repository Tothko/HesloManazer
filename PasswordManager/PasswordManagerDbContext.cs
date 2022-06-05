using PasswordManager.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    public class PasswordManagerDbContext : DbContext
    {
        public PasswordManagerDbContext(DbConnection connection) : base(connection, false)
        {

            Database.SetInitializer(new CreateDatabaseIfNotExists<PasswordManagerDbContext>());

        }


        public DbSet<User> Users { get; set; }
        public DbSet<Password> Passwords { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<User>()
              .HasKey(oi => oi.Id);

            modelBuilder.Entity<Password>()
             .HasKey(oi => oi.Id);

        }
    }
}

