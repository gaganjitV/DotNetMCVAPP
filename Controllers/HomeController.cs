using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RdTestApp.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace RdTestApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IConfiguration _configuration; // Added to access configuration settings


    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _configuration = configuration; // Initialize the configuration
        _logger = logger;
    }

    public IActionResult Index()
    {
        DataTable vDt = null;
        using (SqlConnection vConn = new SqlConnection(_configuration.GetConnectionString("rkvconn")))
        {

            vConn.Open();
            SqlDataAdapter vAdapter = new SqlDataAdapter("SELECT * FROM dept", vConn);
            DataSet vDs = new DataSet();
            vAdapter.Fill(vDs);
            vDt = vDs.Tables[0];
            vConn.Close();
            _logger.LogInformation("Data fetched successfully from the database.");
            ViewData["DeptData"] = vDt; // Pass the DataTable to the view

        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
