using Microsoft.AspNetCore.Mvc;

namespace CofrinhoSenha.API.Controller;

public class AuthController : Microsoft.AspNetCore.Mvc.Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}