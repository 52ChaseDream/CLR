//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：数据库提供公共类
// 作    者：王义波
// 创建时间：2014/6/13 12:11:33
// CLR 版本：1.3
//=====================================================

using System.Data.Common;
using System.Data;
using System;

namespace CLR.DataBase
{
    /// <summary>
    /// 数据库提供公共类
    /// </summary>
    public abstract class DBUtilityProvider : IDisposable
    {
        private DbConnection _conn;
        /// <summary>
        /// 获取或设置数据库连接
        /// </summary>
        public DbConnection Conn
        {
            get { return this._conn; }
            set { this._conn = value; }
        }
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        protected DBUtilityProvider() { }
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        /// <param name="connectionString">用于打开数据库的连接</param>
        protected DBUtilityProvider(string connectionString)
        {
            this._conn = GetConnection(connectionString);
        }
        /// <summary>
        /// 获取连接字符串的新实例。
        /// </summary>
        /// <param name="connectionString">用于打开数据库的连接。</param>
        /// <returns>数据库的连接。</returns>
        protected abstract DbConnection GetConnection(string connectionString);
        /// <summary>
        /// 构造 System.Data.Common.DbCommand 对象的实例。
        /// </summary>
        /// <param name="cmdText">针对数据源运行的文本命令。</param>
        /// <returns>System.Data.Common.DbCommand 对象的实例。</returns>
        protected abstract DbCommand GetCommand(string cmdText);

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        protected void Open()
        {
            if ((this._conn != null) && (this._conn.State == ConnectionState.Closed))
            {
                this._conn.Open();
                System.Console.WriteLine("成功！");
            }
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            if (this._conn != null)
            {
                this._conn.Close();
            }
        }

        /// <summary>
        /// 返回某一列的所有字段名
        /// </summary>
        /// <param name="tableName">string 表名</param>
        /// <returns></returns>
        public string[] GetTableColumn(string tableName)
        {
            this._conn.Open();
            string[] _restrictionValues = new string[3];
            _restrictionValues[2] = tableName;
            DataTable _dataTable = this._conn.GetSchema("Columns", _restrictionValues);
            string[] strArray = new string[_dataTable.Rows.Count];
            for (int i = 0; i < _dataTable.Rows.Count; i++)
            {
                strArray[i] = (string)_dataTable.Rows[i]["COLUMN_NAME"];
            }
            this._conn.Close();
            return strArray;
        }

        #region IDisposable 成员
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (this._conn != null)
            {
                this._conn.Dispose();
                this._conn = null;
            }
        }

        #endregion
    }
}
