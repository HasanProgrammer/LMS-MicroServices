using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Status = DataModel.Enums.User.Status;

namespace DataModel
{
    /*Property | Fields*/
    public partial class User
    {
        public string Id          { get; set; } /*Guid*/
        public string ImageFile   { get; set; } /*نشانی تصویر کاربر*/
        public string Username    { get; set; }
        public string Email       { get; set; }
        public string Phone       { get; set; }
        public string EmailCode   { get; set; } /*برای اعتبارسنجی*/
        public int PhoneCode      { get; set; } /*برای اعتبارسنجی*/
        public string Expert      { get; set; }
        public string Description { get; set; }
        public bool IsVerifyEmail { get; set; }
        public bool IsVerifyPhone { get; set; }
        public Status Status      { get; set; }
        public string CreatedAt   { get; set; }
        public string UpdatedAt   { get; set; }
    }

    /*Relation*/
    public partial class User
    {
        
    }
}