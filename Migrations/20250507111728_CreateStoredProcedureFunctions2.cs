using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFirst.Vehicle.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class CreateStoredProcedureFunctions2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var uspAddVehicleProducts = @"
                CREATE PROCEDURE usp_AddVehicleProducts
                @ProductId NVARCHAR(450),
                @ProductName NVARCHAR(20),
                @ProductDescription NVARCHAR(30),
                @CategoryId INT,
                @ProductPrice NUMERIC(8, 0)
                AS
                BEGIN
                    DECLARE @CategoryExists INT
                    Begin try 
                       Select @productId=ProductId from VehicleProducts where ProductId=@ProductId
                    if @productId is not null
                    Begin
                        Set @CategoryExists = -1
                        return @CategoryExists
                    end
                    else
                    Begin
                        INSERT INTO VehicleProducts (ProductId, ProductName,ProductDescription,ProductPrice,CategoryId)
                        VALUES (@ProductId, @ProductName,@ProductDescription,@ProductPrice,@CategoryId) 
                        Set @CategoryExists = 1
                        return @CategoryExists
                    end
                    End Try
                    Begin Catch
                        Set @CategoryExists = -99
                        return @CategoryExists
                    End Catch
                END
                go";
            migrationBuilder.Sql(uspAddVehicleProducts);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
