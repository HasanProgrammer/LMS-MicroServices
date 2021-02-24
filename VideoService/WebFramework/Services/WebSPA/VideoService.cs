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

namespace WebFramework.Services.WebSPA
{
    public class VideoService
    {
        private readonly VideoRepository<Video>     _VideoService;
        private readonly ChapterRepository<Chapter> _ChapterService;
        
        public VideoService
        (
            VideoRepository<Video>     VideoService,
            ChapterRepository<Chapter> ChapterService
        )
        {
            _VideoService   = VideoService;
            _ChapterService = ChapterService;
        }

        public async Task<List<VideosViewModel>> GetAllAsync(string tokenKey)
        {
            /*باید نقش کاربری ( Role ) که توکن خود را به این مسیر ارسال کرده بازیابی کرد*/
            JwtSecurityToken token = new JwtSecurityTokenHandler().ReadToken(tokenKey) as JwtSecurityToken;

            /*اگر کاربر Admin بود می بایست ، لیست تمام فیلم ها را واکشی کرد*/
            /*اگر کاربر غیر Admin بود ، می بایست فیلم های مربوط به کاربر را واکشی کرد*/
            if(token.Claims.FirstOrDefault(claim => claim.Type == "Role").Value.Equals("Admin"))
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
                    DateUpdated  = video.UpdatedAt
                }).ToList();

            return await GetAllForUserAsync(token.Claims.FirstOrDefault(claim => claim.Type == "UniqueId").Value);
        }
        
        public async Task<List<VideosViewModel>> GetAllForUserAsync(string id)
        {
            return (await _VideoService.FindAllForUserAsNoTrackingAsync(id)).Select(video => new VideosViewModel
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
                DateUpdated  = video.UpdatedAt
            }).ToList();
        }

        public async Task<bool> AddAsync(CreateVideoModel model)
        {
            /*در این قسمت بررسی می شود که آیا Title فیلم مورد نظر، قبلا انتخاب شده است یا خیر*/
            if (await _VideoService.FindWithTitleAsNoTrackingAsync(model.Title) != null) throw new UniqueTitleFieldException("فیلد ( عنوان ) باید یکتا باشد");

            /*در این قسمت پس از بررسی های لازمه و اعتبارسنجی ثانویه ViewModel ؛ یک فیلم جدید منتشر می گردد*/
            if (await _ChapterService.FindWithIdAsNoTrackingAsync(model.ChapterId) == null)
                await _ChapterService.AddAsync(new Chapter
                {
                    Id     = model.ChapterId,
                    TermId = model.TermId,
                    Title  = model.Title
                });
            
            return await _VideoService.AddAsync(new Video
            {
                Id        = Guid.NewGuid().ToString(),
                UserId    = model.UserId,
                TermId    = model.TermId,
                ChapterId = model.ChapterId,
                Title     = model.Title,
                Duration  = model.Duration,
                VideoFile = model.File,
                IsFree    = Convert.ToBoolean(model.IsFree),
                CreatedAt = PersianDatetime.Now(),
                UpdatedAt = PersianDatetime.Now(),
                Status    = DataModel.Enums.Video.Status.Active
            });
        }

        public async Task<bool> ChangeAsync(string id, EditVideoModel model, string tokenKey)
        {
            /*در این قسمت باید بررسی شود که اصلا این فیلم برنامه نویسی وجود دارد یا خیر*/
            Video video = await _VideoService.FindWithIdAsync(id);
            if (video == null) throw new NotFoundException("فیلم مورد نظر وجود خارجی ندارد");
            
            /*در این قسمت باید بررسی گردد که فیلم مربوطه را Admin ویرایش می کنه یا کاربری دیگه ، ( اگر کاربری دیگه ویرایش می کنه ؛ باید معلوم شه این فیلم واسه خود کاربره و نه کس دیگه ؛ ACL*/
            JwtSecurityToken token = new JwtSecurityTokenHandler().ReadToken(tokenKey) as JwtSecurityToken;
            if (!token.Claims.FirstOrDefault(claim => claim.Type == "Role").Value.Equals("Admin"))
                if (!token.Claims.FirstOrDefault(claim => claim.Type == "UniqueId").Value.Equals(video.UserId))
                    throw new AclException("شما دسترسی لازم برای ویرایش فیلم مورد نظر را دارا نمی باشید");
            
            /*در این قسمت فیلم مورد نظر ویرایش می گردد*/
            if(video.ChapterId != model.ChapterId)
                if (await _ChapterService.FindWithIdAsNoTrackingAsync(model.ChapterId) == null) /*در این قسمت بررسی می گردد که اگر Chapter فرستاده شده ، در جدول Chapters موجود نیست ، این مورد به جدول Chapters اضافه گردد*/
                    await _ChapterService.AddAsync(new Chapter
                    {
                        Id     = model.ChapterId,
                        TermId = model.TermId,
                        Title  = model.Title
                    });

            video.TermId    = model.TermId;
            video.ChapterId = model.ChapterId;
            video.IsFree    = Convert.ToBoolean(model.IsFree);
            video.Title     = model.Title;
            video.Duration  = model.Duration;
            video.UpdatedAt = PersianDatetime.Now();

            if (model.File != null) video.VideoFile = model.File;

            return await _VideoService.ChangeAsync(video, id);
        }

        public async Task<bool> ActiveAsync(string id, string tokenKey)
        {
            /*در این قسمت باید بررسی شود که اصلا این فیلم برنامه نویسی وجود دارد یا خیر*/
            Video video = await _VideoService.FindWithIdAsync(id);
            if (video == null) throw new NotFoundException("فیلم مورد نظر وجود خارجی ندارد");
            
            /*در این قسمت باید بررسی گردد که فیلم مربوطه را Admin ویرایش می کنه یا کاربری دیگه ، ( اگر کاربری دیگه ویرایش می کنه ؛ باید معلوم شه این فیلم واسه خود کاربره و نه کس دیگه ؛ ACL*/
            JwtSecurityToken token = new JwtSecurityTokenHandler().ReadToken(tokenKey) as JwtSecurityToken;
            if (!token.Claims.FirstOrDefault(claim => claim.Type == "Role").Value.Equals("Admin"))
                if (!token.Claims.FirstOrDefault(claim => claim.Type == "UniqueId").Value.Equals(video.UserId))
                    throw new AclException("شما دسترسی لازم برای ویرایش فیلم مورد نظر را دارا نمی باشید");

            video.Status    = DataModel.Enums.Video.Status.Active;
            video.UpdatedAt = PersianDatetime.Now();

            return await _VideoService.ChangeAsync(video, id);
        }
        
        public async Task<bool> InActiveAsync(string id, string tokenKey)
        {
            /*در این قسمت باید بررسی شود که اصلا این فیلم برنامه نویسی وجود دارد یا خیر*/
            Video video = await _VideoService.FindWithIdAsync(id);
            if (video == null) throw new NotFoundException("فیلم مورد نظر وجود خارجی ندارد");
            
            /*در این قسمت باید بررسی گردد که فیلم مربوطه را Admin ویرایش می کنه یا کاربری دیگه ، ( اگر کاربری دیگه ویرایش می کنه ؛ باید معلوم شه این فیلم واسه خود کاربره و نه کس دیگه ) ؛ ACL*/
            JwtSecurityToken token = new JwtSecurityTokenHandler().ReadToken(tokenKey) as JwtSecurityToken;
            if (!token.Claims.FirstOrDefault(claim => claim.Type == "Role").Value.Equals("Admin"))
                if (!token.Claims.FirstOrDefault(claim => claim.Type == "UniqueId").Value.Equals(video.UserId))
                    throw new AclException("شما دسترسی لازم برای ویرایش فیلم مورد نظر را دارا نمی باشید");

            video.Status    = DataModel.Enums.Video.Status.Inactive;
            video.UpdatedAt = PersianDatetime.Now();

            return await _VideoService.ChangeAsync(video, id);
        }

        public async Task<bool> RemoveAsync(string id, string tokenKey)
        {
            /*در این قسمت باید بررسی شود که اصلا این فیلم برنامه نویسی وجود دارد یا خیر*/
            Video video = await _VideoService.FindWithIdAsync(id);
            if (video == null) throw new NotFoundException("فیلم مورد نظر وجود خارجی ندارد");
            
            /*در این قسمت باید بررسی گردد که فیلم مربوطه را Admin ویرایش می کنه یا کاربری دیگه ، ( اگر کاربری دیگه ویرایش می کنه ؛ باید معلوم شه این فیلم واسه خود کاربره و نه کس دیگه ) ؛ ACL*/
            JwtSecurityToken token = new JwtSecurityTokenHandler().ReadToken(tokenKey) as JwtSecurityToken;
            if (!token.Claims.FirstOrDefault(claim => claim.Type == "Role").Value.Equals("Admin"))
                if (!token.Claims.FirstOrDefault(claim => claim.Type == "UniqueId").Value.Equals(video.UserId))
                    throw new AclException("شما دسترسی لازم برای حذف فیلم مورد نظر را دارا نمی باشید");

            return await _VideoService.RemoveAsync(id);
        }
    }
}