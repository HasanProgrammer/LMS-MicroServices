using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Status = DataModel.Enums.User.Status;

namespace DataModel
{
    /*Property | Fields*/
    public class User
    {
        public string Id          { get; set; } /*Guid*/
        public string ImageFile   { get; set; } /*نشانی تصویر کاربر*/
        public string Username    { get; set; }
        public string Email       { get; set; }
        public string Phone       { get; set; }
        public string Expert      { get; set; }
        public string Description { get; set; }
    }
}