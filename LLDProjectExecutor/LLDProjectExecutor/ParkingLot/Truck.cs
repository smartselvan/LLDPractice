using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLDProjectExecutor.ParkingLot
{
    public class Truck : Vehicle
    {
        public Truck(string licensePlate) : base(licensePlate, VehicleType.Truck)
        {
        }
    }
}
