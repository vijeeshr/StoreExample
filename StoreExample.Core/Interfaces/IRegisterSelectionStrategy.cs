using System.Collections.Generic;

namespace StoreExample.Core.Interfaces
{
    public interface IRegisterSelectionStrategy
    {
        void SelectRegister(List<Register> registers, Customer customer);

        int Priority { get; }
    }
}
