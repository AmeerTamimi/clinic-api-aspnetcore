using System.Text.Json.Serialization;

namespace ClinicAPI.Responses
{
    public class UserResponse
    {
        [JsonPropertyOrder(0)] public int UserId { get; set; }
        [JsonPropertyOrder(1)] public string? FirstName { get; set; }
        [JsonPropertyOrder(2)] public string? LastName { get; set; }
        [JsonPropertyOrder(3)] public int Age { get; set; }
        [JsonPropertyOrder(4)] public string? Phone { get; set; }
        [JsonPropertyOrder(5)] public string? Email { get; set; }
    }
}
