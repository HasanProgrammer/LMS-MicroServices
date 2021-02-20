using System.ComponentModel.DataAnnotations;

namespace DataAccess.ViewModels
{
    public class CreateVideoModel
    {
        public string ChapterId { get; set; }
        
        [Required(ErrorMessage = "فیلد ( دوره ) الزامی می باشد")]
        public string TermId { get; set; }
        
        [Required(ErrorMessage = "فیلد ( رایگان بودن یا نبودن ) الزامی می باشد")]
        public int? IsFree { get; set; }
        
        [Required(ErrorMessage = "فیلد ( عنوان ) الزامی می باشد")]
        public string Title { get; set; }
    }
}