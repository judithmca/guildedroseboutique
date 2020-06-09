using GildedRoseOnlineStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRoseOnlineStore.Models
{
    public class SQLOrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public SQLOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Order;
        }

        public Order Add(Order newOrder)
        {
            newOrder.Id = Guid.NewGuid() ;

            _context.Order.Add(newOrder);
            _context.SaveChanges();
            return newOrder;
        }

        public Order GetOrderById(Guid id) => _context.Order.FirstOrDefault(a => a.Id == id);

        public Order Remove(Guid id)
        {
            var existing = _context.Order.First(a => a.Id == id);
            if (existing != null)
            {
                _context.Order.Remove(existing);
                _context.SaveChanges();
            }

            return existing;
        }

        public Order Update(Order changeOrder)
        {
            var existing = _context.Order.Attach(changeOrder);
            existing.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return changeOrder;
           
        }


        public Order PlaceOrder(int productId, string userId)
        {
            Order newOrder = new Order()
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                UserId = userId,
                PlaceOrderDate = DateTime.Now,
                State = "Placed"
            };

            _context.Order.Add(newOrder);
            _context.SaveChanges();
            return newOrder;
        }
    }
}
