namespace WebApplication6.Models

{
    public class Cart
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public decimal Total => CartItems.Sum(i => i.SanPham.MaSP * i.Quantity);
    }

    public class CartItem
    {
        public SanPham SanPham { get; set; } = new SanPham();  // đảm bảo không null
        public int Quantity { get; set; }
    }
}
