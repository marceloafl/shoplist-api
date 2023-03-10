using System.Text.Json.Serialization;

namespace ShoplistAPI.Model;

public partial class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Brand { get; set; }
    public string? Description { get; set; }
    public int Number { get; set; }
    public int? ShoplistId { get; set; }
    [JsonIgnore]
    public virtual Shoplist? Shoplist { get; set; }
}
