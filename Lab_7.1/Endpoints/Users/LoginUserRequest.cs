using System.ComponentModel.DataAnnotations;

namespace Lab_7._1.Endpoints.Users;

public record LoginUserRequest(
    [Required] string Email,
    [Required] string Password);