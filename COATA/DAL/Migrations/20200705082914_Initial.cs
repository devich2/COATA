using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnitTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitClassifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitClassifications_UnitTypes_UnitTypeId",
                        column: x => x.UnitTypeId,
                        principalTable: "UnitTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitTypeHierarchy",
                columns: table => new
                {
                    UnitTypeId = table.Column<int>(type: "int", nullable: false),
                    ParentUnitTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitTypeHierarchy", x => new { x.UnitTypeId, x.ParentUnitTypeId });
                    table.ForeignKey(
                        name: "FK_UnitTypeHierarchy_UnitTypes_ParentUnitTypeId",
                        column: x => x.ParentUnitTypeId,
                        principalTable: "UnitTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnitTypeHierarchy_UnitTypes_UnitTypeId",
                        column: x => x.UnitTypeId,
                        principalTable: "UnitTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    UnitClassificationId = table.Column<int>(type: "int", nullable: false),
                    Lft = table.Column<int>(type: "int", nullable: false),
                    Rgt = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_UnitClassifications_UnitClassificationId",
                        column: x => x.UnitClassificationId,
                        principalTable: "UnitClassifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Units_Units_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "UnitTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Області" },
                    { 2, "Райони" },
                    { 3, "Міста" },
                    { 4, "Селища" },
                    { 5, "Сільради" },
                    { 6, "Села" },
                    { 7, "root" }
                });

            migrationBuilder.InsertData(
                table: "UnitClassifications",
                columns: new[] { "Id", "Name", "UnitTypeId" },
                values: new object[,]
                {
                    { 1, "Області", 1 },
                    { 10, "СІЛЬРАДИ БЕРШАДСЬКОГО Р-НУ", 5 },
                    { 9, "СІЛЬРАДИ БАРСЬКОГО Р-НУ", 5 },
                    { 25, "СЕЛИЩА МІСЬКОГО ТИПУ ВЕРХНЬОДНІПРОВСЬКОГО Р-НУ", 4 },
                    { 20, "СЕЛИЩА МІСЬКОГО ТИПУ ВАСИЛЬКІВСЬКОГО Р-НУ", 4 },
                    { 17, "СЕЛИЩА МІСЬКОГО ТИПУ ІВАНИЧІВСЬКОГО Р-НУ", 4 },
                    { 15, "СЕЛИЩА МІСЬКОГО ТИПУ ГОРОХІВСЬКОГО Р-НУ", 4 },
                    { 11, "СЕЛИЩА МІСЬКОГО ТИПУ ВІННИЦЬКОГО Р-НУ", 4 },
                    { 12, "СЕЛИЩА МІСЬКОГО ТИПУ ЖМЕРИНСЬКОГО Р-НУ", 4 },
                    { 35, "ROOT", 7 },
                    { 7, "МІСТА РАЙОННОГО ПІДПОРЯДКУВАННЯ БАРСЬКОГО Р-НУ", 3 },
                    { 6, "МІСТА ОБЛАСНОГО ПІДПОРЯДКУВАННЯ ВОЛИНСЬКОЇ ОБЛАСТІ", 3 },
                    { 5, "МІСТА ОБЛАСНОГО ПІДПОРЯДКУВАННЯ ВІННИЦЬКОЇ ОБЛАСТІ", 3 },
                    { 4, "РАЙОНИ ДНІПРОПЕТРОВСЬКОЇ ОБЛАСТІ", 2 },
                    { 8, "МІСТА РАЙОННОГО ПІДПОРЯДКУВАННЯ БЕРШАДСЬКОГО Р-НУ", 3 },
                    { 2, "РАЙОНИ ВІННИЦЬКОЇ ОБЛАСТІ", 2 },
                    { 3, "РАЙОНИ ВОЛИНСЬКОЇ ОБЛАСТІ", 2 }
                });

            migrationBuilder.InsertData(
                table: "UnitTypeHierarchy",
                columns: new[] { "UnitTypeId", "ParentUnitTypeId" },
                values: new object[,]
                {
                    { 6, 3 },
                    { 6, 2 },
                    { 5, 4 },
                    { 5, 2 },
                    { 4, 2 },
                    { 2, 1 },
                    { 5, 6 },
                    { 2, 3 },
                    { 3, 2 },
                    { 4, 3 },
                    { 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Lft", "Name", "ParentId", "Rgt", "UnitClassificationId" },
                values: new object[] { 1, 1, "ROOT", null, 102, 35 });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Lft", "Name", "ParentId", "Rgt", "UnitClassificationId" },
                values: new object[,]
                {
                    { 2, 2, "ВІННИЦЬКА ОБЛАСТЬ/М.ВІННИЦЯ", 1, 49, 1 },
                    { 3, 50, "ВОЛИНСЬКА ОБЛАСТЬ/М.ЛУЦЬК", 1, 73, 1 },
                    { 4, 74, "ДНІПРОПЕТРОВСЬКА ОБЛАСТЬ/М.ДНІПРО", 1, 95, 1 },
                    { 5, 96, "ДОНЕЦЬКА ОБЛАСТЬ/М.ДОНЕЦЬК", 1, 97, 1 },
                    { 6, 98, "ЖИТОМИРСЬКА ОБЛАСТЬ/М.ЖИТОМИР", 1, 99, 1 },
                    { 7, 100, "ЗАКАРПАТСЬКА ОБЛАСТЬ/М.УЖГОРОД", 1, 101, 1 }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Lft", "Name", "ParentId", "Rgt", "UnitClassificationId" },
                values: new object[,]
                {
                    { 8, 3, "БАРСЬКИЙ РАЙОН/М.БАР", 2, 12, 2 },
                    { 19, 85, "ВЕРХНЬОДНІПРОВСЬКИЙ РАЙОН/М.ВЕРХНЬОДНІПРОВСЬК", 4, 90, 4 },
                    { 18, 77, "ВАСИЛЬКІВСЬКИЙ РАЙОН/СМТ ВАСИЛЬКІВКА", 4, 84, 4 },
                    { 17, 75, "АПОСТОЛІВСЬКИЙ РАЙОН/М.АПОСТОЛОВЕ", 4, 76, 4 },
                    { 30, 71, "НОВОВОЛИНСЬК", 3, 72, 6 },
                    { 29, 69, "КОВЕЛЬ", 3, 70, 6 },
                    { 28, 67, "ВОЛОДИМИР-ВОЛИНСЬКИЙ", 3, 68, 6 },
                    { 27, 65, "ЛУЦЬК", 3, 66, 6 },
                    { 16, 61, "ІВАНИЧІВСЬКИЙ РАЙОН/СМТ ІВАНИЧІ", 3, 64, 3 },
                    { 15, 55, "ГОРОХІВСЬКИЙ РАЙОН/М.ГОРОХІВ", 3, 60, 3 },
                    { 20, 91, "ДНІПРОВСЬКИЙ РАЙОН/М.ДНІПРО", 4, 92, 4 },
                    { 14, 53, "ВОЛОДИМИР-ВОЛИНСЬКИЙ РАЙОН/М.ВОЛОДИМИР-ВОЛИНСЬКИЙ", 3, 54, 3 },
                    { 26, 47, "ХМІЛЬНИК", 2, 48, 5 },
                    { 25, 45, "ЛАДИЖИН", 2, 46, 5 },
                    { 24, 43, "КОЗЯТИН", 2, 44, 5 },
                    { 23, 41, "МОГИЛІВ-ПОДІЛЬСЬКИЙ", 2, 42, 5 },
                    { 22, 39, "ВІННИЦЯ", 2, 40, 5 },
                    { 12, 35, "ЖМЕРИНСЬКИЙ РАЙОН/М.ЖМЕРИНКА", 2, 38, 2 },
                    { 11, 33, "ГАЙСИНСЬКИЙ РАЙОН/М.ГАЙСИН", 2, 34, 2 },
                    { 10, 25, "ВІННИЦЬКИЙ РАЙОН/М.ВІННИЦЯ", 2, 32, 2 },
                    { 9, 13, "БЕРШАДСЬКИЙ РАЙОН/М.БЕРШАДЬ", 2, 24, 2 },
                    { 13, 51, "ВОЛОДИМИР-ВОЛИНСЬКИЙ РАЙОН/М.ВОЛОДИМИР-ВОЛИНСЬКИЙ", 3, 52, 3 },
                    { 21, 93, "КРИВОРІЗЬКИЙ РАЙОН/М.КРИВИЙ РІГ", 4, 94, 4 }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Lft", "Name", "ParentId", "Rgt", "UnitClassificationId" },
                values: new object[,]
                {
                    { 31, 4, "БАР", 8, 5, 7 },
                    { 112, 82, "ЧАПЛИНЕ", 18, 83, 20 },
                    { 111, 80, "ПИСЬМЕННЕ", 18, 81, 20 },
                    { 110, 78, "ВАСИЛЬКІВКА", 18, 79, 20 },
                    { 100, 62, "ІВАНИЧІ", 16, 63, 17 },
                    { 96, 58, "СЕНКЕВИЧІВКА", 15, 59, 15 },
                    { 95, 56, "МАР'ЯНІВКА", 15, 57, 15 },
                    { 85, 36, "БРАЇЛІВ", 12, 37, 12 },
                    { 82, 30, "СТРИЖАВКА", 10, 31, 11 },
                    { 120, 86, "НОВОМИКОЛАЇВКА", 19, 87, 25 },
                    { 81, 28, "ДЕСНА", 10, 29, 11 },
                    { 63, 22, "ГОЛДАШІВСЬКА/С.ГОЛДАШІВКА", 9, 23, 10 },
                    { 62, 20, "ВЕЛИКОКИРІЇВСЬКА/С.ВЕЛИКА КИРІЇВКА", 9, 21, 10 },
                    { 61, 18, "БИРЛІВСЬКА C", 9, 19, 10 },
                    { 60, 16, "БАЛАНІВСЬКА C", 9, 17, 10 },
                    { 32, 14, "БЕРШАДЬ", 9, 15, 8 },
                    { 52, 10, "ВЕРХІВСЬКА/С.ВЕРХІВКА", 8, 11, 9 },
                    { 51, 8, "БАЛКІВСЬКА/С.БАЛКИ", 8, 9, 9 },
                    { 50, 6, "АНТОНІВИІВСЬКА/С.АНТОНІВКА", 8, 7, 9 },
                    { 80, 26, "ВОРОНОВИЦЯ", 10, 27, 11 },
                    { 121, 88, "ДНІПРОВСЬКЕ", 19, 89, 25 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitClassifications_UnitTypeId",
                table: "UnitClassifications",
                column: "UnitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_ParentId",
                table: "Units",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_UnitClassificationId",
                table: "Units",
                column: "UnitClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitTypeHierarchy_ParentUnitTypeId",
                table: "UnitTypeHierarchy",
                column: "ParentUnitTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "UnitTypeHierarchy");

            migrationBuilder.DropTable(
                name: "UnitClassifications");

            migrationBuilder.DropTable(
                name: "UnitTypes");
        }
    }
}
