namespace _01.SoftUniAirline
{
    using System;
    using System.Linq;

    public static class SoftUniAirline
    {
        private static decimal[] Flights;

        public static void Main()
        {
            var numberOfFlights = int.Parse(Console.ReadLine());
            Flights = new decimal[numberOfFlights];
            for (int i = 0; i < numberOfFlights; i++)
            {
                var adults = int.Parse(Console.ReadLine());
                var adultTicketPrice = decimal.Parse(Console.ReadLine());
                var youths = int.Parse(Console.ReadLine());
                var youthTicketPrice = decimal.Parse(Console.ReadLine());
                var fuelPricePerHour = decimal.Parse(Console.ReadLine());
                var fuelConsumptionPerHour = decimal.Parse(Console.ReadLine());
                var flightDuration = decimal.Parse(Console.ReadLine());

                var income = (adults * adultTicketPrice) + (youths * youthTicketPrice);
                var outcome = flightDuration * fuelConsumptionPerHour * fuelPricePerHour;

                var profit = income - outcome;
                Console.WriteLine(profit > 0 ? $"You are ahead with {profit:f3}$." : $"We've got to sell more tickets! We've lost {profit:f3}$.");
                Flights[i] = profit;
            }

            var totalProfit = Flights.Sum();
            Console.WriteLine($"Overall profit -> {totalProfit:f3}$.");
            Console.WriteLine($"Average profit -> {totalProfit / Flights.Length:f3}$.");
        }
    }
}