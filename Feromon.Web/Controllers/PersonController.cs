using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Feromon.Web.SignalR;
using Microsoft.AspNet.SignalR;

namespace Feromon.Web.Controllers
{
    public class PersonController : Controller
    {
        [HttpPost]
        public ActionResult ShootSelectedPersons(List<string> selectedPersonList)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.BroadcastNotification("UserID", "Message");
            
            //foreach (var person in selectedPersonList)
            //{
            //    //save in database
            //}
            return View();
        }
    }
}