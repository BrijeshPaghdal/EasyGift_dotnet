using AutoMapper;
using Azure;
using EasyGift_API.Data;
using EasyGift_API.Models;
using EasyGift_API.Models.Dto;
using EasyGift_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace EasyGift_API.Repository
{
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public AdminRepository(ApplicationDbContext db,IMapper mapper) : base(db)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<LoginResponseDTO<Admin>> Login(LoginRequestDTO requestDTO)
        {
            var admin = _db.Admin.FirstOrDefault(u => u.AdminEmail.ToLower() == requestDTO.EmailId.ToLower() && u.AdminPassword == requestDTO.Password);
            if (admin == null)
                return new LoginResponseDTO<Admin>() { User = null };
            LoginResponseDTO<Admin> loginResponseDTO = new LoginResponseDTO<Admin>()
            {
                User = _mapper.Map<Admin>(admin)
            };
            return loginResponseDTO;
        }

        //public async Task<Admin> UpdateAsync(Admin entity)
        //{
        //    _db.Admin.Update(entity);
        //    await _db.SaveChangesAsync();
        //    return entity;
        //}
    }
}
