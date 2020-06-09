using GildedRoseOnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRoseOnlineStore.Data.MockData
{
    public static class MockData
    {
        public static IEnumerable<Product> GetTestProductItems()
        {
            var _products = new List<Product>()
            {
               new Product()
                {
                    Id = 1,
                    Name="Product 1",
                    Description="Testing Product 1",
                    Picture= "1.jpg",
                    Price= 500,
                    Quantity= 10
                },
                 new Product()
                {
                    Id = 2,
                    Name="Product 2",
                    Description="Testing Product 2",
                    Picture= "2.jpg",
                    Price= 20,
                    Quantity= 20
                },
                new Product()
                {
                    Id = 3,
                    Name="Product 3",
                    Description="Testing Product 3",
                    Picture= "3.jpg",
                    Price= 300,
                    Quantity= 30
                },
                new Product()
                {
                    Id = 4,
                    Name="Product 4",
                    Description="Testing Product 4",
                    Picture= "4.jpg",
                    Price= 40,
                    Quantity= 4
                }, 
                new Product()
                {
                    Id = 5,
                    Name="Product 5",
                    Description="Testing Product 5",
                    Picture= "5.jpg",
                    Price= 550,
                    Quantity= 5
                }
            };

            return _products;
        }
    }
}
