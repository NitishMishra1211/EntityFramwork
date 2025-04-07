using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Models;
using DataAccess.Migrations;
using Microsoft.AspNetCore.Http;
using DataAccess.Data;

namespace EntityFramwork.Areas.Admin.Controllers;

[Area("Admin")]
public class StudentController : Controller
{
    //private readonly ILogger<HomeController> _logger;

    // public HomeController(ILogger<HomeController> logger)
    //{
    //  _logger = logger;
    //}

    private readonly ApplicationDbContext studentDB;

    public StudentController(ApplicationDbContext studentDB)
    {
        this.studentDB = studentDB;
    }

   
    public IActionResult Index()
    {

        var stdData = studentDB.Students.ToList();
        return View(stdData);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Student std)
    {
        if(ModelState.IsValid)
        {
            studentDB.Students.Add(std);
            studentDB.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
        return View(std);
    }

    public IActionResult Edit(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }
        var std = studentDB.Students.Find(id);
        if (std == null)
        {
            return NotFound();
        }
        return View(std);

    }

    [HttpPost]
    public IActionResult Edit(int? id,Student std)
    {
        if (id != std.rollno)
        {
            return NotFound();
        }

        if(ModelState.IsValid)
        {
            studentDB.Students.Update(std);
            studentDB.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        return View(std);
    }
    
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var std = studentDB.Students.Find(id);
        if (std == null)
        {
            return NotFound();
        }
        return View(std);
    }
    [HttpPost,ActionName("Delete")]
    public IActionResult DeleteComfirmed(int id) 
    {
        var std = studentDB.Students.Find(id);
        studentDB.Students.Remove(std);
        studentDB.SaveChanges();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Login(Student std)
    {
        var user =  studentDB.Students.SingleOrDefault(u => u.UserName == std.UserName && u.Password == std.Password);
        if (user != null)
        {
            HttpContext.Session.SetString("Username", user.UserName);
            return RedirectToAction("Dashboard");
        }

        return View(std);
    }


    public IActionResult Register()
    {
        return View();
    }
    public IActionResult Dashboard()
    {
        if(HttpContext.Session.GetString("Username")!=null)
        {
            ViewBag.MySession = HttpContext.Session.GetString("Username");
        }
        else
        {
            return RedirectToAction("login");
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
