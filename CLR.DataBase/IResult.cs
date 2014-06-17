//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：结果信息类型
// 作    者：王义波
// 创建时间：2014/5/26 12:40:56
// CLR 版本：1.3
//=====================================================


namespace CLR.DataBase
{
    /// <summary>
    /// 结果信息类型
    /// </summary>
    public class IResult
    {
        private string _Message;
        private int _Result;
        /// <summary>
        /// 构造函数
        /// </summary>
        public IResult() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Result">int 结果代码</param>
        /// <param name="Message">string 结果信息</param>
        public IResult(int Result, string Message)
        {
            this._Result = Result;
            this._Message = Message;
        }
        /// <summary>
        /// 获取或设置结果信息
        /// </summary>
        public string Message { get { return this._Message; } set { this._Message = value; } }
        /// <summary>
        /// 获取或设置结果代码
        /// </summary>
        public int Result { get { return this._Result; } set { this._Result = value; } }
    }
}
