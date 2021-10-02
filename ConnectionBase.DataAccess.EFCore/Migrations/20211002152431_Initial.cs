using Microsoft.EntityFrameworkCore.Migrations;

namespace ConnectionBase.DataAccess.EFCore.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    BuildingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.BuildingID);
                });

            migrationBuilder.CreateTable(
                name: "Depart",
                columns: table => new
                {
                    DepartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depart", x => x.DepartID);
                });

            migrationBuilder.CreateTable(
                name: "DeviceModel",
                columns: table => new
                {
                    ModelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("DeviceModel$PrimaryKey", x => x.ModelID);
                });

            migrationBuilder.CreateTable(
                name: "Operator",
                columns: table => new
                {
                    OperatorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperatorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operator", x => x.OperatorID);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Building = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_Room_Building",
                        column: x => x.Building,
                        principalTable: "Building",
                        principalColumn: "BuildingID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Depart = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonID);
                    table.ForeignKey(
                        name: "FK_Person_Depart",
                        column: x => x.Depart,
                        principalTable: "Depart",
                        principalColumn: "DepartID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cross",
                columns: table => new
                {
                    CrossID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CrossName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BeginNum = table.Column<int>(type: "int", nullable: false),
                    NumberPair = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((10))"),
                    ATS = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    Room = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cross", x => x.CrossID);
                    table.ForeignKey(
                        name: "FK_Cross_Room",
                        column: x => x.Room,
                        principalTable: "Room",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pair",
                columns: table => new
                {
                    ParaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cross = table.Column<int>(type: "int", nullable: true),
                    PairNum = table.Column<int>(type: "int", nullable: false),
                    PairIN = table.Column<int>(type: "int", nullable: true),
                    BreakIN = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    ConnectionAB = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Para$PrimaryKey", x => x.ParaID);
                    table.ForeignKey(
                        name: "FK_Pair_Cross",
                        column: x => x.Cross,
                        principalTable: "Cross",
                        principalColumn: "CrossID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    DeviceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<int>(type: "int", nullable: false),
                    Room = table.Column<int>(type: "int", nullable: true),
                    Pair = table.Column<int>(type: "int", nullable: true),
                    InvNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.DeviceID);
                    table.ForeignKey(
                        name: "FK_Device_DeviceModel",
                        column: x => x.Model,
                        principalTable: "DeviceModel",
                        principalColumn: "ModelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Device_Pair",
                        column: x => x.Pair,
                        principalTable: "Pair",
                        principalColumn: "ParaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Device_Room",
                        column: x => x.Room,
                        principalTable: "Room",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NumberIN",
                columns: table => new
                {
                    NumberID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberIN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PairATS = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("NumberIN$PrimaryKey", x => x.NumberID);
                    table.ForeignKey(
                        name: "FK_NumberIN_Pair",
                        column: x => x.PairATS,
                        principalTable: "Pair",
                        principalColumn: "ParaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NumberOUT",
                columns: table => new
                {
                    NumberID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOUT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PairATS = table.Column<int>(type: "int", nullable: true),
                    Operator = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("NumberOUT$PrimaryKey", x => x.NumberID);
                    table.ForeignKey(
                        name: "FK_NumberOUT_Operator",
                        column: x => x.Operator,
                        principalTable: "Operator",
                        principalColumn: "OperatorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NumberOUT_Pair",
                        column: x => x.PairATS,
                        principalTable: "Pair",
                        principalColumn: "ParaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PairAB",
                columns: table => new
                {
                    abID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pair = table.Column<int>(type: "int", nullable: false),
                    PairIN = table.Column<int>(type: "int", nullable: true),
                    BreakIN = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PairAB", x => x.abID);
                    table.ForeignKey(
                        name: "FK_PairAB_Pair",
                        column: x => x.Pair,
                        principalTable: "Pair",
                        principalColumn: "ParaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DevicePerson",
                columns: table => new
                {
                    DevicePersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Device = table.Column<int>(type: "int", nullable: false),
                    Person = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevicePerson", x => x.DevicePersonID);
                    table.ForeignKey(
                        name: "FK_DevicePerson_Device",
                        column: x => x.Device,
                        principalTable: "Device",
                        principalColumn: "DeviceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DevicePerson_Person",
                        column: x => x.Person,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cross_Room",
                table: "Cross",
                column: "Room");

            migrationBuilder.CreateIndex(
                name: "IX_Device_Model",
                table: "Device",
                column: "Model");

            migrationBuilder.CreateIndex(
                name: "IX_Device_Pair",
                table: "Device",
                column: "Pair");

            migrationBuilder.CreateIndex(
                name: "IX_Device_Room",
                table: "Device",
                column: "Room");

            migrationBuilder.CreateIndex(
                name: "IX_DevicePerson_Device",
                table: "DevicePerson",
                column: "Device");

            migrationBuilder.CreateIndex(
                name: "IX_DevicePerson_Person",
                table: "DevicePerson",
                column: "Person");

            migrationBuilder.CreateIndex(
                name: "IX_NumberIN_PairATS",
                table: "NumberIN",
                column: "PairATS");

            migrationBuilder.CreateIndex(
                name: "IX_NumberOUT_Operator",
                table: "NumberOUT",
                column: "Operator");

            migrationBuilder.CreateIndex(
                name: "IX_NumberOUT_PairATS",
                table: "NumberOUT",
                column: "PairATS");

            migrationBuilder.CreateIndex(
                name: "IX_Pair_Cross",
                table: "Pair",
                column: "Cross");

            migrationBuilder.CreateIndex(
                name: "IX_PairAB_Pair",
                table: "PairAB",
                column: "Pair");

            migrationBuilder.CreateIndex(
                name: "IX_Person_Depart",
                table: "Person",
                column: "Depart");

            migrationBuilder.CreateIndex(
                name: "IX_Room_Building",
                table: "Room",
                column: "Building");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DevicePerson");

            migrationBuilder.DropTable(
                name: "NumberIN");

            migrationBuilder.DropTable(
                name: "NumberOUT");

            migrationBuilder.DropTable(
                name: "PairAB");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Operator");

            migrationBuilder.DropTable(
                name: "DeviceModel");

            migrationBuilder.DropTable(
                name: "Pair");

            migrationBuilder.DropTable(
                name: "Depart");

            migrationBuilder.DropTable(
                name: "Cross");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Building");
        }
    }
}
