using EasyGift_API.Models;
using EasyGift_API.Models.Dto.Create;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EasyGift_API.Repository.IRepository
{
    public interface ISellerLoginRepository : IRepository<SellerLogin>
    {
        Task<LoginResponseDTO<SellerLogin>> Login(LoginRequestDTO requestDTO);

    }
}
