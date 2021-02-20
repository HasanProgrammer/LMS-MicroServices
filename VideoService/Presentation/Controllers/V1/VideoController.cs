using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Common;
using DataAccess.CustomRepositories;
using DataAccess.ViewModels;
using DataModel;
using DataService.VideoServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Presentation.Controllers.V1
{
    [ApiVersion(version: "1.0")]
    public class VideoController : BaseVideoController
    {
        //Services
        private readonly Service _VideoService;
        
        //Configs
        private readonly Config.StatusCode _StatusCode;
        private readonly Config.Messages   _StatusMessage;
        
        public VideoController
        (
            Service                     VideoService,
            IOptions<Config.StatusCode> StatusCode,
            IOptions<Config.Messages>   StatusMessage
        )
        {
            //Services
            _VideoService = VideoService;
            
            //Configs
            _StatusCode    = StatusCode.Value;
            _StatusMessage = StatusMessage.Value;
        }

        [HttpGet]
        [Route(template: "", Name = "Video.All.Paginate")]
        public async Task<JsonResult> Index()
        {
            /*باید نقش کاربری ( Role ) که توکن خود را به این مسیر ارسال کرده بازیابی کرد*/
            JwtSecurityToken token = new JwtSecurityTokenHandler().ReadToken(await HttpContext.GetTokenAsync("access_token")) as JwtSecurityToken;

            /*اگر کاربر Admin بود می بایست ، لیست تمام فیلم ها را واکشی کرد*/
            /*اگر کاربر غیر Admin بود ، می بایست فیلم های مربوط به کاربر را واکشی کرد*/
            List<VideosViewModel> videos = token.Claims.FirstOrDefault(claim => claim.Type == "Role").Value.Equals("Admin")
                                 ?
                                 await _VideoService.GetAllAsync()
                                 :
                                 await _VideoService.GetAllForUserAsync(token.Claims.FirstOrDefault(claim => claim.Type == "Username").Value);
            
            /*در این قسمت خروجی نهایی API فیلم ها به کاربر ارسال می گردد*/
            return JsonResponse.Return(_StatusCode.SuccessFetchData, _StatusMessage.SuccessFetchData, new { videos });
        }

        [HttpPost]
        [Route(template: "create", Name = "Video.Create")]
        public void Create()
        {
            
        }
    }
}