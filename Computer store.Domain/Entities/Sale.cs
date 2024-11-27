using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_store.Domain.Entities
{
    public class Sale : Entity
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Amount { get; set; }
        public DateOnly Date { get; set; }
        public Sale(int productId, int userId, int amount, DateOnly date)
        {
            ProductId = productId;
            UserId = userId;
            Amount = amount;
            Date = date;
        }
    }
}
