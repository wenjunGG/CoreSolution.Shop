using CoreSolution.Redis.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.Shop.Api.Manger
{
    public class LoginManager
    {
        private static string TOKEN_PREFIX = "Api.User.Token.";
        private static string USERID_PREFIX = "Api.User.UserId.";
        private static string USERPERMISSIONS_PREFIX = "Api.User.Permissions.";
        private static string USERROLES_PREFIX = "Api.User.Roles.";


        public static async Task LoginAsync(string token, Guid userId)
        {
            await RedisHelper.StringSetAsync(TOKEN_PREFIX + token, userId, TimeSpan.FromMinutes(30));
            await RedisHelper.StringSetAsync(USERID_PREFIX + userId, token, TimeSpan.FromHours(30));//正向、反向关系都保存，这样保证一个token只能登陆一次
        }
        /// <summary>
        /// 更新过期时间
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task UpdateTokenExpireDate(string token)
        {
            var userId = await GetUserIdAsync(token);
            await RedisHelper.KeyExpireAsync(TOKEN_PREFIX + token, TimeSpan.FromMinutes(30));
            await RedisHelper.KeyExpireAsync(USERID_PREFIX + userId, TimeSpan.FromMinutes(30));
            await RedisHelper.KeyExpireAsync(USERPERMISSIONS_PREFIX + userId, TimeSpan.FromMinutes(30));
            await RedisHelper.KeyExpireAsync(USERROLES_PREFIX + userId, TimeSpan.FromMinutes(30));


        }

        public static async Task SetActionCount(string controlAction, string token)
        {
            var value = await RedisHelper.StringGetAsync(controlAction + token);
            int numDid = 1;
            if (!String.IsNullOrEmpty(value))
            {
                numDid += int.Parse(value);
            }
            if (numDid > 1)
            {
                await RedisHelper.StringSetRangeAsync(controlAction + token, 0, numDid);
            }
            else
            {
                await RedisHelper.StringSetAsync(controlAction + token, numDid, TimeSpan.FromMinutes(1));
            }

        }

        public static async Task<int> GetActionCount(string controlAction, string token)
        {
            int returnvalue = 0;
            var value = await RedisHelper.StringGetAsync(controlAction + token);
            if (!String.IsNullOrEmpty(value))
            {
                returnvalue = int.Parse(value);
            }

            return returnvalue;

        }


        /// <summary>
        /// 缓存当前用户所具有的权限
        /// </summary>
        /// <param name="permissions">权限数组</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public static async Task SaveCurrentUserPermissionsAsync(string[] permissions, Guid userId)
        {
            await RedisHelper.StringSetAsync(USERPERMISSIONS_PREFIX + userId, permissions, TimeSpan.FromMinutes(30));
        }

        public static async Task SaveCurrentUserRolesAsync(string[] roles, Guid userId)
        {
            await RedisHelper.StringSetAsync(USERROLES_PREFIX + userId, roles, TimeSpan.FromMinutes(30));
        }



        public static async Task<string[]> GetCurrentUserPermissionsAsync(Guid userId)
        {
            return await RedisHelper.StringGetAsync<string[]>(USERPERMISSIONS_PREFIX + userId);
        }

        public static async Task<bool> ClearRedisData(Guid userId, string token)
        {
            await RedisHelper.KeyDeleteAsync(USERPERMISSIONS_PREFIX + userId);
            await RedisHelper.KeyDeleteAsync(USERROLES_PREFIX + userId);
            await RedisHelper.KeyDeleteAsync(TOKEN_PREFIX + token);
            return await RedisHelper.KeyDeleteAsync(USERID_PREFIX + userId);
        }

        public static async Task<string[]> GetCurrentUserRolesAsync(Guid userId)
        {
            return await RedisHelper.StringGetAsync<string[]>(USERROLES_PREFIX + userId);
        }




        public static async Task<Guid?> GetUserIdAsync(string token)
        {
            Guid? userId = await RedisHelper.StringGetAsync<Guid?>(TOKEN_PREFIX + token);
            if (userId == null)
            {
                return null;
            }
            string revertToken = await RedisHelper.StringGetAsync(USERID_PREFIX + userId);
            if (revertToken != token)//如果反向查的token不一样，说明这个token已经过期了
            {
                return null;
            }
            return userId;
        }

        public static async Task<string> GetTokeByUserIdAsync(Guid? userId)
        {
            string token = await RedisHelper.StringGetAsync(USERID_PREFIX + userId);
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }
            Guid? revertuserId = await RedisHelper.StringGetAsync<Guid?>(TOKEN_PREFIX + token);
            if (revertuserId != userId)//如果反向查的token不一样，说明这个token已经过期了
            {
                return null;
            }
            return token;
        }


        /// <summary>
        /// 得到错误登录次数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static async Task<int> GetErrorLoginTimesAsync(string redisKey, string email)
        {
            string key = redisKey + email;
            int? count = await RedisHelper.StringGetAsync<int?>(key);
            if (count == null)
            {
                count = 0;
            }
            return (int)count;
        }

        /// <summary>
        /// 重置登录错误次数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="email"></param>
        public static void ResetErrorLogin(string redisKey, string email)
        {
            string key = redisKey + email;
            RedisHelper.KeyRemove(key);
        }

        /// <summary>
        /// 递增登录错误次数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="email"></param>
        public static async Task IncreaseErrorLoginAsync(string redisKey, string email)
        {
            string key = redisKey + email;
            int? count = await RedisHelper.StringGetAsync<int?>(key) ?? 0;
            count++;
            await RedisHelper.StringSetAsync(key, count, TimeSpan.FromMinutes(15));//超时时间15分钟
        }
    }
}
