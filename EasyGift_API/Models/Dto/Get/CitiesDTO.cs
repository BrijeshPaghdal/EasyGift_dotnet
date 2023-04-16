using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Get
{
    public class CitiesDTO
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
       
    }
}
