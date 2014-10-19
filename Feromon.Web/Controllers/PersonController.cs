using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Feromon.Web.Controllers
{
    public class PersonController : Controller
    {
        [HttpPost]
        public ActionResult ShootSelectedPersons(List<string> selectedPersonList)
        {
            foreach (var person in selectedPersonList)
            {
                //save in database
            }
            return View();
        }
    }
}