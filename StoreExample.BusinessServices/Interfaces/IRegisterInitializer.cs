using System.Collections.Generic;
using StoreExample.Core;

namespace StoreExample.BusinessServices.Interfaces
{
    public interface IRegisterInitializer
    {
        List<Register> Initialize();
    }
}