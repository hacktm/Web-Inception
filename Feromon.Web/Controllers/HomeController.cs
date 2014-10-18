using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Feromon.FacebookHelper;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Feromon.Web.Controllers
{
    public partial class HomeController : Controller
    {
        public static FacebookIdentityHelper _facebookIdentityHelper= null;

        public HomeController()
        {
                _facebookIdentityHelper = new FacebookIdentityHelper();
        }
        public virtual ActionResult Index()
        {
            var token = _facebookIdentityHelper.GetAccessToken((ClaimsIdentity)User.Identity);
            if (String.IsNullOrWhiteSpace(token) == true)
            {
                return RedirectToAction(MVC.Account.ActionNames.Login, MVC.Account.Name);
            }
            var result = FriendsDataHelper.GetFriends("all", token);
            return View();
        }

        public virtual ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public virtual ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}