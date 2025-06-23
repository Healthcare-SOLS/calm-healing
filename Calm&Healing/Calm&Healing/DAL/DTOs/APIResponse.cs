namespace Calm_Healing.DAL.DTOs
{
    public class APIResponse<T>
    {
        public DateTime Date { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; } 
    }
}
