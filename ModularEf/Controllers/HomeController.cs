using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModularEf.Models;

namespace ModularEf.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ItemService _itemService;
        private readonly DbContext _context;

        public HomeController(ILogger<HomeController> logger, ItemService itemService, DbContext context)
        {
            _logger = logger;
            _itemService = itemService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            await _itemService.Create();
            var items = _context.Set<Item>().ToList();
            return View(items);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}