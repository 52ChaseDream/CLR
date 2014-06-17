//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：OleProvider 数据库提供类
// 作    者：王义波
// 创建时间：2014/6/13 11:39:23
// CLR 版本：1.3
//=====================================================

using System;
using System.Collections;
using System.Data.Common;
using System.Data.OleDb;
using CLR.Common;

namespace CLR.DataBase
{
    /// <summary>
    /// OleProvider 数据库提供类接口
    /// </summary>
    public sealed class OleProvider : DBUtilityProvider
    {
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        /// <param name="connectionString">string Oledb 数据库连接字符串</param>
        public OleProvider(string connectionString)
            : base(connectionString)
        {

        }

        /// <summary>
        /// 当前Http请求扩展类
        /// </summary>
        public CurrentHelper CurrentHelperInstance
        {
            get { return new CurrentHelper(); }
        }
        /// <summary>
        /// 使用指定的连接字符串初始化 System.Data.Common.DbConnection 类的新实例。
        /// </summary>
        /// <param name="connectionString">用于打开数据库的连接。</param>
        /// <returns>数据库的连接</returns>
        protected override DbConnection GetConnection(string connectionString)
        {
            return new OleDbConnection(connectionString);
        }

        /// <summary>
        /// 初始化具有查询文本和 System.Data.Common.DbConnection 的 System.Data.Common.DbCommand类的新实例。
        /// </summary>
        /// <param name="cmdText">查询的文本。</param>
        /// <returns>System.Data.Common.DbCommand类的新实例。</returns>
        protected override DbCommand GetCommand(string cmdText)
        {
            return new OleDbCommand(cmdText);
        }
    }
}
