using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication6.Models
{
    [Table("SanPham")]
    public class SanPham
    {
        [Key]
        public int MaSP { get; set; }
        public string? TenSP { get; set; }
        public string? MoTa { get; set; }
        public decimal Gia { get; set; }
        public int SoLuongTon { get; set; }
        public string? LoaiSP { get; set; }

        public ICollection<ChiTietSanPham>? ChiTietSanPham { get; set; }
    }
}