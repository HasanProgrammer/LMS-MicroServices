using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataModel
{
    /*Property | Fields*/
    public class Chapter
    {
        public string Id    { get; set; } /*Guid*/
        public string Title { get; set; }
    }
}