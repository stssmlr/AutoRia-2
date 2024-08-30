using AutoMapper;
using Azure.Core;
using Core.Dtos;
using Core.Interfaces;
using Data.Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Request = Data.Entities.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Core.Models;

namespace Core.Services
{
    public class RequestService : IRequestService
    {
        private readonly CarsDbContext context;
        private readonly IMapper mapper;
        private readonly ICartService cartService;
        private readonly IEmailSender emailSender;
        private readonly IViewRender viewRender;

        public RequestService(
            CarsDbContext context,
            IMapper mapper,
            ICartService cartService,
            IEmailSender emailSender,
            IViewRender viewRender
            )
        {
            this.context = context;
            this.mapper = mapper;
            this.cartService = cartService;
            this.emailSender = emailSender;
            this.viewRender = viewRender;
        }
        public async Task Create(string userId, string userEmail)
        {
            var cars = cartService.GetCarsEntity();
            var carsDtos = mapper.Map<IEnumerable<CarDto>>(cars);
            // create request
            var newRequest = new Request()
            {
                CreatedAt = DateTime.Now,
                Cars = cars,
                UserId = userId
            };

            context.Requests.Add(newRequest);
            context.SaveChanges();

            var html = viewRender.Render("MailTemplates/StyledSummary", new RequestModel
            {
                RequestNumber = newRequest.Id,
                UserName = userEmail,
                Cars = carsDtos,
                TotalPrice = carsDtos.Sum(i => i.Price)
            });

            await emailSender.SendEmailAsync(userEmail, $"New Request #{newRequest.Id}", html);

            cartService.Clear();
        }

        public List<RequestDto> GetRequests(string userId)
        {
            var orders = context.Requests.Include(x => x.User)
                                        .Where(x => x.UserId == userId)
                                        .ToList();
            return mapper.Map<List<RequestDto>>(orders);
        }
    }
}