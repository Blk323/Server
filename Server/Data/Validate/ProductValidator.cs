using Server.Data.interfaces;
using Server.Data.Models;
using System.Linq;

namespace Server.Data.Validate
{
    public class ProductValidator : IProductValidator
    {
        DataContext data;
        public ProductValidator(DataContext dataContext)
        {
            data = dataContext;
        }
        public bool ValidateId(string id = null)
        {
            if (id != null)
            {
                int res;
                try
                {
                    res = int.Parse(id);
                    return data.Products.Where(el => el.Id == res).Any();
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }

        public bool ValidatePrice(Product product = null)
        {
            if (product != null)
            {
                return product.Price >= 1;
            }
            else
                return false;
        }

        public bool ValidateTitle(Product product = null)
        {
            if (product != null)
            {
                return !string.IsNullOrEmpty(product.Title?.Trim());
            }
            else
                return false;
        }
    }
}
