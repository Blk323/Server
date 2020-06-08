using System;

namespace Server.Data.Models
{
    public class OrderWithProductInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProductId { get; set; }

        public string ProductTitle { get; set; }
        
        public double ProductPrice { get; set; }
        
        public int Count { get; set; }

        public DateTime DateOfOrder { get; set; }
    }
}
