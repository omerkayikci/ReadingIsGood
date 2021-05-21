using ReadingIsGood.Core.Entities;
using ReadingIsGood.Core.Services.Abstractions;
using ReadingIsGood.MongoDB.Abstractions;
using System;
using System.Collections.Generic;

namespace ReadingIsGood.Core.Services
{
    public class SeedService : ISeedService
    {
        private readonly IGenericRepository<Product, string> productGenericRepository;
        private readonly IGenericRepository<Customer, string> customerGenericRepository;
        private readonly IGenericRepository<User, string> userGenericRepository;
        public SeedService(IGenericRepository<Product, string> productGenericRepository,
            IGenericRepository<Customer, string> customerGenericRepository,
            IGenericRepository<User, string> userGenericRepository)
        {
            this.productGenericRepository = productGenericRepository;
            this.customerGenericRepository = customerGenericRepository;
            this.userGenericRepository = userGenericRepository;

            SeedProductData();
            SeedCustomerData();
        }

        public void SeedCustomerData()
        {
            if (!this.customerGenericRepository.CollectionExists && !this.userGenericRepository.CollectionExists)
            {
                this.customerGenericRepository.CreateCollection();
                this.userGenericRepository.CreateCollection();

                Customer customer = new Customer
                {
                    Address = "Address Test",
                    Email = "testemail@testemail.com",
                    Name = "Customer Test"
                };

                this.customerGenericRepository.AddOne(customer);

                User user = new User
                {
                    CustomerId = customer.Id,
                    Password = "123456",
                    Username = "customer-test"
                };

                this.userGenericRepository.AddOne(user);
            }
        }

        public void SeedProductData()
        {
            if (!this.productGenericRepository.CollectionExists)
            {
                this.productGenericRepository.CreateCollection();

                List<Product> products = new List<Product>
                {
                    new Product
                    {
                        CreatedDateTime = DateTime.UtcNow,
                        Price = 15,
                        SKU = "SKUBOOK1",
                        Stock = 50
                    },
                    new Product
                    {
                        CreatedDateTime = DateTime.UtcNow,
                        Price = 25,
                        SKU = "SKUBOOK2",
                        Stock = 60
                    },
                    new Product
                    {
                        CreatedDateTime = DateTime.UtcNow,
                        Price = 35,
                        SKU = "SKUBOOK3",
                        Stock = 70
                    },
                    new Product
                    {
                        CreatedDateTime = DateTime.UtcNow,
                        Price = 5,
                        SKU = "SKUBOOK4",
                        Stock = 30
                    }
                };

                this.productGenericRepository.AddMany(products);
            }
        }
    }
}
