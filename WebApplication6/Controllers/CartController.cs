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
            return RedirectToAction(nameof(Cart));
        }
        [HttpGet("Checkout")]
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObjectFromJson<Cart>("Cart");
            if (cart == null || !cart.CartItems.Any())
            {
                return RedirectToAction("TrangGioHang");
            }

            var viewModel = new CheckoutViewModel
            {
                Cart = cart
            };

            return View("~/Views/Home/Checkout.cshtml", viewModel);
        }
        private Cart LayGioHang()
        {
            var cart = HttpContext.Session.GetObjectFromJson<Cart>("Cart");
            return cart;
        }

        private void LuuGioHang(Cart cart)
        {
            HttpContext.Session.Set("Cart", cart);
            int tongSoLuong = cart.CartItems.Sum(i => i.Quantity);
            HttpContext.Session.SetInt32("CartItemCount", tongSoLuong);
        }
    }
}