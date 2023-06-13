using Spg.GammaShop.Domain.DTO;
using Spg.GammaShop.Domain.Interfaces.Generic_Repository_Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.Domain.Models

{
    public enum Rating { SehrGut, Gut, Mittel, Schlecht, SehrSchlecht }

    public class Product
    {
        public int Id { get; private set; }
        public Guid Guid { get;  set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Catagory? catagory { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
        public Rating Rating { get; set; }
        public int Stock { get; set; }
        public int Discount { get; set; }
        public DateTime receive { get; set; }


        public Product(int id, Guid guid, string name, decimal price, Catagory? catagory, string description, string? image, Rating rating, int stock, int discount, DateTime receive)
        {
            Id = id;
            Guid = guid;
            Name = name;
            Price = price;
            this.catagory = catagory;
            Description = description;
            Image = image;
            Rating = rating;
            Stock = stock;
            Discount = discount;
            this.receive = receive;
        }

        public Product( Guid guid, string name, decimal price, Catagory? catagory, string description, string? image, Rating rating, int stock, int discount, DateTime receive)
        {
            Guid = guid;
            Name = name;
            Price = price;
            this.catagory = catagory;
            Description = description;
            Image = image;
            Rating = rating;
            Stock = stock;
            Discount = discount;
            this.receive = receive;
        }

        public Product()
        {
        }

        public Product(ProductDTO pDTO)
        {
            this.Name = pDTO.Name;
            this.Price = pDTO.Price;
            this.catagory = pDTO.catagory;
            this.Description = pDTO.Description;
            this.Image = pDTO.Image;
            this.Rating = pDTO.Rating;
            this.Stock = pDTO.Stock;
            this.Discount = pDTO.Discount;
        }



        public override bool Equals(object? obj)
        {
            return obj is Product product &&
                   Id == product.Id &&
                   Guid.Equals(product.Guid) &&
                   Name == product.Name &&
                   Price == product.Price &&
                   EqualityComparer<Catagory?>.Default.Equals(catagory, product.catagory) &&
                   //Description == product.Description &&
                   Image == product.Image &&
                   Rating == product.Rating &&
                   Stock == product.Stock &&
                   Discount == product.Discount &&
                   receive == product.receive;
                   //EqualityComparer<List<Car>>.Default.Equals(_productFitsForCar, product._productFitsForCar) &&
                   //EqualityComparer<IReadOnlyList<Car>>.Default.Equals(ProductFitsForCar, product.ProductFitsForCar);
        }
    }
}
