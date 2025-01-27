using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSpecialConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SpecialConfigurations",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[,]
                {
                    { 1, "تصویر صفحه اول شماره 1", "../../Writer/ViewFile?id=1" },
                    { 2, "تصویر صفحه اول شماره 2", "../../Writer/ViewFile?id=2" },
                    { 3, "تصویر صفحه اول شماره 3", "../../Writer/ViewFile?id=3" },
                    { 4, "اسم وبلاگ", "وبلاگ من" },
                    { 5, "متن وسط بالا", "به وبلاگ من خوش آمدید" },
                    { 6, "توضیحات متن وسط بالا زیر نوشته", "محلی برای اشتراک افکار، ایده ها و داستانها." },
                    { 7, "عنوان تصویر 1", "عنوان تصویر 1" },
                    { 8, "عنوان تصویر 2", "عنوان تصویر 2" },
                    { 9, "عنوان تصویر 3", "عنوان تصویر 3" },
                    { 10, "توضیح تصویر 1", "توضیح تصویر 1" },
                    { 11, "توضیح تصویر 2", "توضیح تصویر 2" },
                    { 12, "توضیح تصویر 3", "توضیح تصویر 3" },
                    { 13, "لینک تصویر1", "#" },
                    { 14, "لینک تصویر2", "#" },
                    { 15, "لینک تصویر3", "#" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SpecialConfigurations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SpecialConfigurations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SpecialConfigurations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SpecialConfigurations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SpecialConfigurations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SpecialConfigurations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SpecialConfigurations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SpecialConfigurations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SpecialConfigurations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SpecialConfigurations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SpecialConfigurations",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "SpecialConfigurations",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "SpecialConfigurations",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "SpecialConfigurations",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "SpecialConfigurations",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
