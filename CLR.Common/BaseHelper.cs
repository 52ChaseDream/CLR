//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：通用方法、函数集
// 作    者：王义波
// 创建时间：2014/5/25 10:30:50
// CLR 版本：1.1
//=====================================================

using System;
using System.Text;
using System.Text.RegularExpressions;

namespace CLR.Common
{
    /// <summary>
    /// 通用方法、函数集
    /// </summary>
    public class BaseHelper
    {

        /// <summary>
        /// 获取字符串的长度(汉字占两个字节)
        /// </summary>
        /// <param name="str">string 字符串</param>
        /// <returns>int 字符串的长度</returns>
        public int Length(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        /// <summary>
        /// 转换指定的字符串为字符串数组
        /// </summary>
        /// <param name="str">string 字符串</param>
        /// <returns>string[] 数组</returns>
        public string[] GetStrings(string str)
        {
            return ToString(str).Replace("，", ",").Replace(" ", ",").Split(new char[] { Convert.ToChar(",") });
        }

        /// <summary>
        /// 判断对象是否为布尔值
        /// </summary>
        /// <param name="values">object 对象</param>
        /// <returns>bool 是为true，否则false</returns>
        public bool IsBool(object values)
        {
            return (Array.IndexOf<string>(new string[] { "true", "false", "yes", "no", "1", "0" }, ToString(values).ToLower()) >= 0);
        }

        /// <summary>
        /// 判断字符串是否为电子信箱格式
        /// </summary>
        /// <param name="values">string 要判断的字符串</param>
        /// <returns>bool 是为true 否则false</returns>
        public bool IsEmail(string values)
        {
            return Regex.IsMatch(values, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }


        /// <summary>
        /// 判断字符是否仅为英文字符
        /// </summary>
        /// <param name="values">object 要判断的对象</param>
        /// <returns>bool 是为true,否则false</returns>
        public bool IsEnglish(object values)
        {
            return !Regex.IsMatch(IsNull(values) ? "" : values.ToString(), @"[\u4e00-\u9fa5]");
        }

        /// <summary>
        /// 判断对象是否为浮点型数值
        /// </summary>
        /// <param name="values">objetc 对象</param>
        /// <returns>bool 是为true,否则false</returns>
        public bool IsFloat(object values)
        {
            return Regex.IsMatch(IsNull(values) ? "" : values.ToString(), "^(-?[0-9]*[.]*[0-9]*)$");
        }

        /// <summary>
        /// 判断对象是否为整型数值
        /// </summary>
        /// <param name="values">object 对象</param>
        /// <returns>bool是为true,否则false</returns>
        public bool IsInt(object values)
        {
            return Regex.IsMatch(IsNull(values) ? "" : values.ToString(), "^[0-9]*$");
        }

        /// <summary>
        /// 判断字符串是否为IP地址格式
        /// </summary>
        /// <param name="values">string 要判断的字符串</param>
        /// <returns>bool 是为true，否则false</returns>
        public bool IsIP(string values)
        {
            if (Regex.IsMatch(values, @"[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"))
            {
                string[] strArray = values.Split(new char[] { '.' });
                if (((strArray.Length == 4) || (strArray.Length == 6)) && ((int.Parse(strArray[0]) < 0x100) && (((int.Parse(strArray[1]) < 0x100) & (int.Parse(strArray[2]) < 0x100)) & (int.Parse(strArray[3]) < 0x100))))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 判断对象是否为空
        /// </summary>
        /// <param name="values">object 对象</param>
        /// <returns>bool 空为true，否则false</returns>
        public bool IsNull(object values)
        {
            return ((((values == null) || (values == DBNull.Value)) || (values.ToString() == string.Empty)) || (values.ToString().Trim() == ""));
        }

        /// <summary>
        /// 判断字符串是否为身份证号格式
        /// </summary>
        /// <param name="values">string 要判断的字符串</param>
        /// <returns>bool 是为true，否则false</returns>
        public bool IsTheId(string values)
        {
            return Regex.IsMatch(values, @"^\d{6}(19|20)\d{2}((1[0-2])|0\d)([0-2]\d|30|31)\d{3}[\d|X|x]$");
        }

        /// <summary>
        /// 判断字符串是否为日期时间格式
        /// </summary>
        /// <param name="values">string 要判断的字符串</param>
        /// <returns>bool 是为true，否则false</returns>
        public bool IsDateTime(string values)
        {
            try
            {
                DateTime.Parse(values);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断字符串是否为Url地址格式
        /// </summary>
        /// <param name="values">string 要判断的字符串</param>
        /// <returns>bool 是为true，否则false</returns>
        public bool IsUrl(string values)
        {
            return Regex.IsMatch(values, @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 截取指定分隔符后的字符串
        /// </summary>
        /// <param name="str">string 字符串</param>
        /// <param name="chars">string 分隔字符</param>
        /// <returns>string 截取后的字符串</returns>
        public string LastSubString(string str, string chars)
        {
            return (str.LastIndexOf(chars) >= 0) && ((str.LastIndexOf(chars) + chars.Length) < str.Length) ? str.Substring(str.LastIndexOf(chars) + chars.Length) : string.Empty;
        }

        /// <summary>
        /// 过滤字符串中的指定字符
        /// </summary>
        /// <param name="str">string 字符串</param>
        /// <param name="chars">string 要过滤的字符串</param>
        /// <returns>string 过滤后的字符串</returns>
        public string Filter(string str, string chars)
        {
            return Regex.Replace(str, "[" + chars + "]", "");
        }

        /// <summary>
        /// 过滤T-SQL参数中的不安全字符
        /// </summary>
        /// <param name="String">string T-SQL参数</param>
        /// <returns>string 过滤后的参数</returns>
        public string SqlFilter(string String)
        {
            return Regex.Replace(String, "exec insert |select |delete |'|update |chr\\(|master\\(|mid\\(|truncate\\(|char\\(|declare\\(| or | and \t|;and | backup | nchar |drop table |delete from |net user | net \tlocalgroup |xp_cmdshell|<script>|</script>", "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 截取指定长度的字符串
        /// </summary>
        /// <param name="str">string 字符串</param>
        /// <param name="length">long 长度</param>
        /// <returns>string 截取后的字符串</returns>
        public string SubString(string str, int length)
        {
            int _num = 0, _length = 0;
            byte[] bytes = Encoding.Default.GetBytes(str);
            for (int i = 0; i < length; i++)
            {
                if (bytes[i] > 0x7f)
                    _num++;
                _length++;
            }
            if (!(Math.IEEERemainder((double)_num, 2.0) == 0.0))
                _length--;
            byte[] destinationArray = new byte[_length];
            Array.Copy(bytes, destinationArray, _length);
            return Encoding.Default.GetString(destinationArray);
        }

        /// <summary>
        /// 转换纯文本内容为HTML内容
        /// </summary>
        /// <param name="Text">string 纯文本内容</param>
        /// <returns>string 转换后的HTML内容</returns>
        public string ToHTML(string Text)
        {
            return Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace("<p>" + Text + "</p>", "\r\n", "</p><p>"), "\r", "</p><p>"), "\n", "<br />"), "\t", "&nbsp; &nbsp; "), "  ", "&nbsp; ");
        }

        /// <summary>
        /// 转换长整型为文件尺寸文本
        /// </summary>
        /// <param name="length">long 长整型竖尺寸</param>
        /// <returns>string 转换后的尺寸文本</returns>
        public string ToSize(long length)
        {
            if (length < 0x3e8L)
                return (length + " B");
            long num = 0x400L, num2 = 0xfa000L;
            if (length < num2)
                return (Math.Round((double)(((double)length) / ((double)num)), 2) + " KB");
            num *= 0x400L;
            num2 *= 0x400L;
            if (length < num2)
                return (Math.Round((double)(((double)length) / ((double)num)), 2) + " MB");
            num *= 0x400L;
            num2 *= 0x400L;
            if (length < num2)
                return (Math.Round((double)(((double)length) / ((double)num)), 2) + " GB");
            num *= 0x400L;
            return (Math.Round((double)(((double)length) / ((double)num)), 2) + " TB");
        }

        /// <summary>
        /// 转换对象为字符串
        /// </summary>
        /// <param name="values">object 对象</param>
        /// <returns>string 转换后的字符串</returns>
        public string ToString(object values)
        {
            return IsNull(values) ? string.Empty : values.ToString();
        }

        /// <summary>
        /// 转换HTML内容为纯文本内容
        /// </summary>
        /// <param name="html">string HTML内容</param>
        /// <returns>string 转换后的纯文本内容</returns>
        public string ToText(string html)
        {
            return Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(html, @"(?m)<script[^>]*>(\w|\W)*?</script[^>]*>", "", RegexOptions.Multiline | RegexOptions.IgnoreCase), @"(?m)<style[^>]*>(\w|\W)*?</style[^>]*>", "", RegexOptions.Multiline | RegexOptions.IgnoreCase), @"(?m)<select[^>]*>(\w|\W)*?</select[^>]*>", "", RegexOptions.Multiline | RegexOptions.IgnoreCase), @"(?m)<a[^>]*>(\w|\W)*?</a[^>]*>", "", RegexOptions.Multiline | RegexOptions.IgnoreCase), "(<[^>]+?>)|&nbsp;", "", RegexOptions.Multiline | RegexOptions.IgnoreCase), @"(\s)+", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        }

    }
}
