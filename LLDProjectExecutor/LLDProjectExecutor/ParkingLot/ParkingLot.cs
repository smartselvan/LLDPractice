using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LLDProjectExecutor.ParkingLot
{
    public class ParkingLot
    {
        private List<ParkingLevel> _levels;
        private Dictionary<VehicleType, double> _rate;
        public ParkingLot(int parkingLevelCounts, Dictionary<VehicleType, double> rate, Dictionary<VehicleType, double> slotRatio)
        {
            _levels = new List<ParkingLevel>();
            _rate = rate;

            // Initialize levels if required
            for (int i = 1; i <= parkingLevelCounts; i++)
            {
                _levels.Add(new ParkingLevel(i, slotRatio));
            }
        }

        public void AddLevel(ParkingLevel level)
        {
            if (_levels.Find(c => c.GetLevel() == level.GetLevel()) != null) {
                throw new InvalidOperationException($"Level '{level.GetLevel()}' already exists");
            }
            _levels.Add(level);
        }

        public void UpdateSlotCount(int level, int parkingSlotCount, Dictionary<VehicleType, double> slotRatio)
        {
            var parkingLevel = _levels.FirstOrDefault(l => l.GetLevel() == level);
            if (parkingLevel == null)
            {
                throw new InvalidOperationException($"No parking level available with provided data.");
            }
            if(parkingLevel.UpdateParkingSpotCapacity(parkingSlotCount, slotRatio))
            {
                Console.WriteLine($"Parkslots updated on the Level {level}"); 
            }
            else
            {
                Console.WriteLine($"Level {level} cannot be updated, as there are vehicles parked in the level currently.");
            };
        }

        public Ticket ParkVehicle(Vehicle vehicle)
        {
            var level = _levels.FirstOrDefault(l => l.GetAvailableSpotsCount(vehicle.VehicleType) > 0);

            if (level == null)
            {
                throw new InvalidOperationException($"No parking spots available in any levels for this {vehicle.VehicleType}.");
            }
            ParkingSpot? spot = level.ParkVehicle(vehicle);
            if (spot == null)
            {
                throw new InvalidOperationException($"Parking spot already occcupied");
            }
            Console.WriteLine($"{vehicle.VehicleType} {vehicle.LicensePlate} parked successfully.");
            Console.WriteLine();
            DateTime parkedTime = DateTime.Now;
            Console.WriteLine($"Ticket Details: \n Vehicle: {vehicle.LicensePlate} \n Vehicle Type: {vehicle.VehicleType} \n Parked Slot: Level {level.GetLevel()} - Spot { spot.SpotNumber } \n Parked Time: {parkedTime }.");
            Console.WriteLine("___________________________________");
            return new Ticket(vehicle, level, spot, parkedTime);
        }

        public void UnParkVehicle(Ticket ticket)
        {
            ticket.Level?.UnParkVehicle(ticket.Spot);
            Console.WriteLine($"{ticket.Vehicle.VehicleType} {ticket.Vehicle.LicensePlate} unparked successfully."); 
        }

        public double CalculateParkingFee(Ticket ticket)
        {
            double parkedHours = Math.Ceiling((DateTime.Now - ticket.ParkedTime).TotalHours);
            return _rate[ticket.Vehicle.VehicleType] * parkedHours;
        }

        public void DisplayAvailabilityDetails()
        {
            Console.WriteLine("\nAvailability Details:");
            foreach (var level in _levels) {
                Console.WriteLine($"Level {level.GetLevel()}");

                foreach (var vehicleType in Enum.GetValues(typeof(VehicleType)))
                {
                    Console.WriteLine($"Parking Spots available for {vehicleType}: {level.GetAvailableSpotsCount((VehicleType)vehicleType).ToString()}.");
                }
                level.GetAvailableSpotsCount(VehicleType.Car);
            }
            Console.WriteLine("___________________________________");
        }
    }
}
