using DAL.Migrations.StoreProcedures;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class StoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(Resource.UpdateBowers);
            migrationBuilder.Sql(Resource.DeleteNode);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
