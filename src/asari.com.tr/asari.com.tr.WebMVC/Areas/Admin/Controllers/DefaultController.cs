using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace asari.com.tr.WebMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class DefaultController : AdminControllerBase
{
    public IActionResult Index()
    {
        ViewBag.Token = HttpContext.Session.GetString("Token");
        var token = HttpContext.Session.GetString("Token");
        if (token == null)
        {
            return Redirect("Admin/AdminLogin/Index");
        }

        var apiToken = HttpContext.Session.GetString("Token");


        ViewBag.Message = BuildMessage(token, 50);
        return View();
    }

    private string BuildMessage(string stringToSplit, int chunkSize)
    {
        var data = Enumerable.Range(0, stringToSplit.Length / chunkSize).Select(i => stringToSplit.Substring(i * chunkSize, chunkSize));
        string result = "The generated token is:";
        foreach (string str in data)
        {
            result += Environment.NewLine + str;
        }
        return result;
    }

    [AllowAnonymous]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Clear();
        return Redirect("/");
    }
}
