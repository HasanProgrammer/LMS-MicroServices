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
    
    /*User Fields*/
    public partial class CreateVideoModel
    {
        public string UserId          { get; set; }
        public string UserImage       { get; set; }
        public string UserName        { get; set; }
        public string UserEmail       { get; set; }
        public string UserPhone       { get; set; }
        public string UserExpert      { get; set; }
        public string UserDescription { get; set; }
    }
    
    /*Term Fields*/
    public partial class CreateVideoModel
    {
        public string TermId          { get; set; }
        public string TermName        { get; set; }
        public string TermDescription { get; set; }
        public string TermSuitable    { get; set; }
        public string TermResult      { get; set; }
        public int TermPrice          { get; set; }
        public bool TermHasChapter    { get; set; }
        public string TermDateStart   { get; set; }
        public string TermDateEnd     { get; set; }
    }
    
    /*Chapter Fields*/
    public partial class CreateVideoModel
    {
        public string ChapterId    { get; set; }
        public string ChapterTitle { get; set; }
    }
}