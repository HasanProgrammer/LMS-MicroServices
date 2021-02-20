using System.ComponentModel.DataAnnotations;
using Common;
using DataAccess.CustomRepositories;
using DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataAccess.ViewModels
{
    public class CreateVideoModel
    {
        public string ChapterId { get; set; }
        
        [Required(ErrorMessage = "فیلد ( دوره ) الزامی می باشد")]
        public string TermId { get; set; }
        
        [Required(ErrorMessage = "فیلد ( رایگان بودن یا نبودن ) الزامی می باشد")]
        [CheckIsFreeKey(ErrorMessage = "فیلد ( رایگان بودن یا نبودن ) باید یک عدد ( 0 یا 1 ) باشد")]
        public int? IsFree { get; set; }
        
        [Required(ErrorMessage = "فیلد ( عنوان ) الزامی می باشد")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "فیلد ( مدت زمان ) الزامی می باشد")]
        public string Duration { get; set; }
        
        [Required(ErrorMessage = "فیلد ( فایل ) الزامی می باشد")]
        public string File { get; set; }
        
        /*-----------------------------------------------------------*/
        
        public class CheckIsFreeKey : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value != null)
                {
                    if((int)value != 0 && (int)value != 1)
                        return new ValidationResult(ErrorMessage);
                    return ValidationResult.Success;
                }
                
                return ValidationResult.Success;
            }
        }
    }
}