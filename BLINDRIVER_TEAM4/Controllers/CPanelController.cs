using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BLINDRIVER_TEAM4.Controllers
{
    public class CPanelController : Controller
    {
        // GET: CPanel
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Pages()
        {
            return View();
        }
    }
}