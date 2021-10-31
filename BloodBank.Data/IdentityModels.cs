using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BloodBank.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Blood> Inventory { get; set; }
        public DbSet<BloodOrder> Orders { get; set; }
        public DbSet<DonorAppointment> Appointments { get; set; }
        public DbSet<BloodTable> Table {get; set;}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Conventions
                .Remove<PluralizingTableNameConvention>();

            modelBuilder
                .Configurations
                .Add(new IdentityUserLoginConfiguration())
                .Add(new IdentityUserRoleConfiguration());
        }

        public void Seed(ApplicationDbContext Context)
        {

            // Create my debug (testing) objects here
            var TestMyClass = new BloodTable() { Type = BloodType.Ap };
            Context.Table.Add(TestMyClass);

            // Normal seeding goes here
            Context.Table.Add(new BloodTable() { Type = BloodType.An });
            Context.Table.Add(new BloodTable() { Type = BloodType.Bp });
            Context.Table.Add(new BloodTable() { Type = BloodType.Bn });
            Context.Table.Add(new BloodTable() { Type = BloodType.ABp });
            Context.Table.Add(new BloodTable() { Type = BloodType.ABn });
            Context.Table.Add(new BloodTable() { Type = BloodType.Op });
            Context.Table.Add(new BloodTable() { Type = BloodType.On });


            Context.SaveChanges();
        }

        public class DropCreateIfChangeInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                context.Seed(context);

                base.Seed(context);
            }
        }

        public class CreateInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                context.Seed(context);

                base.Seed(context);
            }
        }

        static ApplicationDbContext()
        {
            #if DEBUG
            Database.SetInitializer<ApplicationDbContext>(new DropCreateIfChangeInitializer());
            #else
            Database.SetInitializer<MyDbContext> (new CreateInitializer ());
            #endif
        }
    }

    public class IdentityUserLoginConfiguration : EntityTypeConfiguration<IdentityUserLogin>
    {
        public IdentityUserLoginConfiguration()
        {
            HasKey(iul => iul.UserId);
        }
    }

    public class IdentityUserRoleConfiguration : EntityTypeConfiguration<IdentityUserRole>
    {
        public IdentityUserRoleConfiguration()
        {
            HasKey(iur => iur.UserId);
        }
    }
}