using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
    public class Microwaves : Appliance
    {
        private double capacity;
        private string roomType;

        public double Capacity { get => capacity; set => capacity = value; }
        public string RoomType { get => roomType; set => roomType = value; }

        public Microwaves()
        {
            
        }

        public Microwaves(long itemNumber, string brand, int quantity, double wattage, string color, double price, double capacity, string roomType)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            this.capacity = capacity;
            this.roomType = roomType;
        }

        public override void checkout()
        {
            if (isAvailable())
            {
                Quantity -= 1;
            }
        }

        public override string formatForfile()
        {
            return $"{ItemNumber};{Brand};{Quantity};{Wattage};{Color};{Price};{Capacity};{RoomType}";
        }

        public override bool isAvailable()
        {
            return Quantity > 0;
        }
    }
}
