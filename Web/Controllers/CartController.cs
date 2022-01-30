using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductManager _productManager;

        public CartController(ProductManager productManager)
        {
            _productManager = productManager;
        }

        public IActionResult Index()
        {
            var ProductIdList = Request.Cookies["cartItem"];
            List<Product> productList = null;
            if (ProductIdList != null && ProductIdList != "")
            {
                List<int> ProductIds = ProductIdList.Split("-").Select(x => int.Parse(x)).ToList();
                productList= _productManager.GetByIds(ProductIds.Distinct());
            }   

            return View(ProductIdList);
        }
    }
}
