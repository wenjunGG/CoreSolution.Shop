using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CoreSolutio.Service;
using CoreSolution.Domain;
using CoreSolution.Domain.Entity;
using CoreSolution.Dto.Entity;
using CoreSolution.IService;
using Microsoft.AspNetCore.Mvc;


namespace CoreSolution.Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUserInfoService _IUserInfoService;

        public ValuesController(IUserInfoService IUserInfoService)
        {
            _IUserInfoService = IUserInfoService;
        }


        // GET api/values
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {



            #region 插入
            //UserInfo user = new UserInfo() { UserName = "liwenjun", UserPwd = "123456", UserAge = 12 };
            // _IUserInfoService.insert(user);
            #endregion

            #region  获取单个实体

            //根据ID获取
            var UserDto =_IUserInfoService.GetEntityDto(Guid.Parse("24D29F59-080D-49B4-BB1C-EC7467C2B01F"));

            //根据Expression获取
            //不支持下面写法
            //var UserExpressionDto = _IUserInfoService.GetEntityDto(t=>t.Id== Guid.Parse("24D29F59-080D-49B4-BB1C-EC7467C2B01F"));
            Guid id = Guid.Parse("34D29F59-080D-49B4-BB1C-EC7467C2B01F");
            var UserExpressionDto = _IUserInfoService.GetEntityDto(t=>t.Id== id);
            #endregion


            #region 列表
            int outTotal = 0;
            var ListTotal = _IUserInfoService.GetEntityDtoList(t => t.IsDelete == false, t => t.CreateDateTime, t => new UserInfoDto { Id = t.Id, UserName = t.UserName, UserAge = t.UserAge, UserPwd = t.UserPwd },out outTotal);

            var List= _IUserInfoService.GetEntityDtoList(t => t.IsDelete == false, t => t.CreateDateTime, t => new UserInfoDto { Id = t.Id, UserName = t.UserName, UserAge = t.UserAge, UserPwd = t.UserPwd });
            #endregion

            #region 更新
            UserExpressionDto.UserPwd = "li_ooo";
            _IUserInfoService.Update(UserExpressionDto);

            #endregion


            return new string[] { "value1", "value2" };

            #region 存在的问题
            /*
             * 1.列表查询 没有按照 Lambda 的方式走,现在是通过参数进行传递的
             * 2.base实体 required 没有selector 的方式，返回时 CtreatTime，却存在值
             * 3.
             * 4.
             */
            #endregion

        }

    }
}
