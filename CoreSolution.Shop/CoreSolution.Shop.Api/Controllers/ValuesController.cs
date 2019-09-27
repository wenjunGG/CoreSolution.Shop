using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CoreSolutio.Service;
using CoreSolution.Domain;
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

            UserInfoDto user = new UserInfoDto() { Id = Guid.NewGuid(), UserName = "liwenjun" };

            _IUserInfoService.insert(user);

            return new string[] { "value1", "value2" };
        }
        
    }
}
