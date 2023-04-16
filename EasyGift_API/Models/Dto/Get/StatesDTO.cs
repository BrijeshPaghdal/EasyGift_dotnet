using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Get
{
    public class StatesDTO
    {
        public int Id { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
       
    }
}
