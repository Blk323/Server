using Server.Data.interfaces;
using Server.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Server.Data.Repository
{
    public class ProductRepository : IProduct
    {
        private readonly DataContext _dataContext;

        public ProductRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<Product> GetAllProducts() => _dataContext.Products.Where(c => c.Id > 0);

        public Product GetProduct(int id) => _dataContext.Products.FirstOrDefault(c => c.Id == id);

        public void AddNewProduct(Product webProduct)
        {
            webProduct.Id = FindNewId();

            try
            {
                _dataContext.Products.Add(webProduct);
                _dataContext.SaveChanges();
            }
            finally
            {
            }
        }

        public void DeleteProduct(int id)
        {
            Product product = _dataContext.Products.FirstOrDefault(el => el.Id == id);

            try
            {
                _dataContext.Products.Remove(product);
                _dataContext.SaveChanges();
            }
            finally
            {
            }

        }

        public void EditProduct(int id, Product webProduct)
        {
            var oldProduct = _dataContext.Products.FirstOrDefault(c => c.Id == id);

            oldProduct.Title = webProduct.Title;
            oldProduct.Price = webProduct.Price;

            try
            {
                _dataContext.Products.Update(oldProduct);
                _dataContext.SaveChanges();
            }
            finally
            {
            }
        }

        public int FindNewId()
        {
            int i = 1;
            int max = _dataContext.Products.Max(i => i.Id);
            while (i < max)
            {
                if (!(_dataContext.Products.FirstOrDefault(el => el.Id == i) is Product))
                    return i;
                else
                    i++;
            }
            return max + 1;
        }
    }
}
