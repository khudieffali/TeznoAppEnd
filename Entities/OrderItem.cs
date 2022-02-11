namespace Entities
{
    public class OrderItem : BaseEntity
    {
        public decimal itemPrice { get; set; }
        public ushort Quantity { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public int OrderID { get; set; }
        public virtual Order Order { get; set; }
    }
}
