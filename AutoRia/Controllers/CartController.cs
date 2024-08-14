using AutoMapper;
using AutoRia.Extensions;
using Core.Dtos;
using Data.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoRia.Services;
using Microsoft.EntityFrameworkCore;

namespace AutoRia.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService cartService;

        public CartController(CartService cartService)
        {
             this.cartService = cartService;
        }

        public IActionResult Index()
        {
            return View(cartService.GetProducts());
        }

        public IActionResult Add(int id, string? returnUrl)
        {
            cartService.AddItem(id);
            return Redirect(returnUrl ?? "/");
        }

        public IActionResult Remove(int id)
        {
            cartService.RemoveItem(id);
            return RedirectToAction("Index");
        }
    }
}
