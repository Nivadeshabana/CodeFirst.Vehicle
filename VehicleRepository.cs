using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeFirst.Vehicle.DataAccessLayer.Models;


namespace CodeFirst.Vehicle.DataAccessLayer
{
    public class VehicleRepository 
    {
        public VehicleDBContext VehicleDBContext { get; set; }
        public VehicleRepository( VehicleDBContext vehicleDBContext) { 
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
            var vehiclecatlist= VehicleDBContext.VehicleCategories.ToList();
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
    }
}
