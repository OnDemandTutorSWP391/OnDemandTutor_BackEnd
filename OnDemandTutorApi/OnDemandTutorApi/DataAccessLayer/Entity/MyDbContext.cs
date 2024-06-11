using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnDemandTutorApi.DataAccessLayer.Entity
{
    public partial class MyDbContext : IdentityDbContext<User>
    {
        public MyDbContext()
        {
            
        }

        public MyDbContext(DbContextOptions<MyDbContext> option) : base(option) 
        {
            
        }

        #region DbSet
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<IdentityUserToken<string>> UserTokens { get; set; } = null!;
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public virtual DbSet<Tutor> Tutors { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<RequestCategory> RequestCategories { get; set; } = null!;
        public virtual DbSet<Response> Responses { get; set; } = null!;
        public virtual DbSet<Level> Levels { get; set; } = null!;
        public virtual DbSet<SubjectLevel> SubjectLevels { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<Time> Times { get; set; } = null!;
        public virtual DbSet<Rating> Ratings { get; set; } = null!;
        public virtual DbSet<StudentJoin> StudentJoins { get; set; } = null!;
        public virtual DbSet<CoinManagement> CoinManagements { get; set; } = null!;
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("OnDemandTutor"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<User>(entity => { entity.ToTable(name: "User"); });
            modelBuilder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable(name: "UserRoles"); });
            modelBuilder.Entity<IdentityRole>(entity => { entity.ToTable(name: "Role"); });

            //auto add role for DB
            //SeedRoles(modelBuilder);

            modelBuilder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable(name: "UserClaim"); });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable(name: "UserLogin"); });
            modelBuilder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable(name: "UserToken"); });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable(name: "RoleClaim"); });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable(name: "RefreshToken");
                entity.HasOne(r => r.User)
                      .WithMany(u => u.RefreshTokens)
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_RefreshToken_User");
            });

            modelBuilder.Entity<Tutor>(entity =>
            {
                entity.ToTable(name: "Tutor");
                entity.HasOne(t => t.User)
                      .WithOne(u => u.Tutor)
                      .HasForeignKey<Tutor>(t => t.UserId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Tutor_User");
            });

            modelBuilder.Entity<CoinManagement>(entity =>
            {
                entity.ToTable(name: "CoinManagement");
                entity.HasOne(c => c.User)
                      .WithMany(u => u.CoinManagements)
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_CoinManagement_User");
            });

            modelBuilder.Entity<RequestCategory>(entity =>
            {
                entity.ToTable(name: "RequestCategory");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable(name: "Request");
                entity.HasOne(r => r.User)
                      .WithMany(u => u.Requests)
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Request_User");

                entity.HasOne(r => r.Category)
                      .WithMany(rc => rc.Requests)
                      .HasForeignKey(r => r.RequestCategoryId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Request_RequestCategory");
            });

            modelBuilder.Entity<Response>(entity =>
            {
                entity.ToTable(name: "Response");
                entity.HasOne(rs => rs.Request)
                      .WithOne(r => r.Response)
                      .HasForeignKey<Response>(rs => rs.RequestId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Response_Request");
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.ToTable(name: "Level");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable(name: "Subject");
            });

            modelBuilder.Entity<SubjectLevel>(entity =>
            {
                entity.ToTable(name: "SubjectLevel");
                entity.HasOne(sl => sl.Level)
                      .WithMany(l => l.SubjectLevels)
                      .HasForeignKey(sl => sl.LevelId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_SubjectLevel_Level");

                entity.HasOne(sl => sl.Subject)
                      .WithMany(s => s.SubjectLevels)
                      .HasForeignKey(sl => sl.SubjectId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_SubjectLevel_Subject");

                entity.HasOne(sl => sl.Tutor)
                      .WithMany(t => t.SubjectLevels)
                      .HasForeignKey(sl => sl.TutorId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_SubjectLevel_Tutor");
            });


            modelBuilder.Entity<StudentJoin>(entity =>
            {
                entity.ToTable(name: "StudentJoin");
                entity.HasOne(sj => sj.User)
                      .WithMany(u => u.StudentJoins)
                      .HasForeignKey(sj => sj.UserId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_StudentJoin_User");

                entity.HasOne(sj => sj.SubjectLevel)
                      .WithMany(sl => sl.StudentJoins)
                      .HasForeignKey(sj => sj.SubjectLevelId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_StudentJoin_SubjectLevel");

            });

            modelBuilder.Entity<Time>(entity =>
            {
                entity.ToTable(name: "Time");
                entity.HasOne(t => t.SubjectLevel)
                      .WithMany(sj => sj.Times)
                      .HasForeignKey(t => t.SubjectLevelId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Time_SubjectLevel");

            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable(name: "Rating");
                entity.HasOne(r => r.User)
                      .WithMany(u => u.Ratings)
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Rating_User");

                entity.HasOne(r => r.Tutor)
                      .WithMany(t => t.Ratings)
                      .HasForeignKey(r => r.TutorId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Rating_Tutor");

            });

            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        //private static void SeedRoles(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<IdentityRole>().HasData
        //        (
        //            new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN"},
        //            new IdentityRole() { Name = "Moderator", ConcurrencyStamp = "2", NormalizedName = "MODERATOR" },
        //            new IdentityRole() { Name = "Tutor", ConcurrencyStamp = "3", NormalizedName = "TUTOR" },
        //            new IdentityRole() { Name = "Student", ConcurrencyStamp = "4", NormalizedName = "STUDENT" }
        //        );
        //}
    }
}
