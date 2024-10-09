namespace KoiDeliveryOrdering.MVCWebApp.Middleware
{
    public class SessionCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // If the session key "Username" does not exist, redirect to /Authentication
            if (context.Session.GetString("Username") == null &&
                !context.Request.Path.StartsWithSegments("/Authentication"))
            {
                context.Response.Redirect("/Authentication");
                return;
            }

            await _next(context);
        }
    }
}
