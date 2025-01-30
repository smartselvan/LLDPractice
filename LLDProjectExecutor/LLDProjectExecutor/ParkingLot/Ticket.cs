using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LLDProjectExecutor.ParkingLot
{
    public class Ticket
    {
        public Vehicle Vehicle { get; init; }
        public ParkingLevel Level { get; init; }
        public ParkingSpot Spot { get; init; }
        public DateTime ParkedTime { get; init; }

        public Ticket(Vehicle vehicle, ParkingLevel level, ParkingSpot spot, DateTime parkedTime) =>
            (Vehicle, Level, Spot, ParkedTime) = (vehicle, level, spot, parkedTime);

    }
}
