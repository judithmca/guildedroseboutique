using GildedRoseOnlineStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRoseOnlineStore.Models
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public SQLProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }

        public Product Add(Product newProduct)
        {
            newProduct.Id = _context.Products.Max(e=>e.Id)+1 ;
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return newProduct;
        }

        public Product GetProductById(int id) => _context.Products.FirstOrDefault(a => a.Id == id);

        public Product Remove(int id)
        {
            var existing = _context.Products.First(a => a.Id == id);
            if (existing != null)
            {
                _context.Products.Remove(existing);
                _context.SaveChanges();
            }

            return existing;
        }

        public Product Update(Product changeProduct)
        {
            var existing = _context.Products.Attach(changeProduct);
            existing.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return changeProduct;
           
        }

        public bool Deplete(int id)
        {
            var existing = _context.Products.First(a => a.Id == id);
            
            if (existing != null && existing.Quantity>=1)
            {
                existing.Quantity--;
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
