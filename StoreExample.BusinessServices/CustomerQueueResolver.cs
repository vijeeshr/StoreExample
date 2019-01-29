using System.Collections.Generic;
using System.Linq;
using StoreExample.BusinessServices.Interfaces;
using StoreExample.Core;

namespace StoreExample.BusinessServices
{
    public class CustomerQueueResolver : ICustomerQueueResolver
    {
        // Sort by fewer items first, and if equal, sort by Type A and Type B

        public IEnumerable<Customer> Resolve(IEnumerable<Customer> customers)
        {
            return customers.OrderBy(x => x.Items)
                            .ThenBy(y => y.Priority);
        }
    }
}