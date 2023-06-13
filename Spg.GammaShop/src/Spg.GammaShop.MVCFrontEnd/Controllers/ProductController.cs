﻿using Microsoft.AspNetCore.Mvc;
using Spg.GammaShop.Domain.Interfaces.ProductServiceInterfaces;
using Spg.GammaShop.Domain.Models;

namespace Spg.GammaShop.MVCFrontEnd.Controllers
{
    public class ProductController : Controller
    {
        private readonly IAddUpdateableProductService _addUpdateproductService;
        private readonly IReadOnlyProductService _readOnlyproductService;


        public ProductController(IAddUpdateableProductService addUpdateproductService, IReadOnlyProductService readOnlyproductService)
        {
            _addUpdateproductService = addUpdateproductService;
            _readOnlyproductService = readOnlyproductService;
        }

        public IActionResult Index()
        {
            List<Product> model = _readOnlyproductService.GetAll().ToList();
            return View(model);
        }
    }
}
