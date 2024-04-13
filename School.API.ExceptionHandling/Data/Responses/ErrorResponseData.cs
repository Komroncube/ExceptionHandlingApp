using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace School.API.ExceptionHandling.Data.Responses;

public class ErrorResponseData
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string Path { get; set; }
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
        //return JsonSerializer.Serialize(this);
    }
}
