using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Get
{
    public class CountriesDTO
    {
        public int Id { get; set; }
        public string SortName { get; set; }
        public string CountryName { get; set; }
        public int PhoneCode { get; set; }
       
    }
}
