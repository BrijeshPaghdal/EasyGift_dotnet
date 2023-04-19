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

            CreateMap<CustomerLogin, CustomerLoginDTO>().ReverseMap();
            CreateMap<CustomerLogin, CreateCustomerLoginDTO>().ReverseMap();
            CreateMap<CustomerLogin, UpdateCustomerLoginDTO>().ReverseMap();

            CreateMap<ForgotPassword, ForgotPasswordDTO>().ReverseMap();
            CreateMap<ForgotPassword, CreateForgotPasswordDTO>().ReverseMap();
            CreateMap<ForgotPassword, UpdateForgotPasswordDTO>().ReverseMap();

            CreateMap<Image, ImageDTO>().ReverseMap();
            CreateMap<Image, CreateImageDTO>().ReverseMap();
            CreateMap<Image, UpdateImageDTO>().ReverseMap();

            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, CreateOrderDTO>().ReverseMap();
            CreateMap<Order, UpdateOrderDTO>().ReverseMap();

            CreateMap<OrderComplete, OrderCompleteDTO>().ReverseMap();
            CreateMap<OrderComplete, CreateOrderCompleteDTO>().ReverseMap();
            CreateMap<OrderComplete, UpdateOrderCompleteDTO>().ReverseMap();

            CreateMap<PaymentType, PaymentTypeDTO>().ReverseMap();
            CreateMap<PaymentType, CreatePaymentTypeDTO>().ReverseMap();
            CreateMap<PaymentType, UpdatePaymentTypeDTO>().ReverseMap();

            CreateMap<ProductSuggestion, ProductSuggestionDTO>().ReverseMap();
            CreateMap<ProductSuggestion, CreateProductSuggestionDTO>().ReverseMap();
            CreateMap<ProductSuggestion, UpdateProductSuggestionDTO>().ReverseMap();

            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<Review, CreateReviewDTO>().ReverseMap();
            CreateMap<Review, UpdateReviewDTO>().ReverseMap();

            CreateMap<SellerBankDetails, SellerBankDetailsDTO>().ReverseMap();
            CreateMap<SellerBankDetails, CreateSellerBankDetailsDTO>().ReverseMap();
            CreateMap<SellerBankDetails, UpdateSellerBankDetailsDTO>().ReverseMap();

            CreateMap<SellerForgotPassword, SellerForgotPasswordDTO>().ReverseMap();
            CreateMap<SellerForgotPassword, CreateSellerForgotPasswordDTO>().ReverseMap();
            CreateMap<SellerForgotPassword, UpdateSellerForgotPasswordDTO>().ReverseMap();


        }
    }
}
