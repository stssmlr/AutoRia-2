using AutoMapper;
using Core.Dtos;
using AutoRia.Extensions;

namespace AutoRia.Services
{
    public class CartService
    {
        private readonly HttpContext httpContext;
        public CartService(IHttpContextAccessor contextAccessor)
        {
            httpContext = contextAccessor.HttpContext!;
        }

        public int GetCount()
        {
            var ids = httpContext.Session.Get<List<int>>("liked_items");

            if (ids == null) return 0;

            return ids.Distinct().Count();
        }
    }
}