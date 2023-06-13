using Spg.GammaShop.Domain.Models;

namespace Spg.GammaShop.Domain.DTO
{
    public class ProductDTOFilter
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int? catagoryId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
        public Rating Rating { get; set; }
        public int Stock { get; set; }
        public int Discount { get; set; }

        public ProductDTOFilter(Product product)
        {
            this.Id = product.Id;
            this.Guid = product.Guid;
            this.Name = product.Name;
            this.Price = product.Price;
            this.catagoryId = product.catagory.Id;
            this.Description = product.Description;
            this.Image = product.Image;
            this.Rating = product.Rating;
            this.Stock = product.Stock;
            this.Discount = product.Discount;
        }
    }
    
}
