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
    public class SellerOnlineRepository : Repository<SellerOnline>, ISellerOnlineRepository
    {
        private readonly ApplicationDbContext _db;
        public SellerOnlineRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
