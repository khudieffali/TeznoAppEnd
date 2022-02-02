using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ProductManager _productManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly OrderManager _orderManager;
        public OrdersController(ProductManager productManager, UserManager<IdentityUser> userManager, OrderManager orderManager)
        {
            _productManager = productManager;
            _userManager = userManager;
            _orderManager = orderManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CheckOut()
        {
            var ProductIdList = Request.Cookies["cartItem"];
            List<Product> ProductList = null;
            CheckOutVM vm = new();
            if (ProductIdList != null && ProductIdList != "" )
            {
                List<int> ProductIds=ProductIdList.Split("-").Select(x=>int.Parse(x)).ToList();
                ProductList = _productManager.GetByIds(ProductIds.Distinct());
                vm.Products = ProductList;
                vm.ProductIds = ProductIds;
               var selectedUser= await _userManager.FindByNameAsync(User.Identity.Name);
                if(selectedUser != null)
                {
                    vm.CustomerID = selectedUser.Id;
                    vm.CustomerName = selectedUser.UserName;
                    vm.CustomerEmail = selectedUser.Email;
                    vm.CustomerPhone = selectedUser.PhoneNumber;
                }
             return View(vm);
            }
           return View();
        }
        [HttpPost]
        public IActionResult CheckOut(CheckOutVM checkOut)
        {
           
            Order newOrder = new();
            var ProductIdList = Request.Cookies["cartItem"];
            List<Product> ProductList = null;
            if (ProductIdList != null && ProductIdList != "")
            {
                List<int> ProductIds = ProductIdList.Split("-").Select(x => int.Parse(x)).ToList();
                ProductList = _productManager.GetByIds(ProductIds.Distinct());
                newOrder.CustomerAddress = checkOut.CustomerAddress;
                newOrder.CustomerPhone=checkOut.CustomerPhone;
                newOrder.CustomerEmail = checkOut.CustomerEmail;
                newOrder.CustomerID= checkOut.CustomerID;
                newOrder.CustomerName = checkOut.CustomerName;  
                newOrder.TotalAmount = checkOut.TotalAmount;
                newOrder.OrderCode = Guid.NewGuid().ToString();
                newOrder.PlacedOn = DateTime.Now;
                newOrder.OrderItems = new List<OrderItem>();
                newOrder.OrderItems.AddRange(ProductList.Select(x =>
                new OrderItem()
                {
                    ProductID = x.ID,
                    itemPrice = x.Price,
                    Quantity = (ushort)ProductIds.Where(p => p == x.ID).Count(),
                    OrderID = newOrder.ID
                })
              );
            }
            _orderManager.Add(newOrder);
            return View("Index");
        }
    }
}
