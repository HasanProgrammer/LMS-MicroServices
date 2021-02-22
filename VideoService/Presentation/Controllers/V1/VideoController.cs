using System;
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebFramework.Exceptions;
using WebFramework.Filters;
using WebFramework.Services;

namespace Presentation.Controllers.V1
{
    [ApiVersion(version: "1.0")]
    public class VideoController : BaseVideoController
    {
        //Services
        private readonly VideoService _VideoService;
        
        //Configs
        private readonly Config.StatusCode _StatusCode;
        private readonly Config.Messages   _StatusMessage;
        
        public VideoController
        (
            VideoService                VideoService,
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
        [Route(template: "", Name = "Video.All")]
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
        [ServiceFilter(typeof(ModelValidation))]
        public async Task<JsonResult> Create(CreateVideoModel model)
        {
            try
            {
                if (await _VideoService.AddAsync(model))
                    return JsonResponse.Return(_StatusCode.SuccessCreate, _StatusMessage.SuccessCreate, new { });
                return JsonResponse.Return(_StatusCode.ErrorCreate, _StatusMessage.ErrorCreate, new { });
            }
            catch (UniqueTitleFieldException e)
            {
                throw new UniqueTitleFieldException(e.Message);
            }
        }

        [HttpPatch]
        [Route(template: "edit/{id:int}", Name = "Video.Edit")]
        [ServiceFilter(typeof(ModelValidation))]
        public async Task<JsonResult> Edit(int id, EditVideoModel model)
        {
            try
            {
                if(await _VideoService.ChangeAsync(id, model, HttpContext))
                    return JsonResponse.Return(_StatusCode.SuccessEdit, _StatusMessage.SuccessEdit, new { });
                return JsonResponse.Return(_StatusCode.ErrorEdit, _StatusMessage.ErrorEdit, new { });
            }
            catch (NotFoundException e)
            {
                throw new NotFoundException(e.Message);
            }
            catch (AclException e)
            {
                throw new AclException(e.Message);
            }
        }
    }
}