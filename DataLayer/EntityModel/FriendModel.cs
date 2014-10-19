using System.Collections.Generic;
using System.ComponentModel;

namespace DataLayer.EntityModel
{
    public class FriendModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string category { get; set; }

        public enum list_type
        {
            close_friends,
            acquaintances,
            restricted,
            user_created,
            education,
            work,
            current_city,
            family
        }
    }

    public class Friends
    {
        public List<FacebookFriend> data { get; set; }
    }

    public class FacebookFriend
    {

        public string id { get; set; }
        public string name { get; set; }
        public Picture picture { get; set; }
    
    }

    public class Picture
    {
        public PictureData data { get; set; }
    }

    public class PictureData
    {
        public string url { get; set; }
    }
}
