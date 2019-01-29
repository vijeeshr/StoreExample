using System.Linq;
using NUnit.Framework;
using StoreExample.BusinessServices;
using StoreExample.Core;

namespace StoreExample.Tests
{
    [TestFixture]
    public class GroceryStoreTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(1, new string[] { "A 1 2", "A 2 1" }, ExpectedResult = 7)]
        [TestCase(2, new string[] { "A 1 5", "B 2 1", "A 3 5", "B 5 3", "A 8 2" }, ExpectedResult = 13)]
        [TestCase(2, new string[] { "A 1 2", "A 1 2", "A 2 1", "A 3 2" }, ExpectedResult = 6)]
        [TestCase(2, new string[] { "A 1 2", "A 1 3", "A 2 1", "A 2 1" }, ExpectedResult = 9)]
        [TestCase(2, new string[] { "A 1 3", "A 1 5", "A 3 1", "B 4 1", "A 4 1" }, ExpectedResult = 11)]
        public int GroceryStore_Executes_With_Correctness(int totalRegisters,
                                                          params string[] rawCustomerDetails)
        {
            var registerInitializer = new RegisterInitializer(totalRegisters);
            var customerInitializer = new CustomerInitializer(rawCustomerDetails);
            var customerQueueResolver = new CustomerQueueResolver();

            var store = new GroceryStore(registerInitializer,
                                         customerInitializer,
                                         customerQueueResolver);

            store.Start();

            return store.MinutesOpen;
        }


        [TestCase(1)]
        [TestCase(3)]
        public void Last_Register_Is_Always_A_Training_Register(int totalRegisters)
        {
            var registerInitializer = new RegisterInitializer(totalRegisters);
            var registers = registerInitializer.Initialize();
            var register = registers[registers.Count - 1];

            Assert.IsInstanceOf(typeof(TrainingRegister), register);
        }


        [TestCase("A 1 2", "A 1 1", ExpectedResult = "A 1 1")]
        [TestCase("A 1 4", "A 1 6", "B 1 3", ExpectedResult = "B 1 3")]
        public string Customers_With_Fewer_Items_Given_Preference(params string[] rawCustomerDetails)
        {
            // When multiple customers arrive at same time, those with fewer items get to choose register first.
            var customers = new CustomerInitializer(rawCustomerDetails).Initialize();
            var customerQueueResolver = new CustomerQueueResolver();

            var firstCustomer = customerQueueResolver.Resolve(customers).First();

            return string.Format("{0} {1} {2}",
                            firstCustomer.SelectionStrategy.GetType() == typeof(TypeASelectionStrategy) ? "A" : "B",
                            firstCustomer.TimeToQueue,
                            firstCustomer.Items);
        }


        [TestCase("B 1 2", "A 1 2", ExpectedResult = "A 1 2")]
        [TestCase("A 1 3", "B 1 2", "A 1 2", ExpectedResult = "A 1 2")]
        public string Customer_TypeA_Given_Preference(params string[] rawCustomerDetails)
        {
            // When multiple customers arrive at same time, with same number of items, Customer "TypeA" chooses first.
            var customers = new CustomerInitializer(rawCustomerDetails).Initialize();
            var customerQueueResolver = new CustomerQueueResolver();

            var firstCustomer = customerQueueResolver.Resolve(customers).First();

            return string.Format("{0} {1} {2}",
                            firstCustomer.SelectionStrategy.GetType() == typeof(TypeASelectionStrategy) ? "A" : "B",
                            firstCustomer.TimeToQueue,
                            firstCustomer.Items);
        }
    }
}