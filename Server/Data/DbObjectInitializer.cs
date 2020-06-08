using Server.Data.Models;
using System;
using System.Linq;

namespace Server.Data
{
    public class DbObjectInitializer
    {
        public static void Init(DataContext content)
        {
            if (!content.Products.Any())
                content.Products.AddRange(products);

            content.SaveChanges();

            if (!content.Orders.Any())
                content.Orders.AddRange(orders);

            content.SaveChanges();
        }

        private static Product[] products
        {
            get
            {
                return new Product[]
                {
                    new Product { Id = 1, Price = 100.1f, Title = "Product1"},
                    new Product { Id = 2, Price = 200.1f, Title = "Product2"},
                    new Product { Id = 3, Price = 300.1f, Title = "Product3"},
                    new Product { Id = 4, Price = 400.1f, Title = "Product4"},
                    new Product { Id = 5, Price = 500.1f, Title = "Product5"},
                    new Product { Id = 6, Price = 600.1f, Title = "Product6"},
                    new Product { Id = 7, Price = 700.1f, Title = "Product7"},
                    new Product { Id = 8, Price = 800.1f, Title = "Product8"},
                    new Product { Id = 9, Price = 900.1f, Title = "Product9"}

                };  
            }
        }

        private static Order[] orders
        {
            get
            {              
                return new Order[]
                {
                    new Order { Id = 1, ProductId = 1,  Name = "Mike", Count = 1, Date = DateTime.Now},
                    new Order { Id = 2, ProductId = 2,  Name = "Mike", Count = 2, Date = DateTime.Now},
                    new Order { Id = 3, ProductId = 3,  Name = "Sazonov",   Count = 1, Date = DateTime.Now},
                    new Order { Id = 4, ProductId = 4,  Name = "FFFF",   Count = 4, Date = DateTime.Now},
                    new Order { Id = 5, ProductId = 5,  Name = "Mike",   Count = 1, Date = DateTime.Now},
                    new Order { Id = 6, ProductId = 6,  Name = "asas",   Count = 2, Date = DateTime.Now},
                    new Order { Id = 7, ProductId = 7,  Name = "Mike",   Count = 3, Date = DateTime.Now},
                    new Order { Id = 8, ProductId = 8,  Name = "Sazonov",   Count = 4, Date = DateTime.Now},
                    new Order { Id = 9, ProductId = 9,  Name = "asas",   Count = 1, Date = DateTime.Now}
                };
            }
        }
    }
}
