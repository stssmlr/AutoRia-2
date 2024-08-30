using AutoMapper;
using Core.Interfaces;
using Core.Dtos;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AutoRia.Controllers
{
    [Authorize]
    public class RequestsController : Controller
    {
        private readonly IRequestService requestService;
        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        private string UserEmail => User.FindFirstValue(ClaimTypes.Email)!;

        public RequestsController(IRequestService requestService)
        {
            
            this.requestService = requestService;
        }

        public IActionResult Index()
        {
            return View(requestService.GetRequests(UserId));
        }
        public IActionResult Create()
        {
            // create request
            requestService.Create(UserId, UserEmail);

            return RedirectToAction(nameof(Index));
        }
    }
}
