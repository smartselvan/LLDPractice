using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLDProjectExecutor.ParkingLot
{
    public class ParkingLotDesign
    {
        public static void Run()
        {
            try
            {
                Dictionary<VehicleType, double> parkingRates = new Dictionary<VehicleType, double>();
                parkingRates.Add(VehicleType.Car, 50.00);
                parkingRates.Add(VehicleType.MotorCycle, 20.00);
                parkingRates.Add(VehicleType.Truck, 100.00);

                Dictionary<VehicleType, double> parkingSlotRatio = new Dictionary<VehicleType, double>();
                parkingSlotRatio.Add(VehicleType.Car, 0.40);
                parkingSlotRatio.Add(VehicleType.MotorCycle, 0.50);
                parkingSlotRatio.Add(VehicleType.Truck, 0.10);

                ParkingLot parkingLot = new ParkingLot(2, parkingRates, parkingSlotRatio);
                
                parkingLot.DisplayAvailabilityDetails();
                parkingLot.UpdateSlotCount(2, 500, parkingSlotRatio);
                parkingLot.DisplayAvailabilityDetails();

                Car car = new Car("TN37DE3022");
                Ticket carTicket = parkingLot.ParkVehicle(car);
                parkingLot.UpdateSlotCount(1, 500, parkingSlotRatio);
                parkingLot.DisplayAvailabilityDetails();
                Thread.Sleep(1000);
                parkingLot.UnParkVehicle(carTicket);
                double carParkingRate = parkingLot.CalculateParkingFee(carTicket);
                Console.WriteLine($"You have to pay: {carParkingRate} for your {car.VehicleType} {car.LicensePlate}");
                Console.WriteLine("___________________________________");

                MotorCycle bike = new MotorCycle("TN39DE3022");
                Ticket bikeTicket = parkingLot.ParkVehicle(bike);
                parkingLot.DisplayAvailabilityDetails();
                Thread.Sleep(1000);
                parkingLot.UnParkVehicle(bikeTicket);
                double bikeParkingRate = parkingLot.CalculateParkingFee(bikeTicket);
                Console.WriteLine($"You have to pay: {bikeParkingRate} for your {bike.VehicleType} {bike.LicensePlate}");
                Console.WriteLine("___________________________________");

                Truck truck = new Truck("TN39BE3022");
                Ticket truckTicket = parkingLot.ParkVehicle(truck);
                parkingLot.DisplayAvailabilityDetails();
                Thread.Sleep(1000);
                parkingLot.UnParkVehicle(truckTicket);
                double truckParkingRate = parkingLot.CalculateParkingFee(truckTicket);
                Console.WriteLine($"You have to pay: {truckParkingRate} for your {truck.VehicleType} {truck.LicensePlate}");
                Console.WriteLine("___________________________________");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}
