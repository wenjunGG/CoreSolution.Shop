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
    [Route("api/Values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUserInfoService _IUserInfoService;
        private readonly IMenuService _IMenuService;
        private readonly IRoleService _IRoleService;
        private readonly IUserRoleService _IUserRoleService;
        private readonly IMenuRoleService _IMenuRoleService;

        public ValuesController(IUserInfoService IUserInfoService, IMenuService IMenuService, IRoleService IRoleService, 
            IUserRoleService IUserRoleService, IMenuRoleService IMenuRoleService)
        {
            _IUserInfoService = IUserInfoService;
            _IMenuService = IMenuService;
            _IRoleService = IRoleService;
            _IUserRoleService = IUserRoleService;
            _IMenuRoleService = IMenuRoleService;
        }


        // GET api/values
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [Route("Get")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            #region 插入
            //UserInfo user = new UserInfo() { UserName = "liwenjun", UserPwd = "123456", UserAge = 12 };
            // _IUserInfoService.insert(user);
            #endregion

            #region  获取单个实体

            //根据ID获取
            var UserDto = _IUserInfoService.GetEntityDto(Guid.Parse("24D29F59-080D-49B4-BB1C-EC7467C2B01F"));

            //根据Expression获取
            //不支持下面写法
            //var UserExpressionDto = _IUserInfoService.GetEntityDto(t=>t.Id== Guid.Parse("24D29F59-080D-49B4-BB1C-EC7467C2B01F"));
            Guid id = Guid.Parse("34D29F59-080D-49B4-BB1C-EC7467C2B01F");
            var UserExpressionDto = _IUserInfoService.GetEntityDto(t => t.Id == id);
            #endregion


            #region 列表
            int outTotal = 0;
            var ListTotal = _IUserInfoService.GetEntityDtoList(t => t.IsDelete == false, t => t.CreateDateTime, t => new UserInfoDto { Id = t.Id, UserName = t.UserName, Age = t.Age, Password = t.Password }, out outTotal);

            var List = _IUserInfoService.GetEntityDtoList(t => t.IsDelete == false, t => t.CreateDateTime, t => new UserInfoDto { Id = t.Id, UserName = t.UserName, Age = t.Age, Password = t.Password });
            #endregion

            #region 更新
            UserExpressionDto.Password = "li_ooo";
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

        /// <summary>
        /// 测试 添加管理员
        /// </summary>
        /// <returns></returns>
        [Route("AddAdmin")]
        [HttpGet]
        public ActionResult<string> AddAdmin()
        {
            UserInfoDto user = new UserInfoDto()
            {
                UserName = "shop_admin",
                Password = "e10adc3949ba59abbe56e057f20f883e",
                Age = 30,
                Email = "847055719@qq.com",
                IsEmailConfirmed = true,
                PhoneNum = "18217312775",
                IsPhoneNumConfirmed = true,
                RealName = "lwj",
                Remark = "管理员",
                Sex = true,
                UserType = Dto.Eum.UserType.GeneralUser
            };

            _IUserInfoService.insert(user);

            return "添加管理员用户成功";
        }

        /// <summary>
        /// 测试角色
        /// </summary>
        /// <returns></returns>
        [Route("AddRole")]
        [HttpGet]
        public ActionResult<string> AddRole()
        {
            RoleDto role = new RoleDto()
            {
                 Name="超级管理员",
                 Description="超级管理员角色"
            };

            _IRoleService.insert(role);
            return "添加角色成功";
        }


        /// <summary>
        /// 测试添加菜单
        /// </summary>
        /// <returns></returns>
        [Route("AddMenu")]
        [HttpGet]
        public ActionResult<string> AddMenu()
        {
            MenuDto menu = new MenuDto()
            {
                Name = "设置",
                Url = "www.baidu.com"
            };
            _IMenuService.insert(menu);
            
            MenuDto menu_user = new MenuDto()
            {
                Name = "用户列表",
                Url = "www.baidu.com",
                ParentId= menu.Id
            };
            _IMenuService.insert(menu_user);

            MenuDto menu_Role = new MenuDto()
            {
                Name = "角色列表",
                Url = "www.baidu.com",
                ParentId=menu.Id
            };
            _IMenuService.insert(menu_Role);

            return "添加菜单成功";
        }


       
    }
}
