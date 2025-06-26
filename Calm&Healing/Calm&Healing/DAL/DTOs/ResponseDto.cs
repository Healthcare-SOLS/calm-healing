namespace Calm_Healing.DAL.DTOs
{
    public class ResponseDto<T>
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static ResponseDto<T> Success(string code, string message = null)
        {
            return new ResponseDto<T>
            {
                Code = code,
                Message = message ?? "Success"
            };
        }

        public static ResponseDto<T> WithData(T data, string code = "OK", string message = "Success")
        {
            return new ResponseDto<T>
            {
                Code = code,
                Message = message,
                Data = data
            };
        }
    }
}

