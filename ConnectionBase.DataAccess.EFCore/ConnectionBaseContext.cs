using ConnectionBase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConnectionBase.DataAccess.EFCore
{
    public partial class ConnectionBaseContext : DbContext
    {
        public ConnectionBaseContext()
        {
        }

        public ConnectionBaseContext(DbContextOptions<ConnectionBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Cross> Crosses { get; set; }
        public virtual DbSet<Depart> Departs { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<DeviceModel> DeviceModels { get; set; }
        public virtual DbSet<DevicePerson> DevicePeople { get; set; }
        public virtual DbSet<NumberIn> NumberIns { get; set; }
        public virtual DbSet<NumberOut> NumberOuts { get; set; }
        public virtual DbSet<Operator> Operators { get; set; }
        public virtual DbSet<Pair> Pairs { get; set; }
        public virtual DbSet<PairAb> PairAbs { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Building>(entity =>
            {
                entity.ToTable("Building");

                entity.Property(e => e.BuildingId).HasColumnName("BuildingID");

                entity.Property(e => e.BuildingName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Cross>(entity =>
            {
                entity.ToTable("Cross");

                entity.Property(e => e.CrossId).HasColumnName("CrossID");

                entity.Property(e => e.Ats)
                    .HasColumnName("ATS")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CrossName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NumberPair).HasDefaultValueSql("((10))");

                entity.HasOne(d => d.RoomNavigation)
                    .WithMany(p => p.Crosses)
                    .HasForeignKey(d => d.Room)
                    .HasConstraintName("FK_Cross_Room");
            });

            modelBuilder.Entity<Depart>(entity =>
            {
                entity.ToTable("Depart");

                entity.Property(e => e.DepartId).HasColumnName("DepartID");

                entity.Property(e => e.DepartName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("Device");

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.InvNum).HasMaxLength(50);

                entity.HasOne(d => d.ModelNavigation)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.Model)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Device_DeviceModel");

                entity.HasOne(d => d.PairNavigation)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.Pair)
                    .HasConstraintName("FK_Device_Pair");

                entity.HasOne(d => d.RoomNavigation)
                    .WithMany(p => p.Devices)
                    .HasForeignKey(d => d.Room)
                    .HasConstraintName("FK_Device_Room");
            });

            modelBuilder.Entity<DeviceModel>(entity =>
            {
                entity.HasKey(e => e.ModelId)
                    .HasName("DeviceModel$PrimaryKey");

                entity.ToTable("DeviceModel");

                entity.Property(e => e.ModelId).HasColumnName("ModelID");

                entity.Property(e => e.ModelName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DevicePerson>(entity =>
            {
                entity.ToTable("DevicePerson");

                entity.Property(e => e.DevicePersonId).HasColumnName("DevicePersonID");

                entity.HasOne(d => d.DeviceNavigation)
                    .WithMany(p => p.DevicePeople)
                    .HasForeignKey(d => d.Device)
                    .HasConstraintName("FK_DevicePerson_Device");

                entity.HasOne(d => d.PersonNavigation)
                    .WithMany(p => p.DevicePeople)
                    .HasForeignKey(d => d.Person)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_DevicePerson_Person");
            });

            modelBuilder.Entity<NumberIn>(entity =>
            {
                entity.HasKey(e => e.NumberId)
                    .HasName("NumberIN$PrimaryKey");

                entity.ToTable("NumberIN");

                entity.Property(e => e.NumberId).HasColumnName("NumberID");

                entity.Property(e => e.NumberIn1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("NumberIN");

                entity.Property(e => e.PairAts).HasColumnName("PairATS");

                entity.HasOne(d => d.PairAtsNavigation)
                    .WithMany(p => p.NumberIns)
                    .HasForeignKey(d => d.PairAts)
                    .HasConstraintName("FK_NumberIN_Pair");
            });

            modelBuilder.Entity<NumberOut>(entity =>
            {
                entity.HasKey(e => e.NumberId)
                    .HasName("NumberOUT$PrimaryKey");

                entity.ToTable("NumberOUT");

                entity.Property(e => e.NumberId).HasColumnName("NumberID");

                entity.Property(e => e.NumberOut1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("NumberOUT");

                entity.Property(e => e.PairAts).HasColumnName("PairATS");

                entity.HasOne(d => d.OperatorNavigation)
                    .WithMany(p => p.NumberOuts)
                    .HasForeignKey(d => d.Operator)
                    .HasConstraintName("FK_NumberOUT_Operator");

                entity.HasOne(d => d.PairAtsNavigation)
                    .WithMany(p => p.NumberOuts)
                    .HasForeignKey(d => d.PairAts)
                    .HasConstraintName("FK_NumberOUT_Pair");
            });

            modelBuilder.Entity<Operator>(entity =>
            {
                entity.ToTable("Operator");

                entity.Property(e => e.OperatorId).HasColumnName("OperatorID");

                entity.Property(e => e.OperatorName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Pair>(entity =>
            {
                entity.HasKey(e => e.ParaId)
                    .HasName("Para$PrimaryKey");

                entity.ToTable("Pair");

                entity.Property(e => e.ParaId).HasColumnName("ParaID");

                entity.Property(e => e.BreakIn)
                    .HasColumnName("BreakIN")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ConnectionAb)
                    .HasColumnName("ConnectionAB")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PairIn).HasColumnName("PairIN");

                entity.HasOne(d => d.CrossNavigation)
                    .WithMany(p => p.Pairs)
                    .HasForeignKey(d => d.Cross)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Pair_Cross");
            });

            modelBuilder.Entity<PairAb>(entity =>
            {
                entity.HasKey(e => e.AbId);

                entity.ToTable("PairAB");

                entity.Property(e => e.AbId).HasColumnName("abID");

                entity.Property(e => e.BreakIn)
                    .HasColumnName("BreakIN")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PairIn).HasColumnName("PairIN");

                entity.HasOne(d => d.PairNavigation)
                    .WithMany(p => p.PairAbs)
                    .HasForeignKey(d => d.Pair)
                    .HasConstraintName("FK_PairAB_Pair");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.PersonName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Position).HasMaxLength(150);

                entity.HasOne(d => d.DepartNavigation)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.Depart)
                    .HasConstraintName("FK_Person_Depart");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.RoomName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.BuildingNavigation)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.Building)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_Building");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
