using ShoplistAPI.Model;

namespace ShoplistAPI.Data.DTOs
{
    public class ShoplistDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
