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
}
