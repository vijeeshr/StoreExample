namespace StoreExample.Core
{
    public class TrainingRegister : Register
    {
        private int _offsetCounter = 0;

        public override void ProcessItem(int currentTimeFromOffset)
        {
            var customer = _queue[0];
            if (_offsetCounter == 0) _offsetCounter = customer.TimeToQueue;

            if ((currentTimeFromOffset - _offsetCounter) % 2 == 0)
            {
                customer.Items--;
                _offsetCounter = currentTimeFromOffset;
            }

            if (customer.Items == 0) _queue.RemoveAt(0);
        }
    }
}