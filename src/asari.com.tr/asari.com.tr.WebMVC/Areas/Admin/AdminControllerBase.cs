using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace asari.com.tr.WebMVC.Areas.Admin;

public class AdminControllerBase : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var IsLogin = false;
        if (context.HttpContext.Session.GetString("Token") == null)
        {
            //admin girişi yapılmamış
            context.HttpContext.Response.Redirect("Admin/AdminLogin/Index");
            //admin girişe sayfayı yönlendir.
            //redirect("....")bunu kullanamadık redirectte ezme işlemi yaptık override sayfa yönlendirir.
        }
        else
        {
            //sorun yok admin ieçerde
            //sayfayı çalıştır
            base.OnActionExecuting(context);

        }
    }
}
