using Bogus;
using System;
using System.Configuration;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int seed;

            Console.WriteLine("Enter a int:");
            var input = Console.ReadLine();

            while (!int.TryParse(input, out seed))
            {
                Console.WriteLine("The input was an invalid number, try again: ");
                input = Console.ReadLine();
            }

            for (int i = 0; i < seed; i++)
            {
                var random = new Random();
                DateTime RandDay()
                {
                    DateTime start = new DateTime(2017, 1, 1);
                    int range = (DateTime.Today - start).Days;
                    return start.AddDays(random.Next(range));
                }
                var testItems = new Faker<RequestModel>()
                                    .RuleFor(x => x.Name, "console" + random.Next(int.MaxValue))
                                    .RuleFor(x => x.Date, RandDay())
                                    .RuleFor(x => x.Visits, x => random.Next(100))
                                    .RuleFor(x => x.Index, random.Next(1000));

                var requestItems = testItems.Generate();

                HttpRequest.HttpPost(ConfigurationManager.AppSettings["baseUrl"] + "/api/data", requestItems);
            }

            Console.WriteLine("Finished!");
        }
    }
}
