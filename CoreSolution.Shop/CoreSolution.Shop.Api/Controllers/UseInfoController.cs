using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoreSolution.Dto.Entity;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreSolution.Tools.Extensions;

namespace CoreSolution.Shop.Api.Controllers
{
    [Route("api/UseInfo")]
    [ApiController]

    public class UseInfoController : ControllerBase
    {

        private readonly IUserInfoService _IUserInfoService;
        public UseInfoController(IUserInfoService iUserInfoService)
        {
            _IUserInfoService = iUserInfoService;
        }

        // GET: api/UseInfo
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserInfo")]
        public JsonResult GetUserInfo(Guid Id)
        {
            var result=_IUserInfoService.GetEntityDto(Id);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DoLogin")]
        public JsonResult DoLogin(string userNameOrPhone,string userPwd)
        {
            UserInfoDto user = new UserInfoDto();
            if (userNameOrPhone.IsNumeric())
            {
                user = _IUserInfoService.GetEntityDto(t => t.PhoneNum == userNameOrPhone);
                if (user == null)
                {
                    return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "手机号不存在");
                }

                if (!user.IsPhoneNumConfirmed)
                {
                    return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "请激活该账号");
                }
            }
            else
            {
                user = _IUserInfoService.GetEntityDto(t => t.UserName == userNameOrPhone);
                if (user == null)
                {
                    return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "账号不存在");
                }
            }

            //判断密码
            if(userPwd.ToMd5().ToUpper()== user.Password.ToUpper())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "登陆成功", user);
            }
            else
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "密码错误");
            }
        }


    }
}
