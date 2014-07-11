//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：SqlProvider 数据库提供类
// 作    者：王义波
// 创建时间：2014/5/31 13:28:26
// CLR 版本：1.4
//=====================================================

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace CLR.DataBase
{
    /// <summary>
    /// SqlProvider 数据库提供类
    /// </summary>
    public sealed class SqlProvider : DBParameterProvider<SqlDbType>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString">string Sql Server 数据库连接字符串</param>
        public SqlProvider(string connectionString)
            : base(connectionString)
        {
        }

        /// <summary>
        /// 如果给定包含连接字符串的字符串，则初始化 System.Data.SqlClient.SqlConnection 类的新实例。
        /// </summary>
        /// <param name="connectionString">用于打开 SQL Server 数据库的连接</param>
        /// <returns></returns>
        protected override DbConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// 初始化具有查询文本和 System.Data.Common.DbConnection 的 System.Data.Common.DbCommand 类的新实例。
        /// </summary>
        /// <param name="cmdText">查询的文本。</param>
        /// <returns>System.Data.Common.DbCommand 类的新实例。</returns>
        protected override DbCommand GetCommand(string cmdText)
        {
            return new SqlCommand(cmdText);
        }
        /// <summary>
        /// 获取数据适配器信息
        /// </summary>
        /// <param name="selectCommandText"></param>
        /// <param name="selectConnectionString"></param>
        /// <returns></returns>
        protected override DbDataAdapter GetDataAdapter(string selectCommandText, string selectConnectionString)
        {
            return new SqlDataAdapter(selectCommandText, selectConnectionString);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        protected override DbParameter GetParameter(string parameterName, SqlDbType dbType)
        {
            return new SqlParameter(parameterName, dbType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        protected override DbParameter GetParameter(string parameterName, SqlDbType dbType, int size)
        {
            return new SqlParameter(parameterName, dbType, size);
        }
    }
}
