using Data.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Dtos
{
    public class CarDto
    {
        public int? Id { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
        public string? Mark { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }
        public int Mileage { get; set; }
        public int FuelTypeId { get; set; }
        public string? FuelTypeName { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool Archived { get; set; }
        public string? Description { get; set; }
    }
}
