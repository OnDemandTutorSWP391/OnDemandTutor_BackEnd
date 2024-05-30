using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    public class MyDbContext : IdentityDbContext<User>
    {
        public MyDbContext()
        {
            
        }

        public MyDbContext(DbContextOptions<MyDbContext> option) : base(option) 
        {
            
        }

        #region DbSet
        public virtual DbSet<IdentityUserToken<string>> UserTokens {  get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<User> Users {  get; set; }
        public virtual DbSet<Tutor> Tutors { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("OnDemandTutor"));

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(name: "Users");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("user_id");
                entity.Property(e => e.FullName)
                      .HasMaxLength(150)
                      .HasColumnName("full_name");
                entity.Property(e => e.IdentityCard)
                      .HasColumnName("identity_card");
                entity.Property(e => e.Gender)
                      .HasColumnName("gender");
                entity.Property(e => e.Dob)
                       .HasColumnName("dob");
                entity.Property(e => e.CreatedDate)
                      .HasDefaultValueSql("getutcdate()")
                      .HasColumnName("created_date");
                entity.Property(e => e.Status)
                      .HasColumnName("status");
                entity.Property(e => e.Email)
                      .HasMaxLength(256)
                      .HasColumnName("email");
                entity.Property(e => e.UserName)
                      .HasMaxLength(100)
                      .HasColumnName("user_name");
                entity.Property(e => e.PasswordHash)
                      .HasColumnName("password");
                entity.Property(e => e.PhoneNumber)
                      .HasMaxLength(10)
                      .HasColumnName("phone");
                entity.Property(e => e.Avatar)
                      .HasMaxLength(100)
                      .HasColumnName("avatar");

            });



            modelBuilder.Entity<IdentityUserRole<string>>(entity => 
            { entity.ToTable(name: "UserRoles");
              entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.RoleId).HasColumnName("role_id");
            });


            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Roles");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("role_id");
                entity.Property(e => e.Name)
                      .HasMaxLength(100)
                      .HasColumnName("role_name");
            });

            //auto add role for DB
            SeedRoles(modelBuilder);

            modelBuilder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable(name: "UserClaim"); });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable(name: "UserLogin"); });
            modelBuilder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable(name: "UserToken"); });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable(name: "RoleClaim"); });

            modelBuilder.Entity<Tutor>(entity =>
            {
                entity.ToTable(name: "Tutors");
                entity.HasKey(e => e.TutorId);
                entity.Property(e => e.TutorId).HasColumnName("tutor_id");
                entity.Property(e => e.AcademicLevel)
                      .HasMaxLength(200)
                      .HasColumnName("academic_level");
                entity.Property(e => e.WorkPlace)
                      .HasMaxLength(200)
                      .HasColumnName("work_place");
                entity.Property(e => e.OnlineStatus)
                      .HasMaxLength(50)
                      .HasColumnName("online_status");
                entity.Property(e => e.AverageStar)
                      .HasMaxLength(10)
                      .HasColumnName("average_star");
                entity.Property(e => e.Degree)
                      .HasMaxLength(200)
                      .HasColumnName("degree");
                entity.Property(e => e.CreditCard)
                      .HasMaxLength(200)
                      .HasColumnName("credit_car");
                entity.Property(e => e.UserId).HasMaxLength(450).HasColumnName("user_id");

            });
        }

        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData
                (
                    new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN"},
                    new IdentityRole() { Name = "Moderator", ConcurrencyStamp = "2", NormalizedName = "MODERATOR" },
                    new IdentityRole() { Name = "Tutor", ConcurrencyStamp = "3", NormalizedName = "TUTOR" },
                    new IdentityRole() { Name = "Student", ConcurrencyStamp = "4", NormalizedName = "STUDENT" }
                );
        }
    }
}
