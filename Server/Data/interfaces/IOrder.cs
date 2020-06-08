using Server.Data.Models;
using System.Collections.Generic;

namespace Server.Data.interfaces
{
    public interface IOrder
    {
        public IEnumerable<OrderWithProductInfo> GetAllOrders();

        public IEnumerable<OrderWithProductInfo> GetOrders(string Name);

        public OrderWithProductInfo GetOrder(int id);

        public void AddNewOrder(Order order);

        public void EditOrder(int id, Order order);

        public void DeleteOrder(int id);

        public int FindNewId();

    }
}
