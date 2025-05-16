using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Models;

namespace WebApplication6.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var sanPhams = _context.SanPham
            .Include(sp => sp.ChiTietSanPham)
            .ToList();

        return View(sanPhams);
    }
    public ActionResult GioiThieu()
    {
        return View();
    }
    public ActionResult TinTuc()
    {
        return View();
    }
    public ActionResult Checkout()
    {
        return View();
    }

}