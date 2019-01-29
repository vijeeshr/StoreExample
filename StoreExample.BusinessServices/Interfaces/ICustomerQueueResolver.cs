using System.Collections.Generic;
using StoreExample.Core;

namespace StoreExample.BusinessServices.Interfaces
{
    public interface ICustomerQueueResolver
    {
        IEnumerable<Customer> Resolve(IEnumerable<Customer> customers);
    }
}