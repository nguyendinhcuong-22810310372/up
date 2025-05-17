using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication6.Models
{
    [Table("ChiTietSanPham")]
    public class ChiTietSanPham
    {
        [Key]
        public int MaCTSP { get; set; }
        public int MaSP { get; set; }
        public string? MoTaChiTiet { get; set; }
        public string? HinhAnh { get; set; }

        [ForeignKey("MaSP")]
        public SanPham? SanPham { get; set; }
    }
}