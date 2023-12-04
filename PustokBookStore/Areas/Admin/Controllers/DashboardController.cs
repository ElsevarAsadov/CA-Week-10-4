using PustokBookStore.Data;
using Microsoft.AspNetCore.Mvc;

namespace IDONOTKNOW.Areas.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    private readonly AppDbContext _dbContext;
    public DashboardController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Author()
    {
        return View();
    }
}