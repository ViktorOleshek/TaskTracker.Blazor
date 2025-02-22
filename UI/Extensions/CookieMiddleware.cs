namespace UI.Extensions;

public class CookieMiddleware
{
    private readonly RequestDelegate _next;
    private string _key = "AuthToken";

    public CookieMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path == "/set-token")
        {
            var token = context.Request.Query [_key];

            if (!string.IsNullOrEmpty(token))
            {
                context.Response.Cookies.Append(_key, token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    MaxAge = TimeSpan.FromMinutes(15)
                });
            }

            context.Response.Redirect("/");
            return;
        }

        if (context.Request.Path == "/logout")
        {
            context.Response.Cookies.Delete(_key);
            context.Response.Redirect("/");
            return;
        }

        await _next(context);
    }
}

public static class CookieMiddlewareExtensions
{
    public static IApplicationBuilder UseCookieMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CookieMiddleware>();
    }
}