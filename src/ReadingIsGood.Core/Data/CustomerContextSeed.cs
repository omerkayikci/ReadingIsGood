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
            bool existProduct = customerCollection.Find(p => true).Any();
            if (!existProduct)
            {
                customerCollection.InsertManyAsync(GetPreconfiguredCustomers());
            }
        }

        private static IEnumerable<Customer> GetPreconfiguredCustomers()
        {
            return new List<Customer>()
            {
                new Customer(Guid.Parse("c6fa12d0-9d21-405b-9049-8cfb0e519f33"),"Customer1", "02125431111","asdas1@asdas.com", "Adres1"),
                new Customer(Guid.Parse("3843980c-08bb-4a41-995a-d2171b598b97"),"Customer2", "02125431112","asdas2@asdas.com", "Adres2"),
                new Customer(Guid.Parse("6a31aafc-e98a-4c01-8e99-0e00ddd7c2d8"),"Customer3", "02125431113","asdas3@asdas.com", "Adres3"),
                new Customer(Guid.Parse("4d0cebc4-cd0b-405a-b31b-0477701213e6"),"Customer4", "02125431114","asdas4@asdas.com", "Adres4"),
            };
        }
    }
}
