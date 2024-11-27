using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LoginFarmaclick.Models;

namespace LoginFarmaclick.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    public IActionResult DatosLoginPaciente(string email, string contraseña)
    {
        Paciente usu = BD.IniciarSesionPaciente(email, contraseña);
        if (usu == null)
        {
            ViewBag.Error =  "Login Incorrecto";
            return View("LoginPaciente");
        }
        else
        {
            HttpContext.Session.SetString("user", usu.ToString());
            return RedirectToAction("IndexConSessionPaciente", "Home");
        }
    }
    public IActionResult DatosLoginDoctor(string email, string contraseña)
    {
        Doctor usu = BD.IniciarSesionDoctor(email, contraseña);
        if (usu == null)
        {
            ViewBag.Error =  "Login Incorrecto";
            return View("LoginDoctor");
        }
        else
        {
            HttpContext.Session.SetString("user", usu.ToString());
            return RedirectToAction("IndexConSessionDoctor", "Home");

        }
    }
    public IActionResult DatosLoginFarmacia(string email, string contraseña)
    {
        Farmacia usu = BD.IniciarSesionFarmacia(email, contraseña);
        if (usu == null)
        {
            ViewBag.Error =  "Login Incorrecto";
            return View("LoginFarmacia");
        }
        else
        {
            HttpContext.Session.SetString("user", usu.ToString());
            return RedirectToAction("IndexConSessionFarmacia", "Home");

        }    
    }
    public IActionResult LoginPaciente()
    {
        return View();
    }
    public IActionResult LoginDoctor()
    {
        return View();
    }
    public IActionResult LoginFarmacia()
    {
        return View();
    }
    public IActionResult RegistrarsePaciente()
    {
        return View();
    }
    public IActionResult RegistrarseDoctor()
    {
        return View();
    }
    public IActionResult RegistrarseFarmacia()
    {
        return View();
    }
    public IActionResult GuardarPaciente(Paciente usu)
    {
    
        BD.RegistrarPaciente(usu);
        HttpContext.Session.SetString("user", usu.ToString());
        return RedirectToAction("IndexConSessionPaciente", "Home", usu);
    }
    public IActionResult GuardarDoctor(Doctor usu)
    {
        BD.RegistrarDoctor(usu);
        HttpContext.Session.SetString("user", usu.ToString());
        return RedirectToAction("IndexConSessionDoctor", "Home", usu);
    }
    public IActionResult GuardarFarmacia(Farmacia usu)
    {
        BD.RegistrarFarmacia(usu);
        HttpContext.Session.SetString("user", usu.ToString());
        return RedirectToAction("IndexConSessionFarmacia", "Home", usu);
    }
    
    public IActionResult OlvideContraseña(string email, string DNI)
    {
        return View("Index", "Home");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("user");
        return RedirectToAction("Index","Home");
    }
}
