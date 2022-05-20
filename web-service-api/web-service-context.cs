using Microsoft.EntityFrameworkCore;
using web_service_api;

namespace web_service_api
{
    public class WebServiceContext : DbContext
    {
        private const string connectionString = "server=localhost;port=3306;database=WebService;user=root;";
        //private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=WhenUpDB;Trusted_Connection=True;MultipleActiveResultSets=true";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, MariaDbServerVersion.AutoDetect(connectionString));
            //optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the Name property as the primary
            // key of the Items table
            modelBuilder.Entity<User>().HasKey(e => e.UserName);
            modelBuilder.Entity<Contact>().HasKey(e => e.key);
            modelBuilder.Entity<Message>().HasKey(e => e.id);

            modelBuilder.Entity<Ranking>().HasKey(e => e.id);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DbSet<Ranking> Rankings { get; set; }

 

    }
}
