using AutoMapper;
using EasyGift_API.Data;
using EasyGift_API.Models;
using EasyGift_API.Models.Dto;
using EasyGift_API.Models.Dto.Create;
using EasyGift_API.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EasyGift_API.Repository
{
    public class CustomerLoginRepository : Repository<CustomerLogin>, ICustomerLoginRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CustomerLoginRepository(ApplicationDbContext db, IMapper mapper) : base(db)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<LoginResponseDTO<CustomerLogin>> Login(LoginRequestDTO requestDTO)
        {
            var cust = _db.CustomerLogin.FirstOrDefault(u => u.EmailId.ToLower() == requestDTO.EmailId.ToLower() && u.password == requestDTO.Password);
            if (cust == null)
                return new LoginResponseDTO<CustomerLogin>() { User = null };
            LoginResponseDTO<CustomerLogin> loginResponseDTO = new LoginResponseDTO<CustomerLogin>()
            {
                User = _mapper.Map<CustomerLogin>(cust)
            };
            return loginResponseDTO;
        }

    }
}
