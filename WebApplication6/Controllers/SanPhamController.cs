using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class SanPhamController : Controller
{
    private readonly ApplicationDbContext _context;

    public SanPhamController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult ChiTiet(int id)
    {
        var sanPham = _context.SanPham
            .Include(sp => sp.ChiTietSanPham)
            .FirstOrDefault(sp => sp.MaSP == id);

        if (sanPham == null)
            return NotFound();

        return View(sanPham);
    }
}

