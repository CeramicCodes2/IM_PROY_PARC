using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Domain;
using Infrastructure;

namespace Presentation.WebApp.Controllers;

public class VentasController : Controller
{
    private readonly ClientesDbContext _clientesDbContext;
    private readonly ProductosDbContext _productosDbContext;
    private readonly VentasDbContext _ventasDbContext;
    public VentasController(IConfiguration configuration)
    {         
        _ventasDbContext = new VentasDbContext(configuration.GetConnectionString("DefaultConnection"));
        _clientesDbContext = new ClientesDbContext(configuration.GetConnectionString("DefaultConnection")); 
        _productosDbContext =  new ProductosDbContext(configuration.GetConnectionString("DefaultConnection"));
    }
    public IActionResult Index()
    {
        var data = _ventasDbContext.List();
        ViewBag.LabelsGraph = data
        .GroupBy(x => x.Producto.Descripcion)
        .Select(x => $"'{x.Key}'")
        .ToList();
        var dprex = _ventasDbContext.getDataGraph();
        ViewBag.DataPrex = dprex
        .GroupBy(x => x.operacion)
        .Select(x=>$"{(int)(x.Key)}")
        .ToList();
        return View(data);
    }

    public IActionResult Details(Guid id)
    {
        var data = _ventasDbContext.Details(id);
        return View(data);
    }
    public IActionResult Create()
    {
        //
        ViewBag.Cliente = _clientesDbContext.List();

        // viewBag son vareables de sesion se crea una cookie con todo el codigo
        // se crea una vista las viewbag son vareables de sesion que semandan al cliente
        // hay varios tipos viewbag viewdata y temp data
        // esta mas enfocado a js no tando a c#
        
        ViewBag.Producto = _productosDbContext.List();
        return View();
    }
    [HttpPost]
    public IActionResult Create(Venta data)
    {
        Console.WriteLine(data.ClienteId.ToString());
        _ventasDbContext.Create(data);
        //SmtpClientEmailService.SendEmail(data.Cliente.Correo, "Asunto", $"<h4>Hola {data.Cliente.Nombre}</h1>", true);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(Guid id)
    {
        ViewBag.Cliente = _clientesDbContext.List();

        // viewBag son vareables de sesion se crea una cookie con todo el codigo
        // se crea una vista las viewbag son vareables de sesion que semandan al cliente
        // hay varios tipos viewbag viewdata y temp data
        // esta mas enfocado a js no tando a c#
        
        ViewBag.Producto = _productosDbContext.List();
        var data = _ventasDbContext.Details(id);
        return View(data);
    }
    [HttpPost]
    public IActionResult Edit(Venta data)
    {
        _ventasDbContext.Edit(data);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(Guid id)
    {
        _ventasDbContext.Delete(id);
        return RedirectToAction("Index");
    }
}