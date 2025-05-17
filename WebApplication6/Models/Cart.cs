using System.Collections.Generic;
using System.Linq;

namespace WebApplication6.Models
{
    public class Cart
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public decimal Total => CartItems.Sum(i => i.SanPham.Gia * i.Quantity); // ✅ Dùng Giá thay vì MaSP
    }

    public class CartItem
    {
        public SanPham SanPham { get; set; } = new SanPham(); // ✅ Không null
        public int Quantity { get; set; }
    }
}