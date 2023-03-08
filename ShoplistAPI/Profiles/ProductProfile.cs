using AutoMapper;
using ShoplistAPI.Data.DTOs;
using ShoplistAPI.Model;

namespace ShoplistAPI.Profiles
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
