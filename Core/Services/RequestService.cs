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

namespace Core.Services
{
    public class RequestService : IRequestService
    {
        private readonly CarsDbContext context;
        private readonly IMapper mapper;
        private readonly ICartService cartService;
        private readonly IEmailSender emailSender;

        public RequestService(CarsDbContext context, IMapper mapper, ICartService cartService, IEmailSender emailSender)
        {
            this.context = context;
            this.mapper = mapper;
            this.cartService = cartService;
            this.emailSender = emailSender;
        }
        public async Task Create(string userId, string userEmail)
        {
            // create request
            var newRequest = new Request()
            {
                CreatedAt = DateTime.Now,
                Cars = cartService.GetCarsEntity(),
                UserId = userId
            };

            context.Requests.Add(newRequest);
            context.SaveChanges();

            await emailSender.SendEmailAsync(userEmail, $"New Request #{newRequest.Id}", "<h1>New Request</h1>");

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