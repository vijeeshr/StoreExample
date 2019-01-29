using System.Collections.Generic;

namespace StoreExample.Core
{
    public abstract class Register
    {
        protected List<Customer> _queue;

        public int Id { get; set; }

        public List<Customer> Queue
        {
            get { return _queue; }
        }

        public Register()
        {
            _queue = new List<Customer>();
        }

        public void AddToQueue(Customer customer)
        {
            _queue.Add(customer);
        }

        public Customer GetLastCustomer()
        {
            if (_queue.Count == 0) return null;

            return _queue[_queue.Count - 1];
        }

        public abstract void ProcessItem(int currentTimeFromOffset);
    }
}
