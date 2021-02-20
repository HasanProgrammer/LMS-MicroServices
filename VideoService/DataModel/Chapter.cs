using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataModel
{
    /*Property | Fields*/
    public partial class Chapter
    {
        public string Id    { get; set; } /*Guid*/
        public string Title { get; set; }
    }

    /*Relation*/
    public partial class Chapter
    {
        public User User { get; set; } /*سازنده فصل مربوطه*/
        public Term Term { get; set; }
    }
}