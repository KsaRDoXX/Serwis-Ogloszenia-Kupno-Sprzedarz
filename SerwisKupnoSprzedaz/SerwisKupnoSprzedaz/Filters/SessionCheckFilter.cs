using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System.Linq;
using SerwisKupnoSprzedaz.Data;

public class SessionCheckFilter : ActionFilterAttribute
{
    private readonly AppDBContext _dbContext;

    public SessionCheckFilter(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var userId = context.HttpContext.Session.GetString("UserId");

        if (!string.IsNullOrEmpty(userId))
        {
            // Pobierz użytkownika z bazy danych
            var user = _dbContext.Users.FirstOrDefault(u => u.Id.ToString() == userId);

            if (user != null)
            {
                // Przechowaj informacje o użytkowniku w HttpContext.Items
                context.HttpContext.Items["IsLoggedIn"] = true;
                context.HttpContext.Items["UserName"] = user.UserName;
                context.HttpContext.Items["UserId"] = user.Id;
                context.HttpContext.Items["IsAdmin"] = user.IsAdmin; // Zakładamy, że IsAdmin to właściwość użytkownika
            }
        }
        else
        {
            // Użytkownik nie jest zalogowany
            context.HttpContext.Items["IsLoggedIn"] = false;
        }

        base.OnActionExecuting(context);
    }
}
