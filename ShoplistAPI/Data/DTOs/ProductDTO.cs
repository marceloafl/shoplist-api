using ShoplistAPI.Model;

namespace ShoplistAPI.Data.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Brand { get; set; }
        public string? Description { get; set; }
        public int Number { get; set; }
        public int? ShoplistId { get; set; }
    }
}
