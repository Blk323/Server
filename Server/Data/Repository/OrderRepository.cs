using Server.Data.interfaces;
using Server.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Data.Repository
{
    public class OrderRepository : IOrder
    {
        private readonly DataContext _dataContext;

        public OrderRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<OrderWithProductInfo> GetAllOrders()
        {
            IEnumerable<Order> orders = _dataContext.Orders.Where(c => c.Id > 0);
            return CollectInfo(orders).ToArray();
        }

        public IEnumerable<OrderWithProductInfo> GetOrders(string Name)
        {
            IEnumerable<Order> orders = _dataContext.Orders.Where(c => c.Name == Name);
            return CollectInfo(orders).ToArray();
        }

        private List<OrderWithProductInfo> CollectInfo(IEnumerable<Order> orders)
        {
            IEnumerable<Product> productInfo = _dataContext.Products.Where(element => element.Id > 0);
            List<OrderWithProductInfo> arrayOfOrders = new List<OrderWithProductInfo>();
            foreach (var order in orders)
            {
                var product = productInfo.FirstOrDefault(e => e.Id == order.ProductId);
                arrayOfOrders.Add(new OrderWithProductInfo()
                {
                    Id = order.Id,
                    Name = order.Name,
                    ProductId = product.Id,
                    ProductTitle = product.Title,
                    ProductPrice = product.Price,
                    Count = order.Count,
                    DateOfOrder = order.Date
                });
            }
            return arrayOfOrders;
        }

        public OrderWithProductInfo GetOrder(int id)
        {

            var order = _dataContext.Orders.FirstOrDefault(c => c.Id == id);
            var productInfo = _dataContext.Products.Where(element => element.Id > 0).FirstOrDefault(e => e.Id == order.ProductId);
            return (new OrderWithProductInfo()
            {
                Id = order.Id,
                Name = order.Name,
                ProductId = productInfo.Id,
                ProductTitle = productInfo.Title,
                ProductPrice = productInfo.Price,
                Count = order.Count,
                DateOfOrder = order.Date
            });
        }

        public void AddNewOrder(Order order)
        {

            order.Id = FindNewId();
            order.Date = DateTime.Now;

            try
            {
                _dataContext.Orders.Add(order);
                _dataContext.SaveChanges();
            }
            finally
            {
            }
        }

        public void DeleteOrder(int id)
        {
            Order oldOrder = _dataContext.Orders.FirstOrDefault(c => c.Id == id);

            try
            {
                _dataContext.Orders.Remove(oldOrder);
                _dataContext.SaveChanges();
            }
            finally
            {
            }
        }

        public void EditOrder(int id, Order order)
        {

            var oldOrder = _dataContext.Orders.FirstOrDefault(c => c.Id == id);

            oldOrder.Name = order.Name;
            oldOrder.ProductId = order.ProductId;
            oldOrder.Count = order.Count;

            try
            {
                _dataContext.Orders.Update(oldOrder);
                _dataContext.SaveChanges();
            }
            finally
            {
            }

        }

        public int FindNewId()
        {
            int i = 1;
            int max = _dataContext.Orders.Max(i => i.Id);
            while (i < max)
            {
                if (!(_dataContext.Orders.FirstOrDefault(el => el.Id == i) is Order))
                    return i;
                else
                    i++;
            }
            return max + 1;
        }
    }
}
