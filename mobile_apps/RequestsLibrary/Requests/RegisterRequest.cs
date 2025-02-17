﻿using System.Text.Json.Serialization;

namespace RequestsLibrary.Requests;
public class RegisterRequest(string login, string password, string firstName, string lastName, string email)
{
    [JsonPropertyName("login")] public string Login { get; set; } = login;
    [JsonPropertyName("password")] public string Password { get; set; } = password;
    [JsonPropertyName("firstName")] public string FirstName { get; set; } = firstName;
    [JsonPropertyName("lastName")] public string LastName { get; set; } = lastName;
    [JsonPropertyName("email")] public string Email { get; set; } = email;
}