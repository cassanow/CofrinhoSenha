using System.Security.Cryptography;
using CofrinhoSenha.Data.Context;
using CofrinhoSenha.Entity;
using CofrinhoSenha.Interface;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace CofrinhoSenha.Service;

public class PasswordService : IPasswordService
{
    private readonly Random _random =  new Random();
    
    public string HashPassword(string password)  
    {  
        var salt = RandomNumberGenerator.GetBytes(128/8);  
        var hashed =  Convert.ToBase64String(KeyDerivation.Pbkdf2(  
            password: password!,  
            salt:  salt,  
            prf: KeyDerivationPrf.HMACSHA1,  
            iterationCount: 10000,    
            numBytesRequested: 256 / 8  
        ));  
  
        return $"{Convert.ToBase64String(salt)}:{hashed}";  
    }  
  
    public bool VerifyPassword(string hashedPassword, string password)  
    {  
        var parts = hashedPassword.Split(':');  
        if (parts.Length != 2)   
            return false;  
        var salt = Convert.FromBase64String(parts[0]);  
        var savedHash = parts[1];  
  
        var computedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(  
            password: password!,  
            salt:  salt,  
            prf: KeyDerivationPrf.HMACSHA1,  
            iterationCount: 10000,  
            numBytesRequested: 256 / 8));  
  
        return savedHash == computedHash;  
    }

    public string GenerateStrongPassword(GeneratePasswordRequest request)
    {
        var chars = "";

        if(request.IncludeUppercase)
            chars += "ABCDEFGHIJKLMNOPQRSTUVWXYL";
        
        if (request.IncludeLowercase)
            chars += "abcdefghijklmnopqrstuvwxyl";
        
        if(request.IncludeNumbers)
            chars += "0123456789";
        
        if(request.IncludeSpecialChars)
            chars += "!@#$%^&?";
        
        
        if(string.IsNullOrEmpty(chars))
            throw new Exception("Password generation failed");
        
        var password = new char[request.Length];
        for (var i = 0; i < request.Length; i++)
        {
            password[i] = chars[_random.Next(chars.Length)];
        }
        
        return new string(password);
    }
}