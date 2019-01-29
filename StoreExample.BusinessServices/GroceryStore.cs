using System.Collections.Generic;
using System.Linq;
using StoreExample.BusinessServices.Interfaces;
using StoreExample.Core;

namespace StoreExample.BusinessServices
{
    public class GroceryStore
    {
        private readonly List<Register> _registers;
        private readonly List<Customer> _customers;
        private readonly ICustomerQueueResolver _customerQueueResolver;

        public int MinutesOpen { get; private set; }

        public GroceryStore(IRegisterInitializer registerInitializer,
                            ICustomerInitializer customerInitializer,
                            ICustomerQueueResolver customerQueueResolver)
        {
            _registers = new List<Register>();
            _registers.AddRange(registerInitializer.Initialize());

            _customers = new List<Customer>();
            _customers.AddRange(customerInitializer.Initialize());

            _customerQueueResolver = customerQueueResolver;
        }

        public void Start()
        {
            var timeFromOffset = 0;
            var continueTicks = true;

            while (continueTicks)
            {
                timeFromOffset++;

                ProcessRegister(timeFromOffset);

                EnqueueCustomers(timeFromOffset);

                if (IsDone(timeFromOffset)) continueTicks = false;
            }

            MinutesOpen = timeFromOffset;
        }

        private void ProcessRegister(int timeFromOffset)
        {
            // Process existing Registers
            foreach (var register in _registers)
            {
                if (register.Queue.Count > 0)
                    register.ProcessItem(timeFromOffset);
            }
        }

        private void EnqueueCustomers(int timeFromOffset)
        {
            // Resolve the correct "Register" for the customers            
            var customersToEnqueue = _customerQueueResolver.Resolve(
                    _customers.Where(c => c.TimeToQueue == timeFromOffset));

            foreach (var customerToEnqueue in customersToEnqueue)
            {
                customerToEnqueue.SelectRegister(_registers);
            }
        }

        private bool IsDone(int timeFromOffset)
        {
            if (_customers.Any(x => x.TimeToQueue >= timeFromOffset) == true)
                return false;

            if (_registers.Any(x => x.Queue.Count > 0))
                return false;

            return true;
        }
    }
}