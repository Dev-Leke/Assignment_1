using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
    public class Dishwasher : Appliance
    {
        private string feature;
        private string soundRating;

        public string Feature { get => feature; set => feature = value; }
        public string SoundRating 
        {
            get => soundRating; 
            
            set
            {
                if (!IsValidSoundRating(value))
                {
                    throw new ArgumentException("Invalid Sound Rating. Allowed values are: Qt, Qr, Qu, M");
                }
                soundRating = value;
            }
        
        }

        public Dishwasher()
        {
        }

        public Dishwasher(long itemNumber, string brand, int quantity, double wattage, string color, double price, string feature, string soundRating)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            this.feature = feature;
            this.soundRating = soundRating;
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
            return $"{ItemNumber};{Brand};{Quantity};{Wattage};{Color};{Price};{Feature};{SoundRating}";
        }

        public override bool isAvailable()
        {
            return Quantity > 0;
        }

        private string GetSoundRatingLabel()
        {
            switch(soundRating)
            {
                case "Qt":
                    return "Quietest";
                case "Qr":
                    return "Quieter";
                case "Qu":
                    return "Quiet";
                case "M":
                    return "Moderate";
                default:
                    return "Unknown";
            }
        }

        private bool IsValidSoundRating(string rating)
        {
            return rating == "Qt" || rating == "Qr" || rating == "Qu" || rating == "M";
        }

        public override string ToString()
        {
            return $"Item Number: {ItemNumber}\n" +
                   $"Brand: {Brand}\n" +
                   $"Quantity: {Quantity}\n" +
                   $"Wattage: {Wattage}\n" +
                   $"Color: {Color}\n" +
                   $"Price: {Price}\n" +
                   $"Feature: {Feature}\n" +
                   $"SoundRating: {GetSoundRatingLabel()}";
        }
    }
}
    