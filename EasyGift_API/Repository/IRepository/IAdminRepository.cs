using EasyGift_API.Models;
using System.Linq.Expressions;

namespace EasyGift_API.Repository.IRepository
{
    public interface IAdminRepository : IRepository<Admin>
    {
        //Task<Admin> UpdateAsync(Admin entity);
        Task<LoginResponseDTO<Admin>> Login(LoginRequestDTO requestDTO);

    }
}
