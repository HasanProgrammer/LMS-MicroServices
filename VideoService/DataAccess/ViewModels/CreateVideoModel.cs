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
    /*Video Fields | Main*/
    public partial class CreateVideoModel 
    {
        public int IsFree      { get; set; }
        public string Title    { get; set; }
        public string Duration { get; set; }
        public string File     { get; set; }
    }
    
    /*Chapter Fields*/
    public partial class CreateVideoModel
    {
        public string ChapterId    { get; set; }
        public string ChapterTitle { get; set; }
    }
}