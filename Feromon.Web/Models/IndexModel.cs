using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Feromon.Web.Models
{
    public class IndexModel
    {
        public IEnumerable<FriendModel> Friends { get; set; } 
    }
}