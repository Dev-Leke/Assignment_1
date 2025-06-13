using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment_1
{
    public class Driver
    {
        static List<Appliance> appliances = new List<Appliance>();

        static void Main(string[] args)
        {
            LoadAppliances();

            

            bool running = true;

            while (running)
            {
                //Console.Clear();
                Console.WriteLine("Welcome to Modern Appliances!");
                Console.WriteLine("How may we assist you?");
                Console.WriteLine("1 – Check out appliance");
                Console.WriteLine("2 – Find appliances by brand");
                Console.WriteLine("3 – Display appliances by type");
                Console.WriteLine("4 – Produce random appliance list");
                Console.WriteLine("5 – Save & exit");
                Console.WriteLine("Enter option: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CheckoutAppliance();
                        break;
                    case "2":
                        FindByBrand();
                        break;
                    case "3":
                        DisplayByApplianceType();
                        break;
                    case "4":
                        DisplayRandomAppliances();
                        break;
                    case "5":
                        SaveAppliancesToFile();
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press Enter to continue.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        public static void LoadAppliances()
        {
            string filePath = "..\\..\\Res\\appliances.txt";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return;
            }

            string[] lines = File.ReadAllLines(filePath);
            Console.WriteLine("Reading from file...");

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] fields = line.Split(new[] { ';' });
                for (int i = 0; i < fields.Length; i++) fields[i] = fields[i].Trim();

                char applianceType = fields[0][0];

                try
                {
                    long itemNumber = long.Parse(fields[0]);
                    string brand = fields[1];
                    int quantity = int.Parse(fields[2]);
                    double wattage = double.Parse(fields[3]);
                    string color = fields[4];
                    double price = double.Parse(fields[5]);

                    switch (applianceType)
                    {
                        case '1':
                            if (fields.Length >= 9)
                            {
                                appliances.Add(new Refrigerator(
                                    itemNumber,
                                    brand,
                                    quantity,
                                    wattage,
                                    color,
                                    price,
                                    int.Parse(fields[6]),
                                    int.Parse(fields[7]),
                                    int.Parse(fields[8]) 
                                    ));
                            }
                            break;
                        case '2':
                            if (fields.Length >= 8) 
                            {
                                appliances.Add(new Vacuum(
                                    itemNumber,
                                    brand,
                                    quantity,
                                    wattage,
                                    color,
                                    price,
                                    fields[6], 
                                    int.Parse(fields[7])

                                    ));

                            }
                            break;
                        case '3':
                            if (fields.Length >= 8)
                                appliances.Add(new Microwave(
                                    itemNumber,
                                    brand,
                                    quantity,
                                    wattage,
                                    color,
                                    price,
                                    double.Parse(fields[6]),
                                    fields[7]
                                    ));
                                break;
                        case '4':
                        case '5':
                            if (fields.Length >= 8)
                                appliances.Add(new Dishwasher(
                                    itemNumber,
                                    brand,
                                    quantity,
                                    wattage,
                                    color,
                                    price,
                                    fields[6],
                                    fields[7]
                                    ));

                                break;
                        default:
                            Console.WriteLine($"Unknown appliance type: {line}");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading line:\n{line}\nReason: {ex.Message}");
                }
            }

            
        }

        public static void CheckoutAppliance()
        {
            Console.WriteLine("Enter item number of an Appliance:");
            string input = Console.ReadLine();

            if (long.TryParse(input, out long itemNumber))
            {
                Appliance found = appliances.Find(a => a.ItemNumber == itemNumber);
                if (found == null)
                {
                    Console.WriteLine("No appliances found with that item number.");
                }
                else if (!found.isAvailable())
                {
                    Console.WriteLine("The appliance is not available to be checked out.");
                }
                else
                {
                    found.checkout();
                    Console.WriteLine($"Appliance \"{itemNumber}\" has been checked out.");
                }
            }
            else
            {
                Console.WriteLine("Invalid item number format.");
            }

            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }

        public static void FindByBrand()
        {
            Console.WriteLine("Enter brand to search for: ");
            string brand = Console.ReadLine().Trim().ToLower();

            var matches = appliances
                .Where(a => a.Brand.ToLower() == brand)
                .ToList();

            if (matches.Count == 0)
            {
                Console.WriteLine("No matching appliances found.");
            }
            else
            {
                Console.WriteLine("\nMatching Appliances:");
                foreach (var appliance in matches)
                {
                    Console.WriteLine(appliance.ToString());
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }

        public static void DisplayByApplianceType()
        {
            Console.WriteLine("\nAppliance Types");
            Console.WriteLine("1 – Refrigerators");
            Console.WriteLine("2 – Vacuums");
            Console.WriteLine("3 – Microwaves");
            Console.WriteLine("4 – Dishwashers");
            Console.WriteLine("Enter type of appliance: ");
            string typeInput = Console.ReadLine();

            switch (typeInput)
            {
                case "1":
                    Console.WriteLine("\nEnter number of doors: 2 (double door), 3 (three doors) or 4 (four doors): ");
                    if (int.TryParse(Console.ReadLine(), out int doors))
                    {
                        var refrigerators = appliances
                            .OfType<Refrigerator>()
                            .Where(r => r.NumberOfDoors == doors)
                            .ToList();

                        if (refrigerators.Count == 0)
                            Console.WriteLine("No matching refrigerators found.");
                        else
                            foreach (var r in refrigerators)
                            {  
                                Console.WriteLine(r.ToString());
                                Console.WriteLine();
                            }
                        
                    }
                    break;
                case "2":
                    Console.WriteLine("\nEnter battery voltage value (18 for Low, 24 for High):");
                    if (int.TryParse(Console.ReadLine(), out int voltage))
                    {
                        var vacuums = appliances
                            .OfType<Vacuum>()
                            .Where(v => v.BatteryVoltage == voltage)
                            .ToList();

                        if (vacuums.Count == 0)
                            Console.WriteLine("No matching vacuums found.");
                        else
                            foreach (var v in vacuums)
                            {
                                Console.WriteLine(v.ToString());
                                Console.WriteLine();
                            }
                    }
                    break;
                case "3":
                    Console.WriteLine("\nEnter room type where microwave will be installed (K for Kitchen or W for Work Site):");
                    string roomInput = Console.ReadLine().Trim().ToLower();

                    var microwaves = appliances
                        .OfType<Microwave>()
                        .Where(m => m.RoomType.ToLower().StartsWith(roomInput))
                        .ToList();

                    if (microwaves.Count == 0)
                        Console.WriteLine("No matching microwaves found.");
                    else
                        foreach (var m in microwaves)
                        {
                            Console.WriteLine(m.ToString());
                            Console.WriteLine();
                        }
                    break;
                case "4":
                    Console.WriteLine("Enter the sound rating of the dishwasher: Qt (Quietest), Qr (Quieter), Qu(Quiet) or M (Moderate):");
                    string soundRating = Console.ReadLine().Trim().ToUpper();
                    var dishwashers = appliances
                        .OfType<Dishwasher>()
                        .Where(d => d.SoundRating.ToUpper() == soundRating)
                        .ToList();
                    if (dishwashers.Count == 0)
                        Console.WriteLine("No matching dishwashers found.");
                    else
                        foreach (var d in dishwashers)
                        {
                            Console.WriteLine(d.ToString());
                            Console.WriteLine();
                        }
                    break;

                default:
                    Console.WriteLine("Feature for this type is not yet implemented.");
                    break;
            }

            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }

        public static void DisplayRandomAppliances()
        {
            Console.WriteLine("Enter number of appliances to display: ");
            if (int.TryParse(Console.ReadLine(), out int count))
            {
                if (count <= 0)
                {
                    Console.WriteLine("Please enter a positive number.");
                }
                else
                {
                    var random = new Random();
                    var selected = appliances.OrderBy(a => random.Next()).Take(count).ToList();

                    if (selected.Count == 0)
                    {
                        Console.WriteLine("No appliances available.");
                    }
                    else
                    {
                        Console.WriteLine("\nRandom Appliances:");
                        foreach (var appliance in selected)
                        {
                            Console.WriteLine(appliance.ToString());
                            Console.WriteLine();
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }

            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }

        public static void SaveAppliancesToFile()
        {
            string filePath = "..\\..\\Res\\appliances.txt";
            List<string> outputLines = new List<string>();

            Console.WriteLine("Saving data to file...");

            foreach (var appliance in appliances)
            {
                string line = appliance.formatForfile(); // Convert appliance to line
                outputLines.Add(line); // Collect lines
            }

            // Overwrite file with updated list
            File.WriteAllLines(filePath, outputLines);

            Console.WriteLine("Data saved successfully.");
            Console.WriteLine("Press Enter to return to menu.");
            Console.ReadLine();
        }

    }
}
