namespace _02.SoftUniWaterSupplies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class SoftUniWaterSupplies
    {
        public static void Main()
        {
            var totalWater = double.Parse(Console.ReadLine());
            var inOrderTraversal = (int)totalWater % 2 == 0;
            var bottles = Console.ReadLine()
                .Split()
                .Select(double.Parse)
                .ToArray();

            var singleBottleCapacity = double.Parse(Console.ReadLine());

            if (inOrderTraversal)
            {
                for (var i = 0; i < bottles.Length; i++)
                {
                    totalWater = PourWater(singleBottleCapacity, bottles, i, totalWater, true);
                }
            }
            else
            {
                for (var i = bottles.Length - 1; i >= 0; i--)
                {
                    totalWater = PourWater(singleBottleCapacity, bottles, i, totalWater, false);
                }
            }

            Console.WriteLine("Enough water!");
            Console.WriteLine($"Water left: {totalWater}l.");
        }

        private static double PourWater(double singleBottleCapacity, double[] bottles, int i, double totalWater, bool inOrderTraversal)
        {
            var waterToPour = singleBottleCapacity - bottles[i];
            if (totalWater >= waterToPour)
            {
                totalWater -= waterToPour;
            }
            else
            {
                int amountOfBottlesLeft;
                var litersShort = waterToPour - totalWater;
                List<int> indexesLeft;
                if (inOrderTraversal)
                {
                    amountOfBottlesLeft = bottles.Length - i;
                    for (var j = i + 1; j < bottles.Length; j++)
                    {
                        litersShort += singleBottleCapacity - bottles[j];
                    }

                    indexesLeft = Enumerable.Range(i, bottles.Length - i).ToList();
                }
                else
                {
                    amountOfBottlesLeft = i + 1;
                    for (var j = i - 1; j >= 0; j--)
                    {
                        litersShort += singleBottleCapacity - bottles[j];
                    }

                    indexesLeft = Enumerable.Range(0, i + 1).Reverse().ToList();
                }


                Console.WriteLine("We need more water!");
                Console.WriteLine($"Bottles left: {amountOfBottlesLeft}");
                Console.WriteLine($"With indexes: {string.Join(", ", indexesLeft)}");
                Console.WriteLine($"We need {litersShort} more liters!");
                Environment.Exit(0);
            }

            return totalWater;
        }
    }
}