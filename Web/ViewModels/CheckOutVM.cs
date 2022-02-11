using Entities;

namespace Web.ViewModels
{
    public class CheckOutVM
    {
        public List<int> ProductIds { get; set; }
        public List<Product> Products { get; set; }
        public string CustomerID { get; set; }
        public string? CustomerFirstName { get; set; }
        public string? CustomerLastName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerAddress { get; set; }
        public decimal? TotalAmount { get; set; }


    }
}
