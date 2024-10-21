using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BLL.Controllers.Bases
{
    public abstract class MvcController : Controller
    {
        protected MvcController()
        {
            var cultureInfo = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}
