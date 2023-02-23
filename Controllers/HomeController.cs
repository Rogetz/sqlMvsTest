using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using sqlTestMvc.Models;
using MySql.Data.MySqlClient;

namespace sqlTestMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        testDataClass tdClass = new testDataClass();

        using(MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=myschool;port=3306;password=Lifenimaze@1")){
            // must open the connection for it to work.
            con.Open();
            MySqlCommand command = new MySqlCommand("select * from course",con);
            MySqlDataReader reader = command.ExecuteReader();


            // Note that the reader contains full columns so you have to loop through each single peace of data, using the reader.Read() method(seems like it reads a line each time.)
            while(reader.Read()){
                tdClass.testDataString = reader["CourseName"].ToString() ?? "NO DATA";
            }
            reader.Close();
        }
        return View("Index",tdClass.testDataString);
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
