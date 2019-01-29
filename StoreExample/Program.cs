using System;
using System.IO;
using System.Linq;
using StoreExample.BusinessServices;

namespace StoreExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFilePath = args.Length > 0 ? args[0] : "input.txt";
            var lines = File.ReadAllLines(inputFilePath);

            var registerInitializer = new RegisterInitializer(Convert.ToInt32(lines[0]));
            var customerInitializer = new CustomerInitializer(lines.Skip(1).ToArray());
            var customerQueueResolver = new CustomerQueueResolver();

            var store = new GroceryStore(registerInitializer,
                                         customerInitializer,
                                         customerQueueResolver);

            store.Start();

            Console.WriteLine("Finished at: t={0} minutes", store.MinutesOpen);

            Console.Read();            
        }
    }
}