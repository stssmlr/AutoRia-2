using AutoRia.Models;
using Microsoft.AspNetCore.Mvc;
using Data.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;



namespace AutoRia.Controllers
{
    public class HomeController : Controller
    {
        private readonly CarsDbContext context;

        public HomeController(CarsDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            

            var cars = context.Cars
                .Include(x => x.Category)
                .ToList();
            return View(cars);
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
