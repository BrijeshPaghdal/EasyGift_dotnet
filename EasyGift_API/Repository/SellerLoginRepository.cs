using AutoMapper;
using EasyGift_API.Data;
using EasyGift_API.Models;
using EasyGift_API.Models.Dto;
using EasyGift_API.Models.Dto.Create;
using EasyGift_API.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EasyGift_API.Repository
{
    public class SellerLoginRepository : Repository<SellerLogin>, ISellerLoginRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public SellerLoginRepository(ApplicationDbContext db, IMapper mapper) : base(db)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<LoginResponseDTO<SellerLogin>> Login(LoginRequestDTO requestDTO)
        {
            var seller = _db.SellerLogin.FirstOrDefault(u => u.EmailId.ToLower() == requestDTO.EmailId.ToLower() && u.password == requestDTO.Password);
            if (seller == null)
                return new LoginResponseDTO<SellerLogin>() { User = null };
            LoginResponseDTO<SellerLogin> loginResponseDTO = new LoginResponseDTO<SellerLogin>()
            {
                User = _mapper.Map<SellerLogin>(seller)
            };
            return loginResponseDTO;
        }

    }
}
