using WebApplication6.Models;
namespace WebApplication6.Models
{
    public class CheckoutViewModel
    {
        public string TenKH { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }

        public Cart Cart { get; set; }
    }
}