using System;
using System.Threading.Tasks;

namespace Calculations.ConsoleClient
{
    internal static class Program
    {
        /// <summary>
        /// Calculates the sum from 1 to n synchronously.
        /// The value of n is set by the user from the console.
        /// The user can change the boundary n during the calculation, which causes the calculation to be restarted,
        /// this should not crash the application.
        /// After receiving the result, be able to continue calculations without leaving the console.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task Main()
        {
            while (true)
            {
                Console.Write("Enter n (or 'exit' to quit): ");

                string? input = Console.ReadLine();

                if (input == "exit")
                {
                    break;
                }

                if (!int.TryParse(input, out int n) || n <= 0)
                {
                    Console.WriteLine("Please enter a positive integer.");
                    continue;
                }

                using CancellationTokenSource cts = new ();

                try
                {
                    Console.WriteLine($"Calculating sum from 1 to {n}...");

                    long result = await Calculator.CalculateSumAsync(n, cts.Token);

                    Console.WriteLine($"Result:{result}");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Calculation was canceled.");
                }
            }
        }
    }
}
