using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Common;
using DataAccess.CustomRepositories;
using DataAccess.ViewModels;
using DataModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using WebFramework.Exceptions;

namespace WebFramework.Services
{
    public class VideoService
    {
        private readonly VideoRepository<Video> _VideoService;
        
        public VideoService(VideoRepository<Video> VideoService)
        {
            _VideoService = VideoService;
        }

        public async Task<List<VideosViewModel>> GetAllAsync()
        {
            return (await _VideoService.FindAllAsync()).Select(video => new VideosViewModel
            {
                Id           = video.Id,
                Title        = video.Title,
                Duration     = video.Duration,
                Video        = video.VideoFile,
                IsFreeKey    = video.IsFree ? 1 : 0,
                IsFreeValue  = video.IsFree ? "رایگان" : "غیر رایگان",
                StatusKey    = video.Status == DataModel.Enums.Video.Status.Active ? 1 : 0,
                StatusValue  = video.Status == DataModel.Enums.Video.Status.Active ? "فعال" : "غیر فعال",
                DateCreated  = video.CreatedAt,
                DateUpdated  = video.UpdatedAt,
                UserId       = video.User.Id,
                UserName     = video.User.Username,
                UserImage    = video.User.ImageFile,
                ChapterId    = video.Chapter?.Id,
                ChapterTitle = video.Chapter?.Title,
                TermId       = video.Term?.Id,
                TermName     = video.Term.Name
            }).ToList();
        }
        
        public async Task<List<VideosViewModel>> GetAllForUserAsync(string username)
        {
            return (await _VideoService.FindAllForUserAsync(username)).Select(video => new VideosViewModel
            {
                Id           = video.Id,
                Title        = video.Title,
                Duration     = video.Duration,
                Video        = video.VideoFile,
                IsFreeKey    = video.IsFree ? 1 : 0,
                IsFreeValue  = video.IsFree ? "رایگان" : "غیر رایگان",
                StatusKey    = video.Status == DataModel.Enums.Video.Status.Active ? 1 : 0,
                StatusValue  = video.Status == DataModel.Enums.Video.Status.Active ? "فعال" : "غیر فعال",
                DateCreated  = video.CreatedAt,
                DateUpdated  = video.UpdatedAt,
                UserId       = video.User.Id,
                UserName     = video.User.Username,
                UserImage    = video.User.ImageFile,
                ChapterId    = video.Chapter?.Id,
                ChapterTitle = video.Chapter?.Title,
                TermId       = video.Term?.Id,
                TermName     = video.Term.Name
            }).ToList();
        }

        public async Task<bool> AddAsync(CreateVideoModel model)
        {
            /*در این قسمت بررسی می شود که آیا Title فیلم مورد نظر، قبلا انتخاب شده است یا خیر*/
            if (await _VideoService.FindWithTitleAsync(model.Title) != null) throw new UniqueTitleFieldException("فیلد ( عنوان ) باید یکتا باشد");

            /*در این قسمت پس از بررسی های لازمه و اعتبارسنجی ثانویه ViewModel ؛ یک فیلم جدید منتشر می گردد*/
            return await _VideoService.AddAsync(new Video
            {
                Id        = Guid.NewGuid().ToString(),
                Title     = model.Title,
                Duration  = model.Duration,
                VideoFile = model.File,
                IsFree    = Convert.ToBoolean(model.IsFree),
                CreatedAt = PersianDatetime.Now(),
                UpdatedAt = PersianDatetime.Now(),
                Status    = DataModel.Enums.Video.Status.Active,
                
                User = new User
                {
                    Id          = model.UserId,
                    ImageFile   = model.UserImage,
                    Username    = model.UserName,
                    Email       = model.UserEmail,
                    Phone       = model.UserPhone,
                    Expert      = model.UserExpert,
                    Description = model.UserDescription
                },
                
                Term = new Term
                {
                    Id          = model.TermId,
                    Name        = model.TermName,
                    Description = model.TermDescription,
                    Suitable    = model.TermSuitable,
                    Result      = model.TermResult,
                    Price       = model.TermPrice,
                    HasChapter  = model.TermHasChapter,
                    DateStart   = model.TermDateStart,
                    DateEnd     = model.TermDateEnd 
                },
                
                Chapter = new Chapter
                {
                    Id    = model.ChapterId,
                    Title = model.ChapterTitle 
                }
            });
        }

        public async Task<bool> ChangeAsync(int id, EditVideoModel model, HttpContext context)
        {
            /*در این قسمت باید بررسی شود که اصلا این فیلم برنامه نویسی وجود دارد یا خیر*/
            Video video = await _VideoService.FindWithIdAsync(id);
            if (video == null) throw new NotFoundException("فیلم مورد نظر وجود خارجی ندارد");
            
            /*در این قسمت باید بررسی گردد که فیلم مربوطه را Admin ویرایش می کنه یا کاربری دیگه ، ( اگر کاربری دیگه ویرایش می کنه ؛ باید معلوم شه این فیلم واسه خود کاربره و نه کس دیگه ؛ ACL*/
            JwtSecurityToken token = new JwtSecurityTokenHandler().ReadToken(await context.GetTokenAsync("access_token")) as JwtSecurityToken;
            if (!token.Claims.FirstOrDefault(claim => claim.Type == "Role").Value.Equals("Admin"))
                if (!token.Claims.FirstOrDefault(claim => claim.Type == "Username").Value.Equals(video.User.Username))
                    throw new AclException("شما دسترسی لازم برای ویرایش فیلم مورد نظر را دارا نمی باشید");
            
            /*در این قسمت فیلم مورد نظر ویرایش می گردد*/
            video.IsFree    = Convert.ToBoolean(model.IsFree);
            video.Title     = model.Title;
            video.Duration  = model.Duration;

            if (model.File != null) video.VideoFile = model.File;

            video.Term.Id          = model.TermId;
            video.Term.Name        = model.TermName;
            video.Term.Description = model.TermDescription;
            video.Term.Suitable    = model.TermSuitable;
            video.Term.Result      = model.TermResult;
            video.Term.Price       = model.TermPrice;
            video.Term.HasChapter  = model.TermHasChapter;
            video.Term.DateStart   = model.TermDateStart;
            video.Term.DateEnd     = model.TermDateEnd;
            video.Chapter.Id       = model.ChapterId;
            video.Chapter.Title    = model.ChapterTitle;

            await _VideoService.ChangeAsync(video, id);
            return true;
        }
    }
}