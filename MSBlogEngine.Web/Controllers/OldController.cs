using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MSBlogEngine.Web.Controllers
{
    public class OldController : Controller
    {
        private Dictionary<string, string> _nameMap = new Dictionary<string, string>
        {
            {"Exchange-2010-The-WinRM-client-received-an-HTTP-server-error-status-(500).aspx", "NDC 2012 – Moje oceny"}
        };

        public ActionResult Translate(string name)
        {
            return RedirectToAction("Post", "Post", new { id = _nameMap[name] });
        }

    }
}
