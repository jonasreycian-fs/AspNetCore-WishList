using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WishList.Data;
using WishList.Models;

namespace WishList.Controllers
{
    [Route("[controller]")]
    public class ItemController : Controller
    {


        private readonly ILogger<ItemController> _logger;
        private readonly ApplicationDbContext _context;

        public ItemController(ILogger<ItemController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this._context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create([FromBody] Item item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var itemToDelete = _context.Items.FirstOrDefault((x) => x.Id == id);
            if (itemToDelete != null)
            {
                return NotFound();
            }
            _context.Items.Remove(itemToDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
