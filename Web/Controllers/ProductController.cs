﻿
using Microsoft.AspNetCore.Mvc;
using Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductManager _productManager;

        public ProductController(ProductManager productManager)
        {
            _productManager = productManager;
        }

        public IActionResult Index(string? q,int? categoryId)
        {
            ProductIndexVM vm = new()
            {
                Products = _productManager.SearchProducts(q, categoryId)
            };
            return View(vm);
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var ProductSingle=_productManager.GetById(id);
            if(ProductSingle == null) return NotFound();
            ProductSingleVM vm = new()
            {
                Product = ProductSingle,
            };
            return View(vm);
        }
    }
}
