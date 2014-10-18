using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataLayer.EntityModel;
using Facebook;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Feromon.FacebookHelper
{
    public class FriendsDataHelper
    {
        public static IEnumerable<FriendModel> GetFriends(string category, string accessToken)
        {

            //if (accessToken == null) return null;
            var client = new FacebookClient(accessToken);
            var friends = client.Get(String.Format("{0}/invitable_friends", "me")).ToString();
   
            return null;
        }
    }
}
