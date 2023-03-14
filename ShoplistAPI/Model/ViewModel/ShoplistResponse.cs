using ShoplistAPI.Data.DTOs;

namespace ShoplistAPI.Model.ViewModel
{
    public class ShoplistResponse
    {
        public ICollection<ShoplistDTO> Shoplists { get; set; } = new List<ShoplistDTO>();

        public ShoplistResponse()
        {

        }

        public ShoplistResponse(ICollection<ShoplistDTO> shoplists)
        {
            this.Shoplists = shoplists;
        }
    }
}
