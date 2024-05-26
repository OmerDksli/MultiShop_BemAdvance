using System.Text.Json.Serialization;

namespace MultiShop.Data
{
    public class Category : BaseEntity, IOrdered, IImage
    {
        public int CategoryId { get; set; }
        public int? ParentCategoryId { get; set; }
        public required string CategoryName { get; set; }
        public string? Image { get; set; }
        public int? Order { get; set; }
        public ICollection<Product> Products { get; set; }
    }

}
