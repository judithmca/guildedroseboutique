using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRoseOnlineStore.Models
{
    public interface IProductRepository
    {
        Product GetProductById(int Id);
        IEnumerable<Product> GetAll();
        Product Add(Product newProduct);
        Product Update(Product changeProduct);
        Product Remove(int id);
        bool Deplete(int id);

    }
}
