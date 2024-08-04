using AutoMapper;
using AutoRia.Extensions;
using Core.Dtos;
using Data.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoRia.Controllers
{
    public class CartController : Controller
    {
        private CarsDbContext context = new();
        private readonly IMapper mapper;

        public CartController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var ids = HttpContext.Session.Get<List<int>>("liked_items") ?? new();

            var cars = context.Cars.Include(x => x.Category).Where(x => ids.Contains(x.Id)).ToList();

            return View(mapper.Map<List<CarDto>>(cars));
        }

        public IActionResult Add(int id)
        {
            var ids = HttpContext.Session.Get<List<int>>("liked_items");

            if (ids == null) ids = new();

            ids.Add(id);

            HttpContext.Session.Set("liked_items", ids);

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var ids = HttpContext.Session.Get<List<int>>("liked_items");

            if (ids == null || !ids.Contains(id)) return NotFound();

            ids.Remove(id);

            HttpContext.Session.Set("liked_items", ids);

            return RedirectToAction("Index");
        }
    }
}
