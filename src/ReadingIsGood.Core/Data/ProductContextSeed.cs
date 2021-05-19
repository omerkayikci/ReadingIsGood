using MongoDB.Driver;
using ReadingIsGood.Core.Entities;
using System.Collections.Generic;

namespace ReadingIsGood.Core.Data
{
    public static class ProductContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product(){
                    SKU = "ProductSKU1",
                    Stock = 50
                },
                new Product(){
                    SKU = "ProductSKU2",
                    Stock = 50
                },
                new Product(){
                    SKU = "ProductSKU3",
                    Stock = 50
                },
                new Product(){
                    SKU = "ProductSKU4",
                    Stock = 50
                },
                new Product(){
                    SKU = "ProductSKU5",
                    Stock = 50
                },
                new Product(){
                    SKU = "ProductSKU6",
                    Stock = 50
                },
                new Product(){
                    SKU = "ProductSKU7",
                    Stock = 50
                }
            };
        }
    }
}
