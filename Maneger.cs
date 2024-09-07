using System;

namespace BookingSystem{
    public class Maneger
    {
        public string? Name{ get; set; }
        public double? ManegerNumber { get; set; }
        public Maneger(string name, string passengerID, double number)
        {
            Name = name.Length > 0? name : throw new ArgumentException("name can not be empty");
            ManegerNumber = number > 0? number : throw new ArgumentException("passport number >0");
        }

    }
}