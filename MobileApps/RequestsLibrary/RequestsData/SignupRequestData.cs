using System.Text.Json.Serialization;

namespace RequestsLibrary.RequestsData
{
    public class SignupRequestData
    {
        public SignupRequestData(string firstName, string lastName, string login, string password, DateTime birthDay, string email)
        {
            FirstName = firstName;
            LastName  = lastName;
            Login     = login;
            Password  = password;
            BirthDay  = birthDay;
            Email     = email;
        }
        [JsonPropertyName("firstName")] public string   FirstName { get; set; }
        [JsonPropertyName("lastName" )] public string   LastName  { get; set; }
        [JsonPropertyName("login"    )] public string   Login     { get; set; }
        [JsonPropertyName("password" )] public string   Password  { get; set; }
        [JsonPropertyName("birthDay" )] public DateTime BirthDay  { get; set; }
        [JsonPropertyName("email"    )] public string   Email     { get; set; }
    }
}
