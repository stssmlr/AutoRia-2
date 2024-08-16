using AutoMapper;
using Azure.Core;
using Core.Dtos;
using Core.Interfaces;
using Data.Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Request = Data.Entities.Request;

namespace Core.Services
{
    public class RequestService : IRequestService
    {
        private readonly CarsDbContext context;
        private readonly IMapper mapper;
        private readonly ICartService cartService;

        public RequestService(CarsDbContext context, IMapper mapper, ICartService cartService)
        {
            this.context = context;
            this.mapper = mapper;
            this.cartService = cartService;
        }
        public void Create(string userId)
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