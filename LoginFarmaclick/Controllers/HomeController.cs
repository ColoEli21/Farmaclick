using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LoginFarmaclick.Models;
using System.Text;

namespace LoginFarmaclick.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult IndexConSessionPaciente()
    {
        Paciente usu = Paciente.FromString(HttpContext.Session.GetString("user"));
        if (usu== null)
        {  
            return RedirectToAction("Index","Home");
        }
        ViewBag.Paciente = usu;
        return View();
    }
    public IActionResult IndexConSessionDoctor()
    {
        Doctor usu = Doctor.FromString(HttpContext.Session.GetString("user"));
        if (usu== null)
        {  
            return RedirectToAction("Index","Home");
        }
        ViewBag.Doctor = usu;
        return View();
    }
    public IActionResult IndexConSessionFarmacia()
    {
        Farmacia usu = Farmacia.FromString(HttpContext.Session.GetString("user"));
        if (usu== null)
        {  
            return RedirectToAction("Index","Home");
        }
        ViewBag.Farmacia = usu;
        return View();
    }
    public IActionResult DatosCuentaPaciente()
    {
        Paciente usu = Paciente.FromString(HttpContext.Session.GetString("user"));
        if (usu== null)
        {  
            return RedirectToAction("Index","Home");
        }
        ViewBag.Paciente = usu;
        return View();
    }
    public IActionResult DatosCuentaDoctor()
    {
        Doctor usu = Doctor.FromString(HttpContext.Session.GetString("user"));
        if (usu== null)
        {  
            return RedirectToAction("Index","Home");
        }
        ViewBag.Doctor = usu;
        return View();
    }
    public IActionResult DatosCuentaFarmacia()
    {
        Farmacia usu = Farmacia.FromString(HttpContext.Session.GetString("user"));
        if (usu== null)
        {  
            return RedirectToAction("Index","Home");
        }
        ViewBag.Farmacia = usu;
        return View();
    }
    public IActionResult Stock()
    {
        Farmacia usu = Farmacia.FromString(HttpContext.Session.GetString("user"));
   
        if (usu== null)
        {  
            return RedirectToAction("Index","Home");
        }
        List<Producto> Productos = BD.BuscarProductos(usu.IdFarmacia);
        ViewBag.Productos = Productos;
        ViewBag.Farmacia = usu;
        return View();
    }
    public IActionResult EliminarProducto(string IdProducto)
    {
        BD.EliminarProducto(IdProducto);
        return View("IndexConSessionFarmacia");
    }
    public IActionResult EditarProducto(string IdProducto)
    {
        return View("");
    }
    public IActionResult Editar(string idProducto, string nuevoNombre, string nuevoPrecio, string nuevoStock, string nuevoDescripcion)
    {
        return View("IndexConSessionFarmacia");
    }   
    public IActionResult AgregarProducto(string IdFarmacia)
    {
        return View("");
    }
    public IActionResult GuardarProducto(Producto usu, string IdFarmacia)
    {
        BD.AgregarProducto(usu, IdFarmacia);
        return RedirectToAction("Stock");
    }
}
