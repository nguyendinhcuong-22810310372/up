using System.Collections.Generic;

namespace WebApplication6.Models
{
    public class CheckoutViewModel
    {
        public Cart Cart { get; set; }  // tránh null

        public string TenKH { get; set; } = "";        // tránh lỗi
        public string Email { get; set; } = "";        // tránh lỗi
        public string SoDienThoai { get; set; } = "";  // tránh lỗi
        public string DiaChi { get; set; } = "";       // tránh lỗi
    }
}