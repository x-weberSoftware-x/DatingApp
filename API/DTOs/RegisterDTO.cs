using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDTO
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [StringLength(8), MinLength(4)]
    public string Password { get; set; } = string.Empty;
}
