//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：当前Http请求扩展类
// 作    者：王义波
// 创建时间：2014/5/26 16:18:45
// CLR 版本：1.4
//=====================================================

using System.Web;

namespace CLR.Common
{


    /// <summary>
    /// 当前Http请求扩展类
    /// </summary>
    public class CurrentHelper
    {
        private string _url = HttpContext.Current.Request.ServerVariables["URL"];
        private string _physicalPath = HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"];
        private string _applicationPath = HttpContext.Current.Request.ApplicationPath;
        private string _https = HttpContext.Current.Request.ServerVariables["HTTPS"];
        private string _serverPort = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
        private string _serverName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];

        /// <summary>
        /// 获取通用方法、函数集类接口
        /// </summary>
        public BaseHelper BaseHelperInstance
        {
            get
            {
                return new BaseHelper();
            }
        }

        /// <summary>
        /// 当前 WEB 应用的实际路径
        /// </summary>
        public string PhysicalPath
        {
            get
            {
                if (!_physicalPath.EndsWith(@"\"))
                    _physicalPath = _physicalPath + @"\";
                return _physicalPath;
            }
        }

        /// <summary>
        /// 当前 WEB 应用的虚拟目录
        /// </summary>
        public string Virtual
        {
            get
            {
                return (_applicationPath != null) && _applicationPath.EndsWith("/") ? _applicationPath : (_applicationPath + "/");
            }
        }

        /// <summary>
        /// 当前 HTTP 请求的客户端 IP 地址
        /// </summary>
        public string UserAddress
        {
            get
            {
                string _serverVariables = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                return (_serverVariables == null) || (_serverVariables == "") ? HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] : _serverVariables;
            }
        }

        /// <summary>
        /// 当前 HTTP 请求的完整 Url （包含参数信息）
        /// </summary>
        public string Url
        {
            get
            {
                return (_url + "?" + HttpContext.Current.Request.ServerVariables["QUERY_STRING"]);
            }
        }

        /// <summary>
        /// 当前 HTTP 请求的页面 Url
        /// </summary>
        public string Page
        {
            get
            {
                return _url;
            }
        }

        /// <summary>
        /// 当前请求 HTTP 头
        /// </summary>
        public string Http
        {
            get
            {
                if (_https.ToLower() == "on")
                {
                    return "https://";
                }
                return "http://";
            }
        }

        /// <summary>
        /// 当前 HTTP 请求主机头
        /// </summary>
        public string Domain
        {
            get
            {
                return ((_serverPort == null) || (_serverPort == "") || (_serverPort == "80")) ? _serverName : (_serverName + ":" + _serverPort);
            }
        }

        /// <summary>
        /// 当前 HTTP 请求的参数集合
        /// </summary>
        public HashHelper Querys
        {
            get
            {
                HashHelper _hashHelper = new HashHelper();
                foreach (string items in HttpContext.Current.Request.Form.AllKeys)
                {
                    _hashHelper.Add(this.BaseHelperInstance.ToString(items), HttpContext.Current.Request.Form[items]);
                }
                foreach (string items in HttpContext.Current.Request.QueryString.AllKeys)
                {
                    _hashHelper.Add(this.BaseHelperInstance.ToString(items), HttpContext.Current.Request.QueryString[items]);
                }
                return _hashHelper;
            }
        }
    }
}
