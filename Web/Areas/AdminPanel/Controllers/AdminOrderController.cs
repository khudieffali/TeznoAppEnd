using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Web.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles ="Admin")]
    public class AdminOrderController : Controller
    {
        private readonly OrderManager _orderManager;

        public AdminOrderController(OrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public IActionResult Index()
        {
            return View(_orderManager.GetOrders());
        }
    }
}
