using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_store.Domain.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int ProviderId { get; set; }
        public double Price{ get; set; }
        public int Quantity { get; set; }
        public Product(string name, string description, int categoryId, int providerId, double price, int quantity)
        {
            Name = name;
            Description = description;
            CategoryId = categoryId;
            ProviderId = providerId;
            Price = price;
            Quantity = quantity;
        }
    }
}
