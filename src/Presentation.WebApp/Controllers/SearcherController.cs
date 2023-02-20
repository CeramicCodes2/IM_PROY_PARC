using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Domain;
using Infrastructure;

namespace Presentation.WebApp.Controllers;
public class SearcherController : Controller
{
    public IActionResult Index()
    {
        //var data = _clientesDbContext.List();
        return View();
    }
}