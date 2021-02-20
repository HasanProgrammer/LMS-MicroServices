using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Status = DataModel.Enums.Video.Status;

namespace DataModel
{
    /*Property | Fields*/
    public partial class Video
    {
        public string Id        { get; set; } /*Guid*/
        public string Title     { get; set; }
        public string Duration  { get; set; } /*مدت زمان فیلم برنامه نویسی*/
        public string VideoFile { get; set; } /*مسیر ذخیره شدن فایل فیلم برنامه نویسی*/
        public bool IsFree      { get; set; }
        public Status Status    { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
    
    /*Relation*/
    public partial class Video
    {
        public User User       { get; set; } /*ناشر فیلم برنامه نویسی*/
        public Term Term       { get; set; }
        public Chapter Chapter { get; set; }
    }
}