﻿using Spg.GammaShop.Domain.DTO;
using Spg.GammaShop.Domain.Interfaces.Generic_Repository_Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.Domain.Models
{
    public enum CategoryTypes
    {
        Shake, Pulver, Bar, Tabletten, Drops
    }
    
    public class Catagory
    {
        public int Id { get; private set; }
        public Catagory? TopCatagory{ get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public CategoryTypes CategoryType { get; set; }

        public Catagory(int id, Catagory? topCatagory, string name, string description, CategoryTypes categoryType)
        {
            Id = id;
            TopCatagory = topCatagory;
            Name = name;
            Description = description;
            CategoryType = categoryType;
        }

        public Catagory(Catagory? topCatagory, string name, string description, CategoryTypes categoryType)
        {
            TopCatagory = topCatagory;
            Name = name;
            Description = description;
            CategoryType = categoryType;
        }

        public Catagory()
        {
        }

        public Catagory(CatagoryPostDTO catagoryPostDTO, Catagory topCatagory)
        {
            Name = catagoryPostDTO.Name;
            Description = catagoryPostDTO.Description;
            CategoryType = catagoryPostDTO.CategoryType;
            TopCatagory = topCatagory;
        }
    }
}
