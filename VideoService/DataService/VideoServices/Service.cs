using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}