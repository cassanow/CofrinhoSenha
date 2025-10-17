using System.Security.Cryptography;
using CofrinhoSenha.Interface;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace CofrinhoSenha.Service;

public class PasswordService : IPasswordService
{
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
}