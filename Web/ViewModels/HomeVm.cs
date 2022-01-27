using Entities;

namespace Web.ViewModels
{
    public class HomeVm
    {
        public List<Product>? SliderList { get; set; }
        public List<Product>? ProductList { get; set; }
        public List<Product> WeekProducts { get; set; }
        public List<Product> MonthProducts { get; set; }
        public List<Product> NewProducts { get; set; }


    }
}
