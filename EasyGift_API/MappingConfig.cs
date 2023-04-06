using AutoMapper;
using EasyGift_API.Models;
using EasyGift_API.Models.Dto;

namespace EasyGift_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();

        }
    }
}
