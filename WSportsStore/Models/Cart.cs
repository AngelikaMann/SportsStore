using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSportsStore.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();
        public virtual void AddItem(Product product, int quantity)
        {
            CartLine line = Lines
            .Where(p => p.Product.ProductID == product.ProductID)
            .FirstOrDefault();
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        //++++++++++++++++
        public virtual void RemoveLine(Product product)
        {
            CartLine line = Lines
                        .Where(p => p.Product.ProductID == product.ProductID)
                        .FirstOrDefault();
            if (line.Quantity != 1)
            {
                line.Quantity = line.Quantity - 1;
            }
            else
            {
                Lines.RemoveAll(l => l.Product.ProductID == product.ProductID);
            }
        } 





        //Lines.RemoveAll(l => l.Product.ProductID == product.ProductID);
        public  virtual decimal ComputeTotalValue() =>
        Lines.Sum(e => e.Product.Price * e.Quantity);
        public virtual void Clear() => Lines.Clear();
    }
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}