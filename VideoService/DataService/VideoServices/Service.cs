using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using DataAccess.CustomRepositories;
using DataAccess.ViewModels;
using DataModel;

namespace DataService.VideoServices
{
    public class Service
    {
        private readonly VideoRepository<Video> _VideoService;
        
        public Service(VideoRepository<Video> VideoService)
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
            if (await _VideoService.FindWithTitleAsync(model.Title) != null) throw new Exception("فیلد ( عنوان ) باید یکتا باشد");

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
    }
}