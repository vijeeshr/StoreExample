using System.Collections.Generic;
using StoreExample.BusinessServices.Interfaces;
using StoreExample.Core;

namespace StoreExample.BusinessServices
{
    public class RegisterInitializer : IRegisterInitializer
    {
        private readonly int _registersToInitialize;

        public RegisterInitializer(int registersToInitialize)
        {
            _registersToInitialize = registersToInitialize;
        }

        public List<Register> Initialize()
        {
            var registers = new List<Register>();

            for (var i = 1; i <= _registersToInitialize; i++)
            {
                if (i == _registersToInitialize)
                    registers.Add(new TrainingRegister { Id = i });
                else
                    registers.Add(new RegularRegister { Id = i });
            }

            return registers;
        }
    }
}