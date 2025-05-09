using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeFirst.Vehicle.DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace CodeFirst.Vehicle.DataAccessLayer
{
    public class VehicleRepository
    {
        public VehicleDBContext VehicleDBContext { get; set; }
        public VehicleRepository(VehicleDBContext vehicleDBContext)
        {
            this.VehicleDBContext = vehicleDBContext;
        }
        public bool InsertVehicalcategory(VehicleCategory vehicleCategory)
        {
            try
            {
                VehicleDBContext.VehicleCategories.Add(vehicleCategory);
                VehicleDBContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool InsertVehicleProduct(VehicleProduct vehicleProduct)
        {
            try
            {
                VehicleDBContext.VehicleProducts.Add(vehicleProduct);
                VehicleDBContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<VehicleCategory> GetAllVehicleCategories()
        {
            var vehiclecatlist = VehicleDBContext.VehicleCategories.ToList();
            if (vehiclecatlist.Count == 0)
            {
                return null;
            }
            else
            {
                return vehiclecatlist;
            }
        }
        public List<VehicleProduct> GetAllVehicleProducts()
        {
            return VehicleDBContext.VehicleProducts.ToList();
        }
        public int addvehicleUsingSP(VehicleProduct vehicleProduct)
        {

            try
            { //Constructor pattern for input parameter

                SqlParameter PID = new SqlParameter("@ProductId", vehicleProduct.ProductId);
                SqlParameter PNAme= new SqlParameter("@ProductName", vehicleProduct.ProductName);
                SqlParameter Pdesc= new SqlParameter("@ProductDescription", vehicleProduct.ProductDescription);
                SqlParameter PPrice= new SqlParameter("@ProductPrice", vehicleProduct.ProductPrice);
                SqlParameter PCategory =new SqlParameter("@CategoryId", vehicleProduct.CategoryId);
                SqlParameter PStatus = new SqlParameter("@ReturnResult",System.Data.SqlDbType.Int);
                PStatus.Direction = System.Data.ParameterDirection.Output;
                var result = VehicleDBContext.Database.ExecuteSqlRaw("exec @ReturnResult=usp_AddVehicleProducts @ProductId,@ProductName,@ProductDescription,@CategoryId,@ProductPrice", PStatus, PID, PNAme, Pdesc, PCategory, PPrice);
                var returnResult=Convert.ToInt32(result);
                Console.WriteLine("Return value from SP:{0}", returnResult);
                return returnResult;

            }
            catch (Exception ex)
            {
                return -1;
            }

        }
        public List<VehicleCategory> GetVehicleCategoryByTableValued(byte productId)
        {
            try
            {
                SqlParameter PCategoryId = new SqlParameter("@ProductId", productId);
                var result = VehicleDBContext.VehicleCategories.FromSqlRaw("select * from ufn_vehicleproductCategory(@ProductId)", PCategoryId).ToList();
                if (result.Count == 0)
                {
                    return new List<VehicleCategory>(); // Return an empty list instead of null
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
                return new List<VehicleCategory>(); // Return an empty list instead of null
            }
        }
        public byte GetCatIDbyFunction(byte categoryId)
        {
            try
            {
                var catid = (from i in VehicleDBContext.VehicleCategories
                             select VehicleDBContext.GetCategoryIDfunction(categoryId)).FirstOrDefault();
                Console.WriteLine("category id from function is {0}", catid);
                return catid;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
                return 0; // Return a default value of 0 for byte instead of -1
            }
        }
    }
}
