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
    public IActionResult EliminarProducto(int IdProducto)
    {
        Farmacia farmacia = Farmacia.FromString(HttpContext.Session.GetString("user"));
        BD.EliminarProducto(IdProducto);
        return RedirectToAction("Stock", "Home");
    }
    public IActionResult EditarProducto()
    {
        Farmacia usu = Farmacia.FromString(HttpContext.Session.GetString("user"));
        if (usu== null)
        {  
            return RedirectToAction("Index","Home");
        }
        ViewBag.Farmacia = usu;
        return View();
    }
    public IActionResult Editar(Producto usu)
    {
        Farmacia farmacia = Farmacia.FromString(HttpContext.Session.GetString("user"));
        BD.EditarProducto(usu, farmacia.IdFarmacia);
        return RedirectToAction("IndexConSessionFarmacia");
    }   
    public IActionResult AgregarProducto()
    {
        Farmacia usu = Farmacia.FromString(HttpContext.Session.GetString("user"));
        if (usu== null)
        {  
            return RedirectToAction("Index","Home");
        }
        ViewBag.Farmacia = usu;
        return View();
    }
    public IActionResult GuardarProducto(Producto usu)
    {
        Farmacia farmacia = Farmacia.FromString(HttpContext.Session.GetString("user"));
        BD.AgregarProducto(usu, farmacia.IdFarmacia);
        return RedirectToAction("Stock");
    }
    [HttpGet]
    public List<Producto> ResultadosBusqueda(string query)
    {
       return BD.Buscar(query);
        
    }
    public IActionResult PedidosPaciente()
    {
        Paciente usu = Paciente.FromString(HttpContext.Session.GetString("user"));
        if (usu== null)
        {  
            return RedirectToAction("Index","Home");
        }
        List<Pedido> Pedidos = BD.BuscarPedidos(usu.IdPaciente);
        ViewBag.Pedidos = Pedidos;
        ViewBag.Paciente = usu;
        return View();
    }
    public IActionResult PedidosFarmacia()
    {
        Farmacia usu = Farmacia.FromString(HttpContext.Session.GetString("user"));
        if (usu== null)
        {  
            return RedirectToAction("Index","Home");
        }
        List<Pedido> Pedidos = BD.BuscarPedidosFarmacia(usu.IdFarmacia);
        ViewBag.Pedidos = Pedidos;
        ViewBag.Farmacia = usu;
        return View();
    }
    public IActionResult NotificacionesFarmacia(){
        Farmacia usu = Farmacia.FromString(HttpContext.Session.GetString("user"));
        if (usu== null)
        {  
            return RedirectToAction("Index","Home");
        }
        ViewBag.Farmacia = usu;
        return View();
    }
    public IActionResult ComprarProducto(){
        Paciente usu = Paciente.FromString(HttpContext.Session.GetString("user"));
        if (usu== null)
        {  
            return RedirectToAction("Index","Home");
        }
        return View();
    }
    public IActionResult Comprar(Producto prod, string Direccion){
        Paciente usu = Paciente.FromString(HttpContext.Session.GetString("user"));
        if (usu== null)
        {  
            return RedirectToAction("Index","Home");
        }
        BD.AgregarPedido(prod, usu.IdPaciente, Direccion);
        return View();
    }
}
