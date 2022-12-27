namespace Site.Middleware
{
    public class Authentication
    {
        private readonly RequestDelegate _next;

        public Authentication(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            BeginInvoke(context);
            await _next.Invoke(context);
        }

        private async void BeginInvoke(HttpContext context)
        {
            if (context.Request.Method != "OPTIONS" && !context.User.Identity.IsAuthenticated)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Not Authenticated");
            }
        }
    }
}
