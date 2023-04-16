using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyGift_API.Models.Dto.Get
{
    public class SubCategoryDTO
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string SubCategoryName { get; set; }
    }
}
