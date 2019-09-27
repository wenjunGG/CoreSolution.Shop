using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Script.Serialization;

namespace CoreSolution.Tools
{
    public class Oauth2
    {
        //JavaScriptSerializer Jss = new JavaScriptSerializer();
        public Oauth2() { }

        /// <summary>
        /// 获取用户code
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="redirect_uri">重定向url</param>
        public void GetCodeUrl(string clientId, string getcodeurl, string redirect_uri)
        {
            // string url = string.Format(" http://api.eshimin.com/api/oauth/authorize?client_id={0}&response_type=code&redirect_uri={1}&scope=read", clientId, redirect_uri);
            string url = string.Format(getcodeurl, clientId, redirect_uri);
            CommonMethod.WebRequestPostOrGet(url, "");
            WriteLogHelper.WriteLogDoc("oauthlog", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + url, "oauthlog");


        }


        /// <summary>
        /// 用code 获取accesstoken，获取用户信息
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientsecret"></param>
        /// <param name="redirecturl">重定向地址，</param>
        /// <param name="Code">code</param>
        /// <returns>返回用户信息</returns>
        public string GetTokenInfo(string clientId, string clientsecret, string GetTokenUrl, string Code)
        {

            try
            {
                WriteLogHelper.WriteLogDoc("oauthlog", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + GetTokenUrl + "," + clientsecret + "," + Code, "oauthlog");
                string url = string.Format(GetTokenUrl, clientId, clientsecret, Code);
                string ReText = CommonMethod.GetHttpResponse(url, 1200);//post/get方法获取信息

                WriteLogHelper.WriteLogDoc("oauthlog", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + ReText, "oauthlog");

                Dictionary<string, object> DicText = JsonConvert.DeserializeObject<Dictionary<string, object>>(ReText);


                if (!DicText.ContainsKey("access_token"))
                {
                    return "获取access_token失败";
                }
                else
                {

                    WriteLogHelper.WriteLogDoc("oauthlog", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + DicText["user"] + DicText["access_token"], "oauthlog");

                    return DicText["access_token"].ToString();

                }
            }
            catch (Exception ex)
            {
                WriteLogHelper.WriteLogDoc("oauthlog", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + ex.Message, "oauthlog");
                return "";
            }
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="accesstoken"></param>
        /// <param name="GetUserInfoUrl"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetUserInfo(string accesstoken, string GetUserInfoUrl)
        {
            try
            {
                string url = string.Format(GetUserInfoUrl, accesstoken);

                WriteLogHelper.WriteLogDoc("oauthlog", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + url, "oauthlog");

                string returnText = CommonMethod.GetHttpResponse(url, 1200);
                Dictionary<string, object> DicText = JsonConvert.DeserializeObject<Dictionary<string, object>>(returnText);

                return DicText;
            }
            catch (Exception ex)
            {
                WriteLogHelper.WriteLogDoc("oauthlog", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + ex.Message, "oauthlog");
                return null;
            }
        }


    }

}

