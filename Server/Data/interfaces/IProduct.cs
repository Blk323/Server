using Server.Data.Models;
using System.Collections.Generic;

namespace Server.Data.interfaces
{
    public interface IProduct
    {
        public IEnumerable<Product> GetAllProducts();

        public Product GetProduct(int id);

        public void AddNewProduct(Product product);

        public void EditProduct(int id, Product product);

        public void DeleteProduct(int id);

        public int FindNewId();
    }
}
