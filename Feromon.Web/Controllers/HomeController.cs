using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using DataLayer.EntityModel;
using Feromon.Web.SignalR;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using Feromon.FacebookHelper;
using PayPal;
using PayPal.OpenIdConnect;

namespace Feromon.Web.Controllers
{
    [System.Web.Mvc.Authorize]
    public partial class HomeController : Controller
    {
        public static FacebookIdentityHelper _facebookIdentityHelper = null;

        public HomeController()
        {
            _facebookIdentityHelper = new FacebookIdentityHelper();
        }
        public virtual ActionResult Index()
        {
            var token = _facebookIdentityHelper.GetAccessToken((ClaimsIdentity)User.Identity);
            var result = FriendsDataHelper.GetFriends("all", token);


            //Stream compressedfile = Inception.WebClientMVC.Helpers.Utilities.UploadImageCompressed(file, 1200);
            //var photoURL = azureService.UploadBlobToContainer("photos", Guid.NewGuid() + Path.GetExtension(file.FileName), compressedfile);


            return View(result);
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

        [HttpPost]
        public ActionResult SendNotification()
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.BroadcastNotification("UserID", "Message");
            return null;
        }
    }
}