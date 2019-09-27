using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Tools
{
    public static class BaiduMapHelper
    {

        #region 常量
        //百度地图Api  Ak
        public static string BaiduAk = ConfigHelper.GetSectionValue("BaiduAk");

        /// <summary>
        /// 经纬度  逆地理编码 Url  需要Format 0.ak  1.经度  2.纬度
        /// </summary>
        private const string BaiduGeoCoding_ApiUrl = "http://api.map.baidu.com/geocoder/v2/?ak={0}&location={1},{2}&output=json&pois=0";

        /// <summary>
        /// 0.地址字符串 1.ak
        /// </summary>
        private const string BaiduGeoCoding_ApiCoord = "http://api.map.baidu.com/geocoder/v2/?ak={0}&address={1}&output=json";
        #endregion

        #region 地址转换器
        /// <summary>
        /// 根据地址信息 获取 经纬度
        /// </summary>
        /// <param name="addressStr">地址字符串</param>
        /// <returns></returns>
        public static BaiDuGeoCoding AddressToCoordinate(string addressStr)
        {
            string url = string.Format(Baidu_GeoCoding_ApiCoord, addressStr);
            var model = CommonMethod.GetResponse<BaiDuGeoCoding>(url);
            return model;
        }

        /// <summary>
        /// 根据经纬度  获取 地址信息
        /// </summary>
        /// <param name="lat">经度</param>
        /// <param name="lng">纬度</param>
        /// <returns></returns>
        public static BaiDuGeoCoding CoordinateToAddress(object lat, string lng)
        {
            string url = string.Format(Baidu_GeoCoding_ApiUrl, lat, lng);
            var model = CommonMethod.GetResponse<BaiDuGeoCoding>(url);
            return model;
        }
        #endregion

        #region 辅助格式化
        /// <summary>
        /// /// <summary>
        /// 经纬度  逆地理编码 Url  需要Format 0.经度  1.纬度 
        /// </summary>
        /// </summary>
        public static string Baidu_GeoCoding_ApiUrl
        {
            get
            {
                return string.Format(BaiduGeoCoding_ApiUrl, BaiduAk, "{0}", "{1}");
            }
        }

        /// <summary>
        /// 地址字符串 
        /// </summary>
        public static string Baidu_GeoCoding_ApiCoord
        {
            get
            {
                return string.Format(BaiduGeoCoding_ApiCoord, BaiduAk, "{0}");
            }
        }


        #endregion

        #region model
        public class BaiDuGeoCoding
        {
            public int Status { get; set; }
            public Result Result { get; set; }
        }

        public class Result
        {
            public Location Location { get; set; }

            public string Formatted_Address { get; set; }

            public string Business { get; set; }

            public AddressComponent AddressComponent { get; set; }

            public string CityCode { get; set; }
        }

        public class AddressComponent
        {
            /// <summary>
            /// 省份
            /// </summary>
            public string Province { get; set; }
            /// <summary>
            /// 城市名
            /// </summary>
            public string City { get; set; }

            /// <summary>
            /// 区县名
            /// </summary>
            public string District { get; set; }

            /// <summary>
            /// 街道名
            /// </summary>
            public string Street { get; set; }

            public string Street_number { get; set; }

        }

        public class Location
        {
            public string Lng { get; set; }
            public string Lat { get; set; }
        }
        #endregion
    }
}
