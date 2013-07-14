using System.Web;

namespace MSBlogEngine.Web
{
    public class RssLegacyHandler: IHttpHandler
    {
        private readonly HttpContext _context;

        public RssLegacyHandler(HttpContext context)
        {
            _context = context;
        }

        public void ProcessRequest(HttpContext context)
        {
            IsReusable = true;
            context.Response.Status = "301 Moved Permanently";
            context.Response.AppendHeader("Location", "/RSS");
        }

        public bool IsReusable { get; private set; }
    }

    public class RssLegacyHandlerFactory : IHttpHandlerFactory
    {
        public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            return new RssLegacyHandler(context);
        }

        public void ReleaseHandler(IHttpHandler handler)
        {

        }
    }
}