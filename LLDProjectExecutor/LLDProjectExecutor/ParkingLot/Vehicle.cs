using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLDProjectExecutor.ParkingLot
{
    public abstract class Vehicle
    {
        protected string licensePlate;
        protected VehicleType type;

        public Vehicle(string licensePlate, VehicleType type)
        {
            this.licensePlate = licensePlate;
            this.type = type;
        }
        public VehicleType VehicleType => type;

        public string LicensePlate => licensePlate;
    }
}
