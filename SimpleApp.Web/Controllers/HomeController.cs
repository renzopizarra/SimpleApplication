using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SimpleApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(Session.SessionKeyDisplayName)) && string.IsNullOrEmpty(HttpContext.Session.GetString(Session.SessionKeyUserName)))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
            
        }

        [HttpGet]
        [Produces("application/json")]
        public JsonResult GetTerritories()
        {
            try
            {
                HttpClient _httpClient = new HttpClient();
                var url = "https://netzwelt-devtest.azurewebsites.net/Territories/All";
                var uri = new Uri(url);
                var stringContent = new StringContent(JsonConvert.SerializeObject(""), Encoding.UTF8, "application/json");
                var response = _httpClient.GetAsync(uri);
                var res = response.Result;
                string result = res.Content.ReadAsStringAsync().Result;
                return Json(result);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
