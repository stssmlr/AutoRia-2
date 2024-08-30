using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Entities;
using Data.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using AutoRia.SeedExtensions;
using Core.Interfaces;

namespace AutoRia.Controllers
{
    [Authorize(Roles = Roles.ADMIN)]
    public class CarsController : Controller
    {
        private readonly IMapper mapper;
        private readonly CarsDbContext ctx;
        private readonly IFilesService filesService;

        public CarsController(IMapper mapper, CarsDbContext ctx, IFilesService filesService)
        {
            this.mapper = mapper;
            this.ctx = ctx;
            this.filesService = filesService;
        }

        // -+-+-+-+-+-+-+-+-+-+-+-+- INDEX -+-+-+-+-+-+-+-+-+-+-+-+-
        public IActionResult Index()
        {
            var cars = ctx.Cars
                .Include(x => x.Category)
                .Include(x => x.FuelType)
                .Where(x => !x.Archived)
                .ToList();
            return View(mapper.Map<List<CarDto>>(cars));
        }

        // -+-+-+-+-+-+-+-+-+-+-+-+- ARCHIVE -+-+-+-+-+-+-+-+-+-+-+-+-
        public IActionResult Archive()
        {
            // .. load data from database ..
            var cars = ctx.Cars
                .Include(x => x.Category) // LEFT JOIN
                .Where(x => x.Archived)
                .ToList();
            return View(mapper.Map<List<CarDto>>(cars));
            
        }

        public IActionResult ArchiveItem(int id)
        {
            var cars = ctx.Cars.Find(id);

            if (cars == null) return NotFound();

            cars.Archived = true;
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        // -+-+-+-+-+-+-+-+-+-+-+-+- DELETE -+-+-+-+-+-+-+-+-+-+-+-+-
        public async Task<IActionResult> Delete(int id)
        {
            var car = ctx.Cars.Find(id);

            if (car == null) return NotFound();

            //delete image
            if (car.ImageUrl != null)
                await filesService.DeleteCarImage(car.ImageUrl);

            ctx.Cars.Remove(car);
            ctx.SaveChanges();

            return RedirectToAction("Archive");
        }

        // -+-+-+-+-+-+-+-+-+-+-+-+- RESTORE FROM ARCHIEVE -+-+-+-+-+-+-+-+-+-+-+-+-
        public IActionResult RestoreItem(int id)
        {
            var car = ctx.Cars.Find(id);

            if (car == null) return NotFound();

            car.Archived = false;
            ctx.SaveChanges();

            return RedirectToAction("Archive");
        }

        // -+-+-+-+-+-+-+-+-+-+-+-+- CREATE -+-+-+-+-+-+-+-+-+-+-+-+-
        [HttpGet]
        public IActionResult Create()
        {
            LoadTypesOfFuel();
            LoadCategories();
            ViewBag.CreateMode = true;
            return View("Upsert");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarDto car)
        {
            // TODO: add data validation
            if (!ModelState.IsValid)
            {
                ViewBag.CreateMode = true;
                LoadTypesOfFuel();
                LoadCategories();
                return View("Upsert", car);
            }

            var entity = mapper.Map<Car>(car);

            entity.ImageUrl = await filesService.SaveCarImage(car.Image);

            ctx.Cars.Add(entity);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        // -+-+-+-+-+-+-+-+-+-+-+-+- EDIT -+-+-+-+-+-+-+-+-+-+-+-+-
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var car = ctx.Cars.Find(id);

            if (car == null) return NotFound();

            LoadTypesOfFuel();
            LoadCategories();
            ViewBag.CreateMode = false;
            return View("Upsert", mapper.Map<CarDto>(car));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarDto car)
        {
            // TODO: add data validation
            if (!ModelState.IsValid)
            {
                ViewBag.CreateMode = true;
                LoadTypesOfFuel();
                LoadCategories();
                return View("Upsert", car);
            }

            if (car.Image != null)
            {
                car.ImageUrl = await filesService.EditCarImage(car.ImageUrl, car.Image);
            }

            ctx.Cars.Update(mapper.Map<Car>(car));
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        // -+-+-+-+-+-+-+-+-+-+-+-+- DEATILS -+-+-+-+-+-+-+-+-+-+-+-+-
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var product = ctx.Cars.Find(id);

            if (product == null) return NotFound();

            return View(mapper.Map<CarDto>(product));
        }

        // -+-+-+-+-+-+-+-+-+-+-+-+- FUNCTIONS -+-+-+-+-+-+-+-+-+-+-+-+-
        private void LoadCategories()
        {
            // ViewBag.PropertyName = value;
            ViewBag.Categories = new SelectList(ctx.Category.ToList(), "Id", "Name");
        }
        private void LoadTypesOfFuel()
        {
            // ViewBag.PropertyName = value;
            ViewBag.FuelTypes = new SelectList(ctx.FuelType.ToList(), "Id", "Name");
        }
    }
}
