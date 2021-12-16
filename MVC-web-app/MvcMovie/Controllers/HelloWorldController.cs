using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/
        // GET: /HelloWorld/Index

        //public string Index()
        //{
        //    return "This is my default action...";
        //}

        // 
        // GET: /HelloWorld/Welcome/ 

        //public string Welcome(string name, /*int numTimes = 1*/int ID = 1)
        //{
        //    //return "This is the Welcome action method...";
        //    //return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
        //    return HtmlEncoder.Default.Encode($"Hello {name}, ID: {ID}");
        //}

        public IActionResult Welcome(string name, int numTimes = 1)
        {
            //the ViewData dictionary was used to pass data from the controller to a view
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
