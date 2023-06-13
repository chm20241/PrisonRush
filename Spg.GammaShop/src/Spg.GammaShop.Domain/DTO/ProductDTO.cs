using Spg.GammaShop.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Spg.GammaShop.Domain.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        [Required()]
        [MaxLength(20, ErrorMessage = "Name darf nicht länger als 20 Zeichen sein")]
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public   Catagory? catagory { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
        public Rating Rating { get; set; }
        public int Stock { get; set; }
        public int Discount { get; set; }
        

        public ProductDTO(Product product)
        {
            this.Id = product.Id;
            this.Guid = product.Guid;
            this.Name = product.Name;
            this.Price = product.Price;
            this.catagory = product.catagory;
            this.Description = product.Description;
            this.Image = product.Image;
            this.Rating = product.Rating;
            this.Stock = product.Stock;
            this.Discount = product.Discount;
        }
    }
    
}
