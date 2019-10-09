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
using CoreSolution.Shop.Api.Manger;
using Microsoft.AspNetCore.Cors;
using CoreSolution.Shop.Api.Model;

namespace CoreSolution.Shop.Api.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Route("api/UseInfo")]
    [Produces("application/json")]
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
        /// <param name="token">Token</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserInfo")]
        public async Task<JsonResult> GetUserInfo(string token)
        {
            var userId=await LoginManager.GetUserIdAsync(token);
            if (userId.HasValue)
            {
                var result = _IUserInfoService.GetEntityDto(userId.Value);
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
            }
            else
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "token失效");
            }
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="loginInfo">登陆信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("DoLogin")]
        public async Task<JsonResult> DoLogin(UserLogin loginInfo)
        {
            if (string.IsNullOrEmpty(loginInfo.username))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "用户名不能为空");
            }

            if (string.IsNullOrEmpty(loginInfo.password))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "密码不能为空");
            }
            
            //校验过程
            UserInfoDto user = new UserInfoDto();
            if (loginInfo.username.IsNumeric())
            {
                user = _IUserInfoService.GetEntityDto(t => t.PhoneNum == loginInfo.username);
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
                user = _IUserInfoService.GetEntityDto(t => t.UserName == loginInfo.username);
                if (user == null)
                {
                    return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "账号不存在");
                }
            }

            //判断密码
            if(loginInfo.password.ToMd5().ToUpper()== user.Password.ToUpper())
            {
                //获取已登录的信息,清除已登录用户
                var alreadytoken = await LoginManager.GetTokeByUserIdAsync(user.Id);

                if (!string.IsNullOrEmpty(alreadytoken))
                    await LoginManager.ClearRedisData(user.Id, alreadytoken);
                
                //设置Token
                string token = Guid.NewGuid().ToString();
                await LoginManager.LoginAsync(token, user.Id);

                var userInfo = new
                {
                    token= token,
                    userName=user.UserName,
                    realName=user.RealName,
                    userType = user.UserType,
                    Sex = user.Sex,
                    Picture = user.Picture,
                    PhoneNum = user.PhoneNum,
                };

                return AjaxHelper.JsonResult(HttpStatusCode.OK, "登陆成功", userInfo);
            }
            else
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "密码错误");
            }
        }

        /// <summary>
        /// 退出登陆
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("OutLogin")]
        public async Task<JsonResult> OutLogin()
        {
            string token = HttpContext.Request.Headers["token"];
            var userId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();

            //获取已登录的信息,清除已登录用户
            var alreadytoken = await LoginManager.GetTokeByUserIdAsync(userId);
            if (!string.IsNullOrEmpty(alreadytoken))
                await LoginManager.ClearRedisData(userId, alreadytoken);

            return AjaxHelper.JsonResult(HttpStatusCode.OK, "退出登陆");
        }


    }
}
