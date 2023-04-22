namespace EasyGift_API.Models
{
    public class LoginResponseDTO<T> where T : class
    {
        public T User { get; set; }

    }
}
