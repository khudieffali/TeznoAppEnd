

using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Product : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(700)]
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public ushort InStock { get; set; }
        public decimal Discount { get; set; }
        [MaxLength(800)]
        public string? PhotoUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime? ModifiedOn { get; set; }
        [MaxLength(100)]
        public string? SKU { get; set; }
        [MaxLength()]
        public string Barcode { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSlider { get; set; }
        public bool IsWeek { get; set; }
        public bool IsMonth { get; set; }

        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
