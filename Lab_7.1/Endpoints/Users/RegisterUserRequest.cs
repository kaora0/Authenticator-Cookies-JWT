using System.ComponentModel.DataAnnotations;

namespace Lab_7._1.Endpoints.Users;

public record RegisterUserRequest(
    [Required] string UserName,
    [Required] string Password,
    [Required] string Email);