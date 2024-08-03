namespace Data.Entities;
using System.ComponentModel.DataAnnotations;

public class Car
{
    public int Id { get; set; }
    //[Url]
    public string? ImageUrl { get; set; }
    //[Required]
    public string Mark { get; set; }
    //[Required]
    public string Model { get; set; }
    //[Range(0, int.MaxValue)]
    public int Year { get; set; }
    //[Range(0, int.MaxValue)]
    public int Mileage { get; set; }
    //[Range(1, int.MaxValue, ErrorMessage = "Category is required.")]
    public int FuelTypeId { get; set; }
    //[Range(0, int.MaxValue)]
    public decimal Price { get; set; }
    //[Range(0, 100)]
    public int Discount { get; set; }
    //[Range(0, int.MaxValue)]
    public int Quantity { get; set; }
    //[Range(1, int.MaxValue, ErrorMessage = "Category is required.")]
    public int CategoryId { get; set; }
    public bool Archived { get; set; }
    //[MaxLength(255)]
    public string? Description { get; set; }

    // -------- navigation property ----------

    public Category? Category { get; set; }
    public FuelType? FuelType { get; set; }

}

    //Relationship: One To Many