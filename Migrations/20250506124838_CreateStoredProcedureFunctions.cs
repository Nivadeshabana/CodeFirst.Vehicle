using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFirst.Vehicle.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class CreateStoredProcedureFunctions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var uspRegisterUser = @"
                CREATE PROCEDURE usp_RegisterUser
                @Name NVARCHAR(50),
                @Email NVARCHAR(50),
                @Password CHAR(10)
                AS
                BEGIN
                    INSERT INTO Users (Name, Email,Password)
                    VALUES (@Name, @Email,@Password)
                END";
            migrationBuilder.Sql(uspRegisterUser);

            var ufnProductDetails = @"
                create function ufn_vehicleproductcategory
                (
                    @ProductId int
                )
            returns table as 
            return
            (
                select Vc.CategoryId, Vc.CategoryName, Vc.CategoryDesciption
                from VehicleCategories as Vc
                where Vc.CategoryId = @ProductId
            )
            Go";
            migrationBuilder.Sql(ufnProductDetails);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
