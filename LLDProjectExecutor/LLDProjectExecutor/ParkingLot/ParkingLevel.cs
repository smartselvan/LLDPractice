using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LLDProjectExecutor.ParkingLot
{
    public class ParkingLevel
    {
        private List<ParkingSpot> _spots;
        private readonly int _level;
        private const int DEFAULT_SPOT_CAPACITY = 100;

        public ParkingLevel(int level, Dictionary<VehicleType, double> slotRatio) : this(level, DEFAULT_SPOT_CAPACITY, slotRatio) {}

        public ParkingLevel(int level, int spotsCapacity, Dictionary<VehicleType, double> slotRatio)
        {
            _spots = new List<ParkingSpot>(spotsCapacity);
            _level = level;

            InitializeSpots(spotsCapacity, slotRatio);
        }

        public bool UpdateParkingSpotCapacity(int newCapacity, Dictionary<VehicleType, double> slotRatio)
        {
            if (GetOccupiedSpots().Count == 0)
            {
                InitializeSpots(newCapacity, slotRatio);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void InitializeSpots(int spotsCapacity, Dictionary<VehicleType, double> slotRatio)
        {
            _spots = new List<ParkingSpot>(spotsCapacity);

            int spotNumber = 0;
            foreach (var ratio in slotRatio) { 
                int vehicleSlotRatio = (int)(spotsCapacity * ratio.Value);
                spotNumber = AddSpots(ratio.Key, vehicleSlotRatio, spotNumber+1);
            }
        }

        private int AddSpots(VehicleType type, int count, int startId)
        {
            int spotsAdded = 0;
            for (int i = 0; i < count; i++)
            {
                _spots.Add(new ParkingSpot(type, startId + i));
                spotsAdded = startId + i;
            }
            return spotsAdded;
        }

        public ParkingSpot? ParkVehicle(Vehicle vehicle)
        {
            ParkingSpot parkingSpot = GetAvailableSpots(vehicle.VehicleType).First();
            if (parkingSpot.IsAvailable())
            {
                parkingSpot.ParkVehicle(vehicle);
                return parkingSpot;
            }
            else
            {
                return null;
            }
        }

        public void UnParkVehicle(ParkingSpot parkingSpot)
        {
            if (!parkingSpot.IsAvailable())
            {
                parkingSpot.UnParkVehicle();
            }
        }

        public IReadOnlyList<ParkingSpot> GetAvailableSpots(VehicleType vehicleType)
        {
            return _spots.Where(s => s.IsAvailable() && s.VehicleType == vehicleType).ToList();
        }

        public IReadOnlyList<ParkingSpot> GetOccupiedSpots()
        {
            return _spots.Where(s => !s.IsAvailable()).ToList();
        }

        public int GetAvailableSpotsCount(VehicleType vehicleType)
        {
            return _spots.Where(s => s.IsAvailable() && s.VehicleType == vehicleType).Count();
        }

        public int GetLevel()
        {
            return _level;
        }
    }
}
