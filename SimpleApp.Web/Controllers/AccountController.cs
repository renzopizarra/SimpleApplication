using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimpleApp.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApp.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult Validate(string username, string password)
        {
            try
            {
                var url = "https://netzwelt-devtest.azurewebsites.net/Account/SignIn";
                var baseAddress = new Uri(url);
                var parameters = new Dictionary<string, string> { { "username", username }, { "password", password } };

                using (var handler = new HttpClientHandler { UseCookies = false })
                using (HttpClient client = new HttpClient(handler) { BaseAddress = baseAddress })
                {
                    var message = new HttpRequestMessage(HttpMethod.Get, "/test");

                    var stringContent = new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json");
                    var response = client.PostAsync(url, stringContent);
                    var res = response.Result;
                    if (res.IsSuccessStatusCode && res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string result = res.Content.ReadAsStringAsync().Result;
                        var responseBody = JsonConvert.DeserializeObject<ResponseBody>(result);
                        if (string.IsNullOrEmpty(HttpContext.Session.GetString(Session.SessionKeyDisplayName)) && string.IsNullOrEmpty(HttpContext.Session.GetString(Session.SessionKeyUserName)))
                        {
                            HttpContext.Session.SetString(Session.SessionKeyDisplayName, responseBody.displayName);
                            HttpContext.Session.SetString(Session.SessionKeyUserName, responseBody.userName);
                        }

                        message.Headers.Add("Cookie", "UserName=" + responseBody.userName + "; DisplayName=" + responseBody.displayName + "");
                        return Json(new { status = "success", data = res });
                    }
                    else
                    {
                        return Json(new { status = "error", data = res });
                    }
                }
            
            }
            catch
            {
                return View();
            }
        }

        
    }
}
