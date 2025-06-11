using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
    public class Refrigerator : Appliance
    {
        private int numberOfDoors;
        private int height;
        private int width;

        public int NumberOfDoors { get => numberOfDoors; set => numberOfDoors = value; }
        public int Height { get => height; set => height = value; }
        public int Width { get => width; set => width = value; }

        public Refrigerator()
        {
            
        }

        public Refrigerator(long itemNumber, string brand, int quantity, double wattage, string color, double price, int numberOfDoors, int height, int width)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            this.numberOfDoors = numberOfDoors;
            this.height = height;
            this.width = width;
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
            return $"{ItemNumber};{Brand};{Quantity};{Wattage};{Color};{Price};{NumberOfDoors};{Height};{Width}";
        }

        public override bool isAvailable()
        {
            return Quantity > 0;
        }

        private string GetDoorType()
        {
            switch(numberOfDoors)
            {
                case 2:
                    return "Double doors";
                case 3:
                    return "Three doors";
                case 4:
                    return "Four doors";
                default:
                    return "Unknown Door Type";
            }
        }

       

        public override string ToString()
        {
            return $"Item Number: {ItemNumber}\n" +
                   $"Brand: {Brand}\n" +
                   $"Quantity: {Quantity}\n" +
                   $"Wattage: {Wattage}\n" +
                   $"Color: {Color}\n" +
                   $"Price: {Price}\n" +
                   $"Number of Doors: {GetDoorType()}\n" +
                   $"Height: {Height}\n" +
                   $"Width: {Width}";
        }
    }
}
