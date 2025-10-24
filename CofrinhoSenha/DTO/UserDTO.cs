﻿using System.ComponentModel.DataAnnotations;

namespace CofrinhoSenha.DTO;

public class UserDTO
{
    public string Username { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    public string Password { get; set; }
}