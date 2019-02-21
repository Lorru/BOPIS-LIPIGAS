using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace bopis_api.Models.Bopis
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Configuration> Configuration { get; set; }
        public virtual DbSet<Cylinder> Cylinder { get; set; }
        public virtual DbSet<CylinderByLocal> CylinderByLocal { get; set; }
        public virtual DbSet<Local> Local { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<ProfileRole> ProfileRole { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<ScheduleOfAttention> ScheduleOfAttention { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<TypeLog> TypeLog { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Week> Week { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot iConfigurationRoot = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();

                string connectionString = iConfigurationRoot.GetConnectionString("ModelContext");

                optionsBuilder.UseOracle(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Configuration>(entity =>
            {
                entity.ToTable("Configuration", "BOPIS");

                entity.HasIndex(e => e.Id)
                    .HasName("SYS_C007110")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DataType)
                    .IsRequired()
                    .HasColumnName("dataType")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasColumnName("key")
                    .HasColumnType("varchar2")
                    .HasMaxLength(500);

                entity.Property(e => e.ReadOnly).HasColumnName("readOnly");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value")
                    .HasColumnType("varchar2")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Cylinder>(entity =>
            {
                entity.ToTable("Cylinder", "BOPIS");

                entity.HasIndex(e => e.Id)
                    .HasName("SYS_C007116")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasColumnName("image")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.Kg).HasColumnName("kg");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<CylinderByLocal>(entity =>
            {
                entity.ToTable("CylinderByLocal", "BOPIS");

                entity.HasIndex(e => e.Id)
                    .HasName("SYS_C007132")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CylinderId).HasColumnName("cylinderId");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.FinalPrice).HasColumnName("finalPrice");

                entity.Property(e => e.LocalId).HasColumnName("localId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.ZonePrice).HasColumnName("zonePrice");

                entity.HasOne(d => d.Cylinder)
                    .WithMany(p => p.CylinderByLocal)
                    .HasForeignKey(d => d.CylinderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C007134");

                entity.HasOne(d => d.Local)
                    .WithMany(p => p.CylinderByLocal)
                    .HasForeignKey(d => d.LocalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C007133");
            });

            modelBuilder.Entity<Local>(entity =>
            {
                entity.ToTable("Local", "BOPIS");

                entity.HasIndex(e => e.Id)
                    .HasName("SYS_C007124")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasColumnType("float");

                entity.Property(e => e.Length)
                    .HasColumnName("length")
                    .HasColumnType("float");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.Open).HasColumnName("open");

                entity.Property(e => e.Radio)
                    .HasColumnName("radio")
                    .HasColumnType("float");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("Log", "BOPIS");

                entity.HasIndex(e => e.Id)
                    .HasName("SYS_C007163")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Controller)
                    .IsRequired()
                    .HasColumnName("controller")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar2")
                    .HasMaxLength(500);

                entity.Property(e => e.Method)
                    .IsRequired()
                    .HasColumnName("method")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.TypeLogId).HasColumnName("typeLogId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.TypeLog)
                    .WithMany(p => p.Log)
                    .HasForeignKey(d => d.TypeLogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C007164");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Log)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("SYS_C007165");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order", "BOPIS");

                entity.HasIndex(e => e.Id)
                    .HasName("SYS_C007203")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClientAddress)
                    .IsRequired()
                    .HasColumnName("clientAddress")
                    .HasColumnType("varchar2")
                    .HasMaxLength(500);

                entity.Property(e => e.ClientFullName)
                    .IsRequired()
                    .HasColumnName("clientFullName")
                    .HasColumnType("varchar2")
                    .HasMaxLength(500);

                entity.Property(e => e.ClientRun)
                    .IsRequired()
                    .HasColumnName("clientRun")
                    .HasColumnType("varchar2")
                    .HasMaxLength(30);

                entity.Property(e => e.CylinderByLocalId).HasColumnName("cylinderByLocalId");

                entity.Property(e => e.DateOfDelivery)
                    .HasColumnName("dateOfDelivery")
                    .HasColumnType("date");

                entity.Property(e => e.DateOfRequest)
                    .HasColumnName("dateOfRequest")
                    .HasColumnType("date");

                entity.Property(e => e.OrderStatusId).HasColumnName("orderStatusId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.ScheduleOfRetirement)
                    .HasColumnName("scheduleOfRetirement")
                    .HasColumnType("varchar2")
                    .HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.CylinderByLocal)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CylinderByLocalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C007205");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.OrderStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C007204");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C007206");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("OrderStatus", "BOPIS");

                entity.HasIndex(e => e.Id)
                    .HasName("SYS_C007175")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profile", "BOPIS");

                entity.HasIndex(e => e.Id)
                    .HasName("SYS_C007146")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<ProfileRole>(entity =>
            {
                entity.ToTable("ProfileRole", "BOPIS");

                entity.HasIndex(e => e.Id)
                    .HasName("SYS_C007169")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ProfileId).HasColumnName("profileId");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.ProfileRole)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C007170");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ProfileRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C007171");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role", "BOPIS");

                entity.HasIndex(e => e.Id)
                    .HasName("SYS_C007142")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<ScheduleOfAttention>(entity =>
            {
                entity.ToTable("ScheduleOfAttention", "BOPIS");

                entity.HasIndex(e => e.Id)
                    .HasName("SYS_C007191")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Finish)
                    .IsRequired()
                    .HasColumnName("finish")
                    .HasColumnType("varchar2")
                    .HasMaxLength(5);

                entity.Property(e => e.LocalId).HasColumnName("localId");

                entity.Property(e => e.Start)
                    .IsRequired()
                    .HasColumnName("start")
                    .HasColumnType("varchar2")
                    .HasMaxLength(5);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.WeekId).HasColumnName("weekId");

                entity.HasOne(d => d.Local)
                    .WithMany(p => p.ScheduleOfAttention)
                    .HasForeignKey(d => d.LocalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C007192");

                entity.HasOne(d => d.Week)
                    .WithMany(p => p.ScheduleOfAttention)
                    .HasForeignKey(d => d.WeekId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C007193");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("Stock", "BOPIS");

                entity.HasIndex(e => e.Id)
                    .HasName("SYS_C007184")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CylinderByLocalId).HasColumnName("cylinderByLocalId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.CylinderByLocal)
                    .WithMany(p => p.Stock)
                    .HasForeignKey(d => d.CylinderByLocalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C007185");
            });

            modelBuilder.Entity<TypeLog>(entity =>
            {
                entity.ToTable("TypeLog", "BOPIS");

                entity.HasIndex(e => e.Id)
                    .HasName("SYS_C007138")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "BOPIS");

                entity.HasIndex(e => e.Id)
                    .HasName("SYS_C007154")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnName("fullName")
                    .HasColumnType("varchar2")
                    .HasMaxLength(500);

                entity.Property(e => e.LocalId).HasColumnName("localId");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar2")
                    .HasMaxLength(500);

                entity.Property(e => e.ProfileId).HasColumnName("profileId");

                entity.Property(e => e.Run)
                    .IsRequired()
                    .HasColumnName("run")
                    .HasColumnType("varchar2")
                    .HasMaxLength(30);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Local)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.LocalId)
                    .HasConstraintName("SYS_C007156");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C007155");
            });

            modelBuilder.Entity<Week>(entity =>
            {
                entity.ToTable("Week", "BOPIS");

                entity.HasIndex(e => e.Id)
                    .HasName("SYS_C007179")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DayOfWeek)
                    .IsRequired()
                    .HasColumnName("dayOfWeek")
                    .HasColumnType("varchar2")
                    .HasMaxLength(20);

                entity.Property(e => e.Status).HasColumnName("status");
            });
        }
    }
}
