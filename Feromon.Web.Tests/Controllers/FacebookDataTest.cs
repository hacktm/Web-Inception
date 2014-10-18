using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.EntityModel;
using Feromon.FacebookHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Feromon.Web.Tests.Controllers
{
    [TestClass]
    public class FacebookDataTest
    {
        [TestMethod]
        public IEnumerable<FriendModel> GetFriends(string category, string accessToken)
        {
            var result = FriendsDataHelper.GetFriends(category, accessToken);
            return null;
        }
    }
}
