using System.Text.Json;

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

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}