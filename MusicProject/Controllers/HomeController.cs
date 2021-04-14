using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MusicProject.Controllers
{
    public class HomeController : Controller
    {
       // private readonly ILogger<HomeController> _logger;
        private readonly ContactContext _auc;
        public HomeController(ContactContext auc)
        {
            _auc = auc;
        }

        /*public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(Contact uc)
        {
            _auc.Add(uc);
            _auc.SaveChanges();
            ViewBag.message = "The user " + uc.CName + " is saved successfully";
            return View();
        }

        public IActionResult Thanks(Contact c)
        {
            ViewBag.Name = c.CName;
            return View();
        }

        public IActionResult AllSongs()
        {
            return View();
        }

        public IActionResult Independent()
        {
            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }

        public IActionResult Artist()
        {
            return View();
        }

        public IActionResult ArtistDescription(string artistimage,string artistname,string artistinfo,string artistinstagram)
        {
            ViewBag.artistimage = artistimage;
            ViewBag.artistname = artistname;
            ViewBag.artistinfo = artistinfo;
            ViewBag.artistinstagram = artistinstagram;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
