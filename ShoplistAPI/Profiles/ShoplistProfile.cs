using AutoMapper;
using ShoplistAPI.Data.DTOs;
using ShoplistAPI.Model;

namespace ShoplistAPI.Profiles
{
    public class ShoplistProfile: Profile
    {
        public ShoplistProfile()
        {
            CreateMap<ShoplistDTO, Shoplist>().ReverseMap();
        }
    }
}
