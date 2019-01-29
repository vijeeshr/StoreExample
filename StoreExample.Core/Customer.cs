using System.Collections.Generic;
using StoreExample.Core.Interfaces;

namespace StoreExample.Core
{
    public class Customer
    {
        private IRegisterSelectionStrategy _selectionStrategy;

        public int Items { get; set; }

        public int TimeToQueue { get; set; }

        public IRegisterSelectionStrategy SelectionStrategy
        {
            get { return _selectionStrategy; }
        }

        public Customer(IRegisterSelectionStrategy selectionStrategy)
        {
            _selectionStrategy = selectionStrategy;
        }

        public int Priority
        {
            get { return _selectionStrategy.Priority; }
        }

        public void SelectRegister(List<Register> registers)
        {
            _selectionStrategy.SelectRegister(registers, this);
        }
    }
}
