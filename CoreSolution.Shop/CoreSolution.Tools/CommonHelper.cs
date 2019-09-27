﻿

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Web;

namespace CoreSolution.Tools
{
    public class CommonHelper
    {
        public static bool StrValid(string val)
        {
            if (!string.IsNullOrEmpty(val))
            {
                return false;
            }
            return true;
        }
        public static string StrValid(string val, string falseval)
        {
            if (!string.IsNullOrEmpty(val))
            {
                return val;
            }
            return falseval;
        }
        public static string ToYYYYMMDDHHMMSS(DateTime dt)
        {
            try
            {
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch
            {
                return "";
            }
        }
        public static string ToHHmmHHmm(DateTime dt1, DateTime dt2)
        {
            try
            {
                string year = dt1.ToString("yyyy-MM-dd  HH:mm");
                string hour = dt2.ToString("HH:mm");
                string datestr = year + "-" + hour;
                return datestr;
            }
            catch
            {
                return "";
            }
        }
        public static string ImgDate(DateTime dt)
        {
            try
            {
                return dt.ToString("yyyyMMddHHmmss");
            }
            catch
            {
                return "";
            }
        }
        public static string ToYYYYMMDD(DateTime dt)
        {
            try
            {

                return dt.ToString("yyyy-MM-dd");
            }
            catch
            {
                return "";
            }
        }

        public static string ToYYYYMMDDObj(object dt)
        {
            try
            {

                return Convert.ToDateTime(dt).ToString("yyyy-MM-dd");
            }
            catch
            {
                return "";
            }
        }
        public static string ToHHMM(DateTime dt)
        {
            try
            {
                return dt.ToString("HH:mm");
            }
            catch
            {
                return "";
            }
        }
        public static string ToYYYY(DateTime dt)
        {
            try
            {
                return dt.ToString("yyyy");
            }
            catch
            {
                return "";
            }
        }
        public static Guid? ToGuid(object vals)
        {
            Guid? revalue = null;
            try
            {
                revalue = Guid.Parse(ToStr(vals));
            }
            catch
            {
                ;
            }
            return revalue;
        }
        public static double ToDouble(object vals, int mathround = 1, int retentiondecimalnum = 2)
        {
            double revalue = 0;
            try
            {
                revalue = !Convert.IsDBNull(vals) ? Convert.ToDouble(vals) : 0;
                revalue = Math.Round(revalue / mathround, retentiondecimalnum);
                if (revalue == 0)
                {
                    revalue = 0;
                }
            }
            catch
            {
                ;
            }
            return revalue;
        }
        public static decimal ToDecimal(object vals, int mathround = 1, int retentiondecimalnum = 2)
        {
            decimal revalue = 0;
            try
            {
                revalue = !Convert.IsDBNull(vals) ? Convert.ToDecimal(vals) : 0;
                revalue = Math.Round(revalue / mathround, retentiondecimalnum);
                if (revalue == 0)
                {
                    revalue = 0;
                }
            }
            catch
            {
                ;
            }
            return revalue;
        }

        public static bool ToBool(object obj)
        {
            try
            {
                return Convert.ToBoolean(obj);
            }
            catch
            {
                return false;
            }
        }

        public static int ToInt(object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return 0;
            }
        }
        public static string ToStr(object obj)
        {
            try
            {
                return obj.ToString();
            }
            catch
            {
                return "";
            }
        }

        ///   <summary>   
        ///    去除HTML标记   
        ///   </summary>   
        ///   <param    name="NoHTML">包括HTML的源码   </param>   
        ///   <returns>已经去除后的文字</returns>   
        public static string NoHTML(string Htmlstring)
        {
            if (!string.IsNullOrEmpty(Htmlstring))
            {
                //删除脚本   
                Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
                //删除HTML   
                Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

                Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

                Htmlstring.Replace("<", "");
                Htmlstring.Replace(">", "");
                Htmlstring.Replace("\r\n", "");
                Htmlstring = HttpUtility.HtmlEncode(Htmlstring).Trim();
                

            }
            return Htmlstring;
        }
        #region    移除HTML标签
        ///   <summary>   
        ///    移除HTML标签   
        ///   </summary>   
        ///   <param    name="HTMLStr">HTMLStr</param>   
        public static string ParseTags(string HTMLStr)
        {
            return System.Text.RegularExpressions.Regex.Replace(HTMLStr, "<[^>]*>", "");
        }

        #endregion

        //IP转int
        public static long chIP2Int(string ipaddress)
        {
            try
            {
                string[] startIP = ipaddress.Split('.');
                long U = Convert.ToInt64(startIP[0]) << 24;
                U += Convert.ToInt64(startIP[1]) << 16;
                U += Convert.ToInt64(startIP[2]) << 8;
                U += Convert.ToInt64(startIP[3]);
                return U;
            }
            catch
            {
                return 0;
            }
        }
        //int转IP
        public static string chInt2IP(long ipaddress)
        {
            try
            {
                if (ipaddress == 0)
                {
                    return "";
                }
                else
                {
                    long ui1 = ipaddress & 0xFF000000;
                    ui1 = ui1 >> 24;
                    long ui2 = ipaddress & 0x00FF0000;
                    ui2 = ui2 >> 16;
                    long ui3 = ipaddress & 0x0000FF00;
                    ui3 = ui3 >> 8;
                    long ui4 = ipaddress & 0x000000FF;
                    string IPstr = "";
                    IPstr = ui1.ToString() + "." + ui2.ToString() + "." + ui3.ToString() + "." + ui4.ToString();
                    return IPstr;
                }
            }
            catch
            {
                return "";
            }
        }
        //完整IP地址
        public static string completeIP(string ip, int k = 1)
        {
            int addno = 0;
            if (k == 2) { addno = 255; }
            string temp = "";
            if (ip == "")
            {
                temp = addno.ToString() + "." + addno.ToString() + "." + addno.ToString() + "." + addno.ToString();
            }
            else
            {
                string[] ips = ip.Split('.');
                int dots = ips.Length;
                for (int i = 0; i < 4; i++)
                {
                    if ((i < dots) && (ips[i] != ""))
                    {
                        temp += "." + ips[i];
                    }
                    else
                    {
                        temp += "." + addno.ToString();
                    }
                }
                temp = temp.Substring(1);
            }

            return temp;

        }

        public static string NoHTMLSubStr(string str, int lenmin, int lenmax, bool showellipsis = true)
        {
            string restr = "";
            string objstr = NoHTML(str);
            if (!string.IsNullOrEmpty(objstr))
            {
                if (objstr.Length > lenmin && objstr.Length > (lenmax + lenmin))
                {
                    restr = objstr.Substring(lenmin, lenmax);
                    if (showellipsis && objstr.Length > (lenmax - lenmin))
                    {
                        restr += "...";
                    }
                }
                else
                {
                    restr = objstr;
                }
            }
            return restr;
        }
        public static string StrSubStr(string str, int lenmin, int lenmax, bool showellipsis = true)
        {
            string restr = "";
            string objstr = str;
            if (!string.IsNullOrEmpty(objstr))
            {
                if (objstr.Length > lenmin && objstr.Length > (lenmax + lenmin))
                {
                    restr = objstr.Substring(lenmin, lenmax);
                    if (showellipsis && objstr.Length > (lenmax - lenmin))
                    {
                        restr += "...";
                    }
                }
                else
                {
                    restr = objstr;
                }
            }
            return restr;
        }
        public static string IdCardConversion(string idcard)
        {
            try
            {

                string front3 = idcard.Substring(0, 3);

                string after3 = idcard.Substring(idcard.Length - 3);
                string str = front3 + "************" + after3;
                return str;
            }
            catch (Exception)
            {

                return "";
            }
        }
        public static string PasswordConversion(string pwd)
        {
            try
            {
                int len = pwd.Length;
                string str = "";
                for (int i = 0; i < len; i++)
                {
                    str += "x";
                }
                return str;
            }
            catch (Exception)
            {

                return "";
            }

        }/// 过滤SQL字符。
         /// </summary>
         /// <param name="str">要过滤SQL字符的字符串。</param>
         /// <returns>已过滤掉SQL字符的字符串。</returns>
        public static string FilterSql(string str)
        {
            if (str == String.Empty)
                return String.Empty;

            str = str.Replace("'", "‘");
            str = str.Replace(";", "；");
            str = str.Replace("?", "?");
            str = str.Replace("<", "＜");
            str = str.Replace(">", "＞");
            str = str.Replace("(", "(");
            str = str.Replace(")", ")");
            str = str.Replace("@", "＠");
            str = str.Replace("=", "＝");
            str = str.Replace("+", "＋");
            str = str.Replace("*", "＊");
            str = str.Replace("&", "＆");
            str = str.Replace("#", "＃");
            str = str.Replace("%", "％");
            str = str.Replace("$", "￥");

            return str;
        }
        public static string Time2Now(DateTime? Dtime)
        {
            string returnvalue = "";
            if (Dtime == null)
            {
                return returnvalue;
            }
            DateTime tdate = Convert.ToDateTime(Dtime);

            TimeSpan ts = DateTime.Now - tdate;
            if (ts.Days > 0)
            {
                returnvalue += ts.Days + "天前";
                return returnvalue;
            }
            if (ts.Hours > 0)
            {
                returnvalue += ts.Hours + "小时前";
                return returnvalue;
            }
            if (ts.Minutes > 0)
            {
                returnvalue += ts.Minutes + "分钟前";
                return returnvalue;
            }

            return returnvalue;

        }

        /// <summary>
        /// convert from byte to object
        /// </summary>
        /// <param name="Bytes"></param>
        /// <returns></returns>
        public static object BytesToObject(byte[] Bytes)
        {
            using (MemoryStream ms = new MemoryStream(Bytes))
            {
                IFormatter formatter = new BinaryFormatter(); return formatter.Deserialize(ms);
            }
        }

        /// <summary>
        /// convert from object to bytes
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ObjectToBytes(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter(); formatter.Serialize(ms, obj); return ms.GetBuffer();
            }
        }

        /// <summary>
        /// 得到本周第一天(以星期一为第一天)
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime GetWeekFirstDayMon(DateTime datetime)
        {
            //星期一为第一天
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //本周第一天
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }



    }
}
