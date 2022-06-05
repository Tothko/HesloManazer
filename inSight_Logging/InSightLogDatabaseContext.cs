using inSight_Logging.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inSight_Logging
{
    public class InSightLogDatabaseContext : DbContext
    {
        public InSightLogDatabaseContext(DbConnection connection) : base(connection, false)
        {

                Database.SetInitializer(new CreateDatabaseIfNotExists<InSightLogDatabaseContext>());

        }


        public DbSet<DefaultLog> DefaultLog { get; set; }
        public DbSet<ClientLog> ClientLog { get; set; }
        public DbSet<DataAccessLog> DatabaseLog { get; set; }
        public DbSet<ApiLog> ApiAccessLog { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<DefaultLog>()
              .HasKey(oi => oi.ID);

            modelBuilder.Entity<ClientLog>()
             .HasKey(oi => oi.ID);
            modelBuilder.Entity<DataAccessLog>()
             .HasKey(oi => oi.ID);
            modelBuilder.Entity<ApiLog>()
             .HasKey(oi => oi.ID);
        }
    }
}