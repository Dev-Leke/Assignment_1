using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
    public class Vacuum : Appliance
    {

        private string grade;
        private int batteryVoltage;

        public string Grade { get => grade; set => grade = value; }
        public int BatteryVoltage { get => batteryVoltage; set => batteryVoltage = value; }

        public Vacuum()
        {
            
        }

        public Vacuum(long itemNumber, string brand, int quantity, double wattage, string color, double price, string grade, int batteryVoltage)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            this.grade = grade;
            this.batteryVoltage = batteryVoltage;
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
            return $"{ItemNumber};{Brand};{Quantity};{Wattage};{Color};{Price};{Grade};{BatteryVoltage}";
        }

        public override bool isAvailable()
        {
            return Quantity > 0;
        }

        private string GetBatteryVoltageLevel()
        {
            switch(batteryVoltage)
            {
                case 18:
                    return "Low";
                case 24:
                    return "High";
                default:
                    return $"{batteryVoltage}V";
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
                   $"Grade: {Grade}\n" +
                   $"Battery voltage: {GetBatteryVoltageLevel()}";
        }
    }
}
