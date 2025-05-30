﻿using Microsoft.EntityFrameworkCore.Migrations;

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
