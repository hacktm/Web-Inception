using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Feromon.Web.SignalR
{
    public class NotificationHub: Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
        public void Send(string receiver, string message)
        {
            string fromUserId = Context.ConnectionId;
            //Clients.Client(receiver).broadcastNotification(receiver, message);
            Clients.All.broadcastNotification(receiver, message);
            //Clients.AllExcept(Clients.Caller).broadcastNotification(receiver, message);
        }
    }
}