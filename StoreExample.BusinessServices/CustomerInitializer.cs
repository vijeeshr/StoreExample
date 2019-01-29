using System;
using System.Collections.Generic;
using StoreExample.BusinessServices.Interfaces;
using StoreExample.Core;
using StoreExample.Core.Interfaces;

namespace StoreExample.BusinessServices
{
    public class CustomerInitializer : ICustomerInitializer
    {
        private readonly string[] _customersToInitialize;

        public CustomerInitializer(string[] customersToInitialize)
        {
            _customersToInitialize = customersToInitialize;
        }

        public List<Customer> Initialize()
        {
            var customers = new List<Customer>();

            for (var i = 0; i < _customersToInitialize.Length; i++)
            {
                var parsedCustomerDetails = _customersToInitialize[i].Split(' ');

                IRegisterSelectionStrategy selectionStrategy;

                if (parsedCustomerDetails[0].Trim().ToUpper() == "A")
                    selectionStrategy = new TypeASelectionStrategy();
                else
                    selectionStrategy = new TypeBSelectionStrategy();

                customers.Add(new Customer(selectionStrategy)
                {
                    TimeToQueue = Convert.ToInt32(parsedCustomerDetails[1].Trim()),
                    Items = Convert.ToInt32(parsedCustomerDetails[2].Trim())
                });
            }

            return customers;
        }
    }
}