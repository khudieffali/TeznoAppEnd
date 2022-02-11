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
        private readonly UserManager<ECommerseUser> _eCommerseUser;
        private readonly OrderManager _orderManager;
        public OrdersController(ProductManager productManager, UserManager<ECommerseUser> userManager, OrderManager orderManager, UserManager<ECommerseUser> eCommerseUser)
        {
            _productManager = productManager;
            _orderManager = orderManager;
            _eCommerseUser = eCommerseUser;
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
            if (ProductIdList != null && ProductIdList != "")
            {
                List<int> ProductIds = ProductIdList.Split("-").Select(x => int.Parse(x)).ToList();
                ProductList = _productManager.GetByIds(ProductIds.Distinct());
                vm.Products = ProductList;
                vm.ProductIds = ProductIds;
                var selectedUser = await _eCommerseUser.GetUserAsync(User);
                if (selectedUser != null)
                {
                    vm.CustomerID = selectedUser.Id;
                    vm.CustomerFirstName = selectedUser.FisrtName;
                    vm.CustomerLastName = selectedUser.LastName;
                    vm.CustomerEmail = selectedUser.Email;
                    vm.CustomerPhone = selectedUser.PhoneNumber;
                    vm.CustomerAddress = selectedUser.Address;
                }
                else
                {
                    return RedirectToAction("Login","Account", new {area="Identity" });
                }
                return View(vm);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CheckOut(CheckOutVM checkOut)
        {
            Order newOrder = new();
            var ProductIdList = Request.Cookies["cartItem"];
            List<Product> ProductList = null;
            if (ProductIdList != null && ProductIdList != "")
            {
                List<int> ProductIds = ProductIdList.Split("-").Select(x => int.Parse(x)).ToList();
                ProductList = _productManager.GetByIds(ProductIds.Distinct());
                var selectedUser = await _eCommerseUser.GetUserAsync(User);
                if (selectedUser != null)
                {
                    newOrder.CustomerName = selectedUser.UserName;
                    newOrder.CustomerAddress = checkOut.CustomerAddress;
                    newOrder.CustomerPhone = checkOut.CustomerPhone;
                    newOrder.CustomerEmail = selectedUser.Email;
                    newOrder.CustomerID = selectedUser.Id;
                    newOrder.TotalAmount = (decimal)checkOut.TotalAmount;
                    newOrder.OrderCode = Guid.NewGuid().ToString();
                    newOrder.PlacedOn = DateTime.Now;
                    newOrder.OrderItems = new List<OrderItem>();
                    newOrder.OrderItems.AddRange(ProductList.Select(x =>
                    new OrderItem()
                    {
                        ProductID = x.ID,
                        itemPrice = x.Price,
                        Quantity = (ushort)ProductIds.Where(p => p == x.ID).Count(),
                        OrderID = newOrder.ID,


                    })



                  );
                    newOrder.TotalAmount = newOrder.OrderItems.Select(c => c.Quantity * c.itemPrice).Sum();
                }
                _orderManager.Add(newOrder);
                Response.Cookies.Delete("cartItem");
            }
            return View("Index");
        }
    }
}
