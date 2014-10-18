using System.Collections.Generic;
using System.Web.Mvc;
using Feromon.FacebookHelper;
using PayPal;
using PayPal.OpenIdConnect;

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
            //var token = _facebookIdentityHelper.GetAccessToken((ClaimsIdentity)User.Identity);
            //if (String.IsNullOrWhiteSpace(token) == true)
            //{
            //    return RedirectToAction(MVC.Account.ActionNames.Login, MVC.Account.Name);
            //}
            //var result = FriendsDataHelper.GetFriends("all", token);


            //Stream compressedfile = Inception.WebClientMVC.Helpers.Utilities.UploadImageCompressed(file, 1200);
            //var photoURL = azureService.UploadBlobToContainer("photos", Guid.NewGuid() + Path.GetExtension(file.FileName), compressedfile);
            
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