using MongoDB.Bson;

namespace DataAccess.ViewModels
{
    public class VideosViewModel
    {
        public string Id          { get; set; } /*Guid*/
        public string Title       { get; set; }
        public string Duration    { get; set; }
        public string Video       { get; set; }
        public int IsFreeKey      { get; set; }
        public string IsFreeValue { get; set; }
        public int StatusKey      { get; set; }
        public string StatusValue { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
        public string ChapterId  { get; set; }
        public string ChapterTitle { get; set; }
    }
}