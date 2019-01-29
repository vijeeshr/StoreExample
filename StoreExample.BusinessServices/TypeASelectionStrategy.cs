using System.Collections.Generic;
using System.Linq;
using StoreExample.Core;
using StoreExample.Core.Interfaces;

namespace StoreExample.BusinessServices
{
    public class TypeASelectionStrategy : IRegisterSelectionStrategy
    {
        // Strategy for CustomerType A
        public void SelectRegister(List<Register> registers, Customer customer)
        {
            // Pick register with fewest customers in line.
            // If multiple registers have equal customers in line, just pick the first register
            var registersToUpdate = registers.OrderBy(x => x.Queue.Count)
                                             .ThenBy(y => y.Id)
                                             .First();

            registersToUpdate.AddToQueue(customer);
        }

        public int Priority
        {
            get { return 1; }
        }
    }
}