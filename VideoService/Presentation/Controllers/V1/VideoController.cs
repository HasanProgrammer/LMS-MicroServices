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
using WebFramework.Services.WebSPA;

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
            return JsonResponse.Return(_StatusCode.SuccessFetchData, _StatusMessage.SuccessFetchData, new
            {
                videos = _VideoService.GetAllAsync(await HttpContext.GetTokenAsync("access_token"))
            });
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

        [HttpPut]
        [Route(template: "edit/{id}", Name = "Video.Edit")]
        [ServiceFilter(typeof(ModelValidation))]
        public async Task<JsonResult> Edit(string id, EditVideoModel model)
        {
            try
            {
                if(await _VideoService.ChangeAsync(id, model, await HttpContext.GetTokenAsync("access_token")))
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

        [HttpPatch]
        [Route(template: "active/{id}", Name = "Video.Active")]
        public async Task<JsonResult> Active(string id)
        {
            try
            {
                if(await _VideoService.ActiveAsync(id, await HttpContext.GetTokenAsync("access_token")))
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
        
        [HttpPatch]
        [Route(template: "inactive/{id}", Name = "Video.InActive")]
        public async Task<JsonResult> InActive(string id)
        {
            try
            {
                if(await _VideoService.InActiveAsync(id, await HttpContext.GetTokenAsync("access_token")))
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

        [HttpDelete]
        [Route(template: "delete/{id}", Name = "Video.Delete")]
        public async Task<JsonResult> Delete(string id)
        {
            try
            {
                if(await _VideoService.RemoveAsync(id, await HttpContext.GetTokenAsync("access_token")))
                    return JsonResponse.Return(_StatusCode.SuccessDelete, _StatusMessage.SuccessDelete, new { });
                return JsonResponse.Return(_StatusCode.ErrorDelete, _StatusMessage.ErrorDelete, new { });
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