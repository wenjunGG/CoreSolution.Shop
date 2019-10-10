using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Dto.Entity;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.Shop.Api.Controllers
{
    [Route("api/Menu")]
    [ApiController]
    public class MenuController : ControllerBase
    {

        private readonly IMenuService _IMenuService;
        public MenuController(IMenuService menuController)
        {
            _IMenuService = menuController;
        }
        
        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMenuInfo")]
        public JsonResult GetMenuInfo(Guid Id)
        {
            var result = _IMenuService.GetEntityDto(Id);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }
        
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMenuInfoList")]
        public JsonResult GetMenuInfoList()
        {
            var ListMenu=_IMenuService.GetMenuList(new List<Guid>());
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", ListMenu);
        }

        /// <summary>
        /// 获取当前用户的菜单
        /// </summary>
        /// <param name="Token">Token</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCurrentMenu")]
        public JsonResult GetCurrentMenu(string Token)
        {
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功");
        }



    }
}