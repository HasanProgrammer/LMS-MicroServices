using MongoDB.Bson;
using Status = DataModel.Enums.Term.Status;

namespace DataModel
{
    /*Property | Fields*/
    public partial class Term
    {
        public string Id          { get; set; } /*Guid*/
        public string Name        { get; set; }
        public string Description { get; set; }
        public string Suitable    { get; set; }
        public string Result      { get; set; }
        public int Price          { get; set; }
        public bool HasChapter    { get; set; }
        public Status Status      { get; set; }
        public string DateStart   { get; set; } /*تاریخ شروع برنامه نویسی*/
        public string DateEnd     { get; set; } /*تاریخ پایان برنامه نویسی*/
    }

    /*Relation*/
    public partial class Term
    {
        
    }
}