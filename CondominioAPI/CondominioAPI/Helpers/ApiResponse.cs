namespace CondominioAPI.Helpers
{
    public class ApiResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ApiResponse(int status, string message, object data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }

}
