using System;

namespace Server.Data.Models
{
    public class Order
    {
        public int Id { set; get; }
        
        public int ProductId { set; get; }

        public int Count { set; get; }

        public string Name { set; get; }

        public DateTime Date { set; get; }
    }
}
