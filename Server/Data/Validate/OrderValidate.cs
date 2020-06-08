using Server.Data.interfaces;
using Server.Data.Models;
using System.Linq;

namespace Server.Data.Validate
{
    public class OrderValidate : IOrderValidator
    {
        DataContext data;
        public OrderValidate(DataContext dataContext)
        {
            data = dataContext;
        }

        public bool ValidateName(Order order = null, string name = null)
        {
            if (order != null)
                return !string.IsNullOrEmpty(order.Name?.Trim());
            else
                return !string.IsNullOrEmpty(name?.Trim());
        } 


        public bool ValidateId(string id = null)
        {

            if (id != null)
            {
                int res = 0;
                try
                {
                    res = int.Parse(id);
                    return data.Orders.Where(el => el.Id == res).Any();
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
                
        }


        public bool ValidateProductId(Order order = null)
        {
            if (order != null)
            
                return data.Products.Where(el => el.Id == order.ProductId).Any();
            else
                return false;
        }

        public bool ValidateCount(Order order = null)
        {
            if (order != null)

                return order.Count >= 1;
            else
                return false;
        }


    }
}
