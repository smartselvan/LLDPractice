using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLDProjectExecutor.ParkingLot
{
    public class MotorCycle : Vehicle
    {
        public MotorCycle(string licensePlate) : base(licensePlate, VehicleType.MotorCycle)
        {
        }
    }
}
