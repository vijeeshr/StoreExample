namespace StoreExample.Core
{
    public class RegularRegister : Register
    {
        public override void ProcessItem(int currentTimeFromOffset)
        {
            var customer = _queue[0];

            customer.Items--;

            if (customer.Items == 0) _queue.RemoveAt(0);
        }
    }
}