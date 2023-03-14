using ShoplistAPI.Data.DTOs;

namespace ShoplistAPI.Model.ViewModel
{
    public class ProductResponse
    {
        public ICollection<ProductDTO> Products { get; set;} = new List<ProductDTO>();

        public ProductResponse()
        {

        }
        public ProductResponse(ICollection<ProductDTO> products)
        {
            Products = products;
        }
    }
}
