using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zity.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataFromSqlFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Đọc file SQL từ thư mục Scripts
            var sqlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../Data", "init_data.sql");
            var sqlScript = File.ReadAllText(sqlFilePath);

            // Thực thi câu lệnh SQL từ file
            migrationBuilder.Sql(sqlScript);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
