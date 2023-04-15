using Newtonsoft.Json.Linq;
using System.Net;

namespace EasyGift_API.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorsMessages { get; set; }
        public object Result { get; set; }
    }
}
