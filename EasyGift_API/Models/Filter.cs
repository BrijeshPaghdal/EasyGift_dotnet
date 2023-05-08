using System.ComponentModel.DataAnnotations;

namespace EasyGift_API.Models
{
    public class Filter
    {

        public string? CityName { get; set; } = null;
        public int? MinPrice { get; set; } = null;
        public int? MaxPrice { get; set; } = null;
        public string? CategoryName { get; set; } = null;
        public string? SubCategoryName { get; set; } = null;
        public string? CategoryIds { get; set; } = null;
        public string? SuggestionIds { get; set; } = null;

    }
}
