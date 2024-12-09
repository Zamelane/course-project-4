using System.Text.Json.Serialization;

namespace RequestsLibrary.RequestsData
{
    public class LoginRequestData
    {
        public LoginRequestData(string login, string password)
        {
            Login = login;
            Password = password;
        }

        [JsonPropertyName("login")   ] public string Login { get; set; }
        [JsonPropertyName("password")] public string Password { get; set; }
    }
}
