namespace _04.SoftUniCoffeeSupplies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class SoftUniCoffeeSupplies
    {
        private const string EndA = "end of info";
        private const string EndB = "end of week";

        private static string delimiterA;
        private static string delimiterB;

        private static readonly Dictionary<string, long> CofeeQuantity = 
            new Dictionary<string, long>();

        private static readonly Dictionary<string, string> PersonCofee = 
            new Dictionary<string, string>();

        private static readonly Dictionary<string, HashSet<string>> CofeeDrinkers = 
            new Dictionary<string, HashSet<string>>();

        public static void Main()
        {
            var delimitersToken = Console.ReadLine().Split().ToArray();
            delimiterA = delimitersToken[0];
            delimiterB = delimitersToken[1];

            var inputLine = Console.ReadLine();
            while (inputLine != EndA)
            {
                if (IsCofeeQuantity(inputLine))
                {
                    var tokens = inputLine
                        .Split(new [] {delimiterB}, StringSplitOptions.RemoveEmptyEntries);

                    var cofeeName = tokens[0];
                    var cofeeQuantityValue = int.Parse(tokens[1]);
                    if (!CofeeQuantity.ContainsKey(cofeeName))
                    {
                        CofeeQuantity[cofeeName] = 0;
                    }

                    CofeeQuantity[cofeeName] += cofeeQuantityValue;
                }
                else
                {
                    var tokens = inputLine
                        .Split(new [] { delimiterA }, StringSplitOptions.RemoveEmptyEntries);

                    var employeeName = tokens[0];
                    var cofeeName = tokens[1];

                    if (!CofeeQuantity.ContainsKey(cofeeName))
                    {
                        CofeeQuantity[cofeeName] = 0;
                    }

                    if (!CofeeDrinkers.ContainsKey(cofeeName))
                    {
                        CofeeDrinkers[cofeeName] = new HashSet<string>();
                    }

                    CofeeDrinkers[cofeeName].Add(employeeName);
                    PersonCofee[employeeName] = cofeeName;
                }

                inputLine = Console.ReadLine();
            }

            foreach (var cofee in CofeeQuantity.Where(c => c.Value == 0))
            {
                Console.WriteLine($"Out of {cofee.Key}");
            }

            inputLine = Console.ReadLine();
            while (inputLine != EndB)
            {
                var tokens = inputLine.Split().ToArray();
                var employeeName = tokens[0];
                var cofeeName = PersonCofee[employeeName];
                var cofeeQuantityValue = int.Parse(tokens[1]);

                CofeeQuantity[cofeeName] -= cofeeQuantityValue;
                if (CofeeQuantity[cofeeName] <= 0)
                {
                    CofeeQuantity[cofeeName] = 0;
                    Console.WriteLine($"Out of {cofeeName}");
                }

                inputLine = Console.ReadLine();
            }

            PrintOutput();
        }

        private static void PrintOutput()
        {
            Console.WriteLine("Coffee Left:");
            var sortedCofeeLeft = CofeeQuantity
                .Where(c => c.Value > 0)
                .OrderByDescending(c => c.Value);
            foreach (var cofeeName in sortedCofeeLeft)
            {
                Console.WriteLine($"{cofeeName.Key} {cofeeName.Value}");
            }

            Console.WriteLine("For:");
            var orderedCofeeDrinkers = CofeeDrinkers
                .Where(c => CofeeQuantity[c.Key] > 0)
                .OrderBy(c => c.Key);

            foreach (var orderedCofeeDrinker in orderedCofeeDrinkers)
            {
                var drinkers = orderedCofeeDrinker.Value.OrderByDescending(dr => dr);
                foreach (var drinker in drinkers)
                {
                    Console.WriteLine($"{drinker} {orderedCofeeDrinker.Key}");
                }
            }
        }

        private static bool IsCofeeQuantity(string line)
        {
            return line.Contains(delimiterB);
        }
    }
}