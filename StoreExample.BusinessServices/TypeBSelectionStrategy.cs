using System.Collections.Generic;
using System.Linq;
using StoreExample.Core;
using StoreExample.Core.Interfaces;

namespace StoreExample.BusinessServices
{
    public class TypeBSelectionStrategy : IRegisterSelectionStrategy
    {
        // Strategy for CustomerType B        
        public void SelectRegister(List<Register> registers, Customer customer)
        {
            // Find first empty line; pick that register
            var registersToUpdate = registers.Where(x => x.Queue.Count == 0)
                                             .OrderBy(y => y.Id)
                                             .FirstOrDefault();

            if (registersToUpdate != null)
            {
                registersToUpdate.AddToQueue(customer);
                return;
            }

            // Look up last customer is each register with fewest number of items to check-in.
            registersToUpdate = registers.OrderBy(x => x.GetLastCustomer().Items)
                                         .ThenBy(y => y.Id)
                                         .First();

            registersToUpdate.AddToQueue(customer);
        }

        public int Priority
        {
            get { return 2; }
        }
    }
}