﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLDProjectExecutor.ParkingLot
{
    public class Car : Vehicle
    {
        public Car(string licensePlate) : base(licensePlate, VehicleType.Car) { }
    }
}
