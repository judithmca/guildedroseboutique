using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRoseOnlineStore.Models
{
    public interface IOrderRepository
    {
        Order GetOrderById(Guid Id);
        IEnumerable<Order> GetAll();
        Order Add(Order newProduct);
        Order Update(Order changeProduct);
        Order Remove(Guid id);
        Order PlaceOrder(int productId, string userId);
    }
}
