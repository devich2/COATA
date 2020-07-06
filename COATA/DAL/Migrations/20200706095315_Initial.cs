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
                    { 6, "Села" }
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
                    { 4, "РАЙОНИ ДНІПРОПЕТРОВСЬКОЇ ОБЛАСТІ", 2 },
                    { 7, "МІСТА РАЙОННОГО ПІДПОРЯДКУВАННЯ БАРСЬКОГО Р-НУ", 3 },
                    { 6, "МІСТА ОБЛАСНОГО ПІДПОРЯДКУВАННЯ ВОЛИНСЬКОЇ ОБЛАСТІ", 3 },
                    { 5, "МІСТА ОБЛАСНОГО ПІДПОРЯДКУВАННЯ ВІННИЦЬКОЇ ОБЛАСТІ", 3 },
                    { 2, "РАЙОНИ ВІННИЦЬКОЇ ОБЛАСТІ", 2 },
                    { 8, "МІСТА РАЙОННОГО ПІДПОРЯДКУВАННЯ БЕРШАДСЬКОГО Р-НУ", 3 },
                    { 3, "РАЙОНИ ВОЛИНСЬКОЇ ОБЛАСТІ", 2 }
                });

            migrationBuilder.InsertData(
                table: "UnitTypeHierarchy",
                columns: new[] { "UnitTypeId", "ParentUnitTypeId" },
                values: new object[,]
                {
                    { 6, 2 },
                    { 5, 4 },
                    { 5, 2 },
                    { 3, 1 },
                    { 4, 2 },
                    { 2, 1 },
                    { 6, 3 },
                    { 2, 3 },
                    { 3, 2 },
                    { 4, 3 },
                    { 5, 6 }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Lft", "Name", "ParentId", "Rgt", "UnitClassificationId" },
                values: new object[,]
                {
                    { 2, 1, "ВІННИЦЬКА ОБЛАСТЬ/М.ВІННИЦЯ", null, 48, 1 },
                    { 3, 49, "ВОЛИНСЬКА ОБЛАСТЬ/М.ЛУЦЬК", null, 72, 1 },
                    { 4, 73, "ДНІПРОПЕТРОВСЬКА ОБЛАСТЬ/М.ДНІПРО", null, 94, 1 },
                    { 5, 95, "ДОНЕЦЬКА ОБЛАСТЬ/М.ДОНЕЦЬК", null, 96, 1 },
                    { 6, 97, "ЖИТОМИРСЬКА ОБЛАСТЬ/М.ЖИТОМИР", null, 98, 1 },
                    { 7, 99, "ЗАКАРПАТСЬКА ОБЛАСТЬ/М.УЖГОРОД", null, 100, 1 }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Lft", "Name", "ParentId", "Rgt", "UnitClassificationId" },
                values: new object[,]
                {
                    { 8, 2, "БАРСЬКИЙ РАЙОН/М.БАР", 2, 11, 2 },
                    { 19, 84, "ВЕРХНЬОДНІПРОВСЬКИЙ РАЙОН/М.ВЕРХНЬОДНІПРОВСЬК", 4, 89, 4 },
                    { 18, 76, "ВАСИЛЬКІВСЬКИЙ РАЙОН/СМТ ВАСИЛЬКІВКА", 4, 83, 4 },
                    { 17, 74, "АПОСТОЛІВСЬКИЙ РАЙОН/М.АПОСТОЛОВЕ", 4, 75, 4 },
                    { 30, 70, "НОВОВОЛИНСЬК", 3, 71, 6 },
                    { 29, 68, "КОВЕЛЬ", 3, 69, 6 },
                    { 28, 66, "ВОЛОДИМИР-ВОЛИНСЬКИЙ", 3, 67, 6 },
                    { 27, 64, "ЛУЦЬК", 3, 65, 6 },
                    { 16, 60, "ІВАНИЧІВСЬКИЙ РАЙОН/СМТ ІВАНИЧІ", 3, 63, 3 },
                    { 15, 54, "ГОРОХІВСЬКИЙ РАЙОН/М.ГОРОХІВ", 3, 59, 3 },
                    { 20, 90, "ДНІПРОВСЬКИЙ РАЙОН/М.ДНІПРО", 4, 91, 4 },
                    { 14, 52, "ВОЛОДИМИР-ВОЛИНСЬКИЙ РАЙОН/М.ВОЛОДИМИР-ВОЛИНСЬКИЙ", 3, 53, 3 },
                    { 26, 46, "ХМІЛЬНИК", 2, 47, 5 },
                    { 25, 44, "ЛАДИЖИН", 2, 45, 5 },
                    { 24, 42, "КОЗЯТИН", 2, 43, 5 },
                    { 23, 40, "МОГИЛІВ-ПОДІЛЬСЬКИЙ", 2, 41, 5 },
                    { 22, 38, "ВІННИЦЯ", 2, 39, 5 },
                    { 12, 34, "ЖМЕРИНСЬКИЙ РАЙОН/М.ЖМЕРИНКА", 2, 37, 2 },
                    { 11, 32, "ГАЙСИНСЬКИЙ РАЙОН/М.ГАЙСИН", 2, 33, 2 },
                    { 10, 24, "ВІННИЦЬКИЙ РАЙОН/М.ВІННИЦЯ", 2, 31, 2 },
                    { 9, 12, "БЕРШАДСЬКИЙ РАЙОН/М.БЕРШАДЬ", 2, 23, 2 },
                    { 13, 50, "ВОЛОДИМИР-ВОЛИНСЬКИЙ РАЙОН/М.ВОЛОДИМИР-ВОЛИНСЬКИЙ", 3, 51, 3 },
                    { 21, 92, "КРИВОРІЗЬКИЙ РАЙОН/М.КРИВИЙ РІГ", 4, 93, 4 }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Lft", "Name", "ParentId", "Rgt", "UnitClassificationId" },
                values: new object[,]
                {
                    { 31, 3, "БАР", 8, 4, 7 },
                    { 112, 81, "ЧАПЛИНЕ", 18, 82, 20 },
                    { 111, 79, "ПИСЬМЕННЕ", 18, 80, 20 },
                    { 110, 77, "ВАСИЛЬКІВКА", 18, 78, 20 },
                    { 100, 61, "ІВАНИЧІ", 16, 62, 17 },
                    { 96, 57, "СЕНКЕВИЧІВКА", 15, 58, 15 },
                    { 95, 55, "МАР'ЯНІВКА", 15, 56, 15 },
                    { 85, 35, "БРАЇЛІВ", 12, 36, 12 },
                    { 82, 29, "СТРИЖАВКА", 10, 30, 11 },
                    { 120, 85, "НОВОМИКОЛАЇВКА", 19, 86, 25 },
                    { 81, 27, "ДЕСНА", 10, 28, 11 },
                    { 63, 21, "ГОЛДАШІВСЬКА/С.ГОЛДАШІВКА", 9, 22, 10 },
                    { 62, 19, "ВЕЛИКОКИРІЇВСЬКА/С.ВЕЛИКА КИРІЇВКА", 9, 20, 10 },
                    { 61, 17, "БИРЛІВСЬКА C", 9, 18, 10 },
                    { 60, 15, "БАЛАНІВСЬКА C", 9, 16, 10 },
                    { 32, 13, "БЕРШАДЬ", 9, 14, 8 },
                    { 52, 9, "ВЕРХІВСЬКА/С.ВЕРХІВКА", 8, 10, 9 },
                    { 51, 7, "БАЛКІВСЬКА/С.БАЛКИ", 8, 8, 9 },
                    { 50, 5, "АНТОНІВИІВСЬКА/С.АНТОНІВКА", 8, 6, 9 },
                    { 80, 25, "ВОРОНОВИЦЯ", 10, 26, 11 },
                    { 121, 87, "ДНІПРОВСЬКЕ", 19, 88, 25 }
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
