using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mobi.DbContext;
using Mobi.Entities;
using Mobi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mobi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public AppDbContext DbContext { get; }

        public HomeController(ILogger<HomeController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            DbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Products()
        {
            ViewBag.Category = await DbContext.Categories.ToListAsync();
            return View();
        }

        public async Task<IActionResult> ProductDetail(int productId)
        {
            var product = await DbContext.Products.Include(p=>p.Category).FirstOrDefaultAsync(p => p.Id == productId);

            if (product==null)
            {
                return RedirectToAction("Products");
            }
            return View(product);
        }

        public async Task<IActionResult> MyFavorites()
        {
            ViewBag.Category = await DbContext.Categories.ToListAsync();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
