using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers;

[Route("Culture")] // Définit la route de base
public class CultureController : Controller
{
    [HttpGet("Set")] // Déclare explicitement que Set() répond à /Culture/Set
    public IActionResult Set(string culture, string redirectUri = "/")
    {
        if (!string.IsNullOrEmpty(culture))
        {
            var requestCulture = new RequestCulture(culture, culture);
            var cookieName = CookieRequestCultureProvider.DefaultCookieName;
            var cookieValue = CookieRequestCultureProvider.MakeCookieValue(requestCulture);

            HttpContext.Response.Cookies.Append(cookieName, cookieValue);
        }

        return LocalRedirect(redirectUri);
    }
}
