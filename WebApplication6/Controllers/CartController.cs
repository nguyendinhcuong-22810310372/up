using Microsoft.AspNetCore.Mvc;
using WebApplication6.Models;
using Microsoft.EntityFrameworkCore;

namespace VCV_Coffee.Controllers
{
    [Route("GioHang")]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        private void LuuGioHang(Cart cart)
        {
            HttpContext.Session.Set("Cart", cart);
            int tongSoLuong = cart.CartItems.Sum(i => i.Quantity);
            HttpContext.Session.SetInt32("CartItemCount", tongSoLuong);
        }

        private Cart LayGioHang()
        {
            var cart = HttpContext.Session.Get<Cart>("Cart");
            if (cart == null)
            {
                cart = new Cart();
                HttpContext.Session.Set("Cart", cart);
            }
            return cart;
        }

        // GET: /GioHang
        [HttpGet("")]
        public IActionResult TrangGioHang()
        {
            var cart = LayGioHang();
            return View("~/Views/GioHang/Cart.cshtml", cart);
        }

        // POST: /GioHang/ThemVaoGio
        [HttpPost("ThemVaoGio")]
        public IActionResult ThemVaoGio(int productId, int soLuong = 1)
        {
            var product = _context.SanPham
                .Include(p => p.ChiTietSanPham)
                .FirstOrDefault(p => p.MaSP == productId);

            if (product == null)
            {
                return NotFound();
            }

            var cart = LayGioHang();
            var item = cart.CartItems.FirstOrDefault(i => i.SanPham.MaSP == productId);

            if (item != null)
                item.Quantity += soLuong;
            else
                cart.CartItems.Add(new CartItem { SanPham = product, Quantity = soLuong });

            LuuGioHang(cart);
            return RedirectToAction(nameof(TrangGioHang));
        }

        // POST: /GioHang/CapNhatSoLuong
        [HttpPost("CapNhatSoLuong")]
        public IActionResult CapNhatSoLuong(int productId, int change)
        {
            var cart = LayGioHang();
            var item = cart.CartItems.FirstOrDefault(i => i.SanPham.MaSP == productId);

            if (item != null)
            {
                item.Quantity += change;
                if (item.Quantity <= 0)
                    cart.CartItems.Remove(item);
            }

            LuuGioHang(cart);
            return RedirectToAction(nameof(TrangGioHang));
        }

        // POST: /GioHang/XoaSanPham
        [HttpPost("XoaSanPham")]
        public IActionResult XoaSanPham(int productId)
        {
            var cart = LayGioHang();
            var item = cart.CartItems.FirstOrDefault(i => i.SanPham.MaSP == productId);

            if (item != null)
                cart.CartItems.Remove(item);

            LuuGioHang(cart);
            return RedirectToAction(nameof(TrangGioHang));
        }

        // GET: /GioHang/Checkout
        [HttpGet("Checkout")]
        public IActionResult Checkout()
        {
            var cart = LayGioHang();
            if (cart == null || cart.CartItems.Count == 0)
            {
                Console.WriteLine("Empty cart or not found in session");
                return RedirectToAction(nameof(TrangGioHang));
            }

            var model = new CheckoutViewModel
            {
                Cart = cart
            };

            Console.WriteLine("Checkout model ready with " + model.Cart.CartItems.Count + " items");

            return View("~/Views/GioHang/Checkout.cshtml", model);
        }

    }
}