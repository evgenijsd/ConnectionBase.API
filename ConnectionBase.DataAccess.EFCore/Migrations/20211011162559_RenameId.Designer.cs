﻿// <auto-generated />
using System;
using ConnectionBase.DataAccess.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConnectionBase.DataAccess.EFCore.Migrations
{
    [DbContext(typeof(ConnectionBaseContext))]
    [Migration("20211011162559_RenameId")]
    partial class RenameId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ConnectionBase.Domain.Entities.Building", b =>
                {
                    b.Property<int>("BuildingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BuildingID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BuildingName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("BuildingId");

                    b.ToTable("Building");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.Cross", b =>
                {
                    b.Property<int>("CrossId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CrossID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Ats")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("ATS")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("BeginNum")
                        .HasColumnType("int");

                    b.Property<string>("CrossName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("NumberPair")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((10))");

                    b.Property<int?>("Room")
                        .HasColumnType("int");

                    b.HasKey("CrossId");

                    b.HasIndex("Room");

                    b.ToTable("Cross");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.Depart", b =>
                {
                    b.Property<int>("DepartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DepartID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DepartName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("DepartId");

                    b.ToTable("Depart");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.Device", b =>
                {
                    b.Property<int>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DeviceID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("InvNum")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Model")
                        .HasColumnType("int");

                    b.Property<int?>("Pair")
                        .HasColumnType("int");

                    b.Property<int?>("Room")
                        .HasColumnType("int");

                    b.HasKey("DeviceId");

                    b.HasIndex("Model");

                    b.HasIndex("Pair");

                    b.HasIndex("Room");

                    b.ToTable("Device");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.DeviceModel", b =>
                {
                    b.Property<int>("ModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ModelID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ModelId")
                        .HasName("DeviceModel$PrimaryKey");

                    b.ToTable("DeviceModel");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.DevicePerson", b =>
                {
                    b.Property<int>("DevicePersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("DevicePersonID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Device")
                        .HasColumnType("int");

                    b.Property<int?>("Person")
                        .HasColumnType("int");

                    b.HasKey("DevicePersonId");

                    b.HasIndex("Device");

                    b.HasIndex("Person");

                    b.ToTable("DevicePerson");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.NumberIn", b =>
                {
                    b.Property<int>("NumberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("NumberID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Number_In")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("NumberIN");

                    b.Property<int?>("PairAts")
                        .HasColumnType("int")
                        .HasColumnName("PairATS");

                    b.HasKey("NumberId")
                        .HasName("NumberIN$PrimaryKey");

                    b.HasIndex("PairAts");

                    b.ToTable("NumberIN");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.NumberOut", b =>
                {
                    b.Property<int>("NumberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("NumberID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Number_Out")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("NumberOUT");

                    b.Property<int?>("Operator")
                        .HasColumnType("int");

                    b.Property<int?>("PairAts")
                        .HasColumnType("int")
                        .HasColumnName("PairATS");

                    b.HasKey("NumberId")
                        .HasName("NumberOUT$PrimaryKey");

                    b.HasIndex("Operator");

                    b.HasIndex("PairAts");

                    b.ToTable("NumberOUT");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.Operator", b =>
                {
                    b.Property<int>("OperatorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OperatorID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("OperatorName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("OperatorId");

                    b.ToTable("Operator");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.Pair", b =>
                {
                    b.Property<int>("PairId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ParaID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("BreakIn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("BreakIN")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("Cross")
                        .HasColumnType("int");

                    b.Property<bool?>("PairAb")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("ConnectionAB")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("PairIn")
                        .HasColumnType("int")
                        .HasColumnName("PairIN");

                    b.Property<int>("PairNum")
                        .HasColumnType("int");

                    b.HasKey("PairId")
                        .HasName("Para$PrimaryKey");

                    b.HasIndex("Cross");

                    b.ToTable("Pair");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.PairAb", b =>
                {
                    b.Property<int>("AbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("abID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("BreakIn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("BreakIN")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("Pair")
                        .HasColumnType("int");

                    b.Property<int?>("PairIn")
                        .HasColumnType("int")
                        .HasColumnName("PairIN");

                    b.HasKey("AbId");

                    b.HasIndex("Pair");

                    b.ToTable("PairAB");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PersonID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Depart")
                        .HasColumnType("int");

                    b.Property<string>("PersonName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Position")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("PersonId");

                    b.HasIndex("Depart");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RoomID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Building")
                        .HasColumnType("int");

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RoomId");

                    b.HasIndex("Building");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.Cross", b =>
                {
                    b.HasOne("ConnectionBase.Domain.Entities.Room", "RoomNavigation")
                        .WithMany("Crosses")
                        .HasForeignKey("Room")
                        .HasConstraintName("FK_Cross_Room");

                    b.Navigation("RoomNavigation");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.Device", b =>
                {
                    b.HasOne("ConnectionBase.Domain.Entities.DeviceModel", "ModelNavigation")
                        .WithMany("Devices")
                        .HasForeignKey("Model")
                        .HasConstraintName("FK_Device_DeviceModel")
                        .IsRequired();

                    b.HasOne("ConnectionBase.Domain.Entities.Pair", "PairNavigation")
                        .WithMany("Devices")
                        .HasForeignKey("Pair")
                        .HasConstraintName("FK_Device_Pair");

                    b.HasOne("ConnectionBase.Domain.Entities.Room", "RoomNavigation")
                        .WithMany("Devices")
                        .HasForeignKey("Room")
                        .HasConstraintName("FK_Device_Room");

                    b.Navigation("ModelNavigation");

                    b.Navigation("PairNavigation");

                    b.Navigation("RoomNavigation");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.DevicePerson", b =>
                {
                    b.HasOne("ConnectionBase.Domain.Entities.Device", "DeviceNavigation")
                        .WithMany("DevicePeople")
                        .HasForeignKey("Device")
                        .HasConstraintName("FK_DevicePerson_Device")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConnectionBase.Domain.Entities.Person", "PersonNavigation")
                        .WithMany("DevicePeople")
                        .HasForeignKey("Person")
                        .HasConstraintName("FK_DevicePerson_Person")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("DeviceNavigation");

                    b.Navigation("PersonNavigation");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.NumberIn", b =>
                {
                    b.HasOne("ConnectionBase.Domain.Entities.Pair", "PairAtsNavigation")
                        .WithMany("NumberIns")
                        .HasForeignKey("PairAts")
                        .HasConstraintName("FK_NumberIN_Pair");

                    b.Navigation("PairAtsNavigation");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.NumberOut", b =>
                {
                    b.HasOne("ConnectionBase.Domain.Entities.Operator", "OperatorNavigation")
                        .WithMany("NumberOuts")
                        .HasForeignKey("Operator")
                        .HasConstraintName("FK_NumberOUT_Operator");

                    b.HasOne("ConnectionBase.Domain.Entities.Pair", "PairAtsNavigation")
                        .WithMany("NumberOuts")
                        .HasForeignKey("PairAts")
                        .HasConstraintName("FK_NumberOUT_Pair");

                    b.Navigation("OperatorNavigation");

                    b.Navigation("PairAtsNavigation");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.Pair", b =>
                {
                    b.HasOne("ConnectionBase.Domain.Entities.Cross", "CrossNavigation")
                        .WithMany("Pairs")
                        .HasForeignKey("Cross")
                        .HasConstraintName("FK_Pair_Cross")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("CrossNavigation");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.PairAb", b =>
                {
                    b.HasOne("ConnectionBase.Domain.Entities.Pair", "PairNavigation")
                        .WithMany("PairAbs")
                        .HasForeignKey("Pair")
                        .HasConstraintName("FK_PairAB_Pair")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PairNavigation");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.Person", b =>
                {
                    b.HasOne("ConnectionBase.Domain.Entities.Depart", "DepartNavigation")
                        .WithMany("People")
                        .HasForeignKey("Depart")
                        .HasConstraintName("FK_Person_Depart");

                    b.Navigation("DepartNavigation");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.Room", b =>
                {
                    b.HasOne("ConnectionBase.Domain.Entities.Building", "BuildingNavigation")
                        .WithMany("Rooms")
                        .HasForeignKey("Building")
                        .HasConstraintName("FK_Room_Building")
                        .IsRequired();

                    b.Navigation("BuildingNavigation");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.Building", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.Cross", b =>
                {
                    b.Navigation("Pairs");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.Depart", b =>
                {
                    b.Navigation("People");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.Device", b =>
                {
                    b.Navigation("DevicePeople");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.DeviceModel", b =>
                {
                    b.Navigation("Devices");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.Operator", b =>
                {
                    b.Navigation("NumberOuts");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.Pair", b =>
                {
                    b.Navigation("Devices");

                    b.Navigation("NumberIns");

                    b.Navigation("NumberOuts");

                    b.Navigation("PairAbs");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.Person", b =>
                {
                    b.Navigation("DevicePeople");
                });

            modelBuilder.Entity("ConnectionBase.Domain.Entities.Room", b =>
                {
                    b.Navigation("Crosses");

                    b.Navigation("Devices");
                });
#pragma warning restore 612, 618
        }
    }
}