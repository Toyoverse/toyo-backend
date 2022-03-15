namespace BackendToyo.Models.ResponseEntities
{
    public class ResponseStatusEntity
    {
        public ResponseStatusEntity(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}