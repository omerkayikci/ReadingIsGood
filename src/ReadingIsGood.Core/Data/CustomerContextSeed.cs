using MongoDB.Driver;
using ReadingIsGood.Core.Entities;
using System;
using System.Collections.Generic;

namespace ReadingIsGood.Core.Data
{
    public class CustomerContextSeed
    {
        public static void SeedData(IMongoCollection<Customer> customerCollection)
        {
            bool existCustomer = customerCollection.Find(p => true).Any();
            if (!existCustomer)
            {
                customerCollection.InsertManyAsync(GetPreconfiguredCustomers());
            }
        }

        private static IEnumerable<Customer> GetPreconfiguredCustomers()
        {
            return new List<Customer>()
            {
                new Customer(){
                    Name ="Customer1",
                    PhoneNumber = "02125431111",
                    Email = "asdas1@asdas.com",
                    Address = "Adres1"
                },
                new Customer(){
                    Name ="Customer2",
                    PhoneNumber = "02125431112",
                    Email = "asdas2@asdas.com",
                    Address = "Adres2"
                },
                new Customer(){
                    Name ="Customer3",
                    PhoneNumber = "02125431113",
                    Email = "asdas3@asdas.com",
                    Address = "Adres3"
                },
                new Customer(){
                    Name ="Customer4",
                    PhoneNumber = "02125431114",
                    Email = "asdas4@asdas.com",
                    Address = "Adres4"
                }
            };
        }
    }
}
