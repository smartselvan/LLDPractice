using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLDProjectExecutor.ParkingLot
{
    public class ParkingSpot
    {
        private readonly VehicleType _vehicleType;
        private readonly int _number;
        private Vehicle? _parkedVehicle = null;

        private readonly object _spotLock = new();
        public ParkingSpot(VehicleType vehicleType, int number) { 
            _vehicleType = vehicleType;
            _number = number;
        }

        public bool ParkVehicle(Vehicle vehicle)
        {
            lock (_spotLock)
            {
                if (_parkedVehicle != null)
                {
                    return false; // Parking spot already occupied
                }

                _parkedVehicle = vehicle;
                return true;
            }
        }

        public bool UnParkVehicle()
        {
            lock (_spotLock)
            {
                if (_parkedVehicle == null)
                {
                    return false; // No vehicle to unpark
                }

                _parkedVehicle = null;
                return true;
            }
        }

        public bool IsAvailable()
        {
            return _parkedVehicle == null;
        }

        public int SpotNumber => _number;

        public VehicleType VehicleType => _vehicleType;

        public Vehicle? ParkedVehicle => _parkedVehicle;

    }
}
