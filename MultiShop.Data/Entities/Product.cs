using System.Text.Json.Serialization;

namespace MultiShop.Data
{
    public class Product : BaseEntity, IOrdered, IImage
    {
        public int ProductID { get; set; }
        public required string ProductName { get; set; }
        public decimal? Price { get; set; }
        public decimal DistcountedPrice { get; set; }
        public short? StockQuantity { get; set; }
        public int? Order { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public int CategoryID { get; set; }
        
        public Category Category { get; set; }//Navigation property
        public ICollection<ProductRating> ProductRatings { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
//İlişki kurmak için 3 farklı yöntem  var 
//Default convention 
//Attribute
//OnModel Creating tarafında yapmak 