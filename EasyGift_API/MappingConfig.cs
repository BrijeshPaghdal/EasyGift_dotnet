using AutoMapper;
using EasyGift_API.Models;
using EasyGift_API.Models.Dto.Create;
using EasyGift_API.Models.Dto.Get;
using EasyGift_API.Models.Dto.Update;

namespace EasyGift_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();

            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<Address, CreateAddressDTO>().ReverseMap();
            CreateMap<Address, UpdateAddressDTO>().ReverseMap();
            
            CreateMap<Admin, AdminDTO>().ReverseMap();
            CreateMap<Admin, CreateAdminDTO>().ReverseMap();
            CreateMap<Admin, UpdateAdminDTO>().ReverseMap();
            
            CreateMap<BankDetails, BankDetailsDTO>().ReverseMap();
            CreateMap<BankDetails, CreateBankDetailsDTO>().ReverseMap();
            CreateMap<BankDetails, UpdateBankDetailsDTO>().ReverseMap();

            CreateMap<Seller, SellerDTO>().ReverseMap();
            CreateMap<Seller, CreateSellerDTO>().ReverseMap();
            CreateMap<Seller, SellerDTO>().ReverseMap();
            CreateMap<CreateNewSellerDTO, Seller>();

            CreateMap<SellerLogin, SellerLoginDTO>().ReverseMap();
            CreateMap<SellerLogin, CreateSellerLoginDTO>().ReverseMap();
            CreateMap<SellerLogin, UpdateSellerLoginDTO>().ReverseMap();
            
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();
            
            CreateMap<Cities, CitiesDTO>().ReverseMap();
            CreateMap<Cities, CreateCitiesDTO>().ReverseMap();
            CreateMap<Cities, UpdateCitiesDTO>().ReverseMap();
             
            CreateMap<Countries, CountriesDTO>().ReverseMap();
            CreateMap<Countries, CreateCountriesDTO>().ReverseMap();
            CreateMap<Countries, UpdateCountriesDTO>().ReverseMap();
              
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Customer, CreateCustomerDTO>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDTO>().ReverseMap();


        }
    }
}
