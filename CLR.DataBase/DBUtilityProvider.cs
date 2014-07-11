//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：数据库提供公共类
// 作    者：王义波
// 创建时间：2014/6/13 12:11:33
// CLR 版本：1.4
//=====================================================

using System.Data.Common;
using System.Data;
using System;
using System.Threading.Tasks;

namespace CLR.DataBase
{
    /// <summary>
    /// 数据库提供公共类
    /// </summary>
    public abstract class DBUtilityProvider : DBUtility, IDisposable
    {
        private string _connectionString;

        private DbConnection _conn;

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
            this._connectionString = connectionString;
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
        /// 获取数据适配器信息
        /// </summary>
        /// <param name="selectCommandText"></param>
        /// <param name="selectConnectionString"></param>
        /// <returns></returns>
        protected abstract DbDataAdapter GetDataAdapter(string selectCommandText, string selectConnectionString);

        private DbDataAdapter GetDataAdapter(string name, CommandType type, params DbParameter[] parameters)
        {
            this.Open();
            var _dataAdapter = this.GetDataAdapter(name, _connectionString);
            try
            {
                _dataAdapter.SelectCommand.CommandType = type;
                if (parameters != null)
                {
                    Parallel.ForEach(parameters, item => {
                        _dataAdapter.SelectCommand.Parameters.Add(item);
                    });
                }
            }
            catch (Exception ex)
            {
                _dataAdapter.Dispose();
                throw new Exception(ex.Message);
            }
            return _dataAdapter;
        }

        private DbCommand GetCommand(string name, CommandType type, params DbParameter[] parameters)
        {
            this.Open();
            var _command = this.GetCommand(name);
            _command.Connection = _conn;
            try
            {
                _command.CommandType = type;
                if (parameters != null)
                {
                    Parallel.ForEach(parameters, item =>
                    {
                        _command.Parameters.Add(item);
                    });
                }
            }
            catch (Exception exception)
            {
                _command.Dispose();
                throw new Exception(exception.Message);
            }
            return _command;
        }

        /// <summary>
        /// 执行数据集
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="tbName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected override DataSet ExecDataSet(string name, CommandType type, string tbName, params DbParameter[] parameters)
        {
            var _dataSet = new DataSet();
            try
            {
                var _dataAdapter = this.GetDataAdapter(name, type, parameters);
                _dataAdapter.Fill(tbName == null ? _dataSet : _dataSet, tbName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return _dataSet;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected override int Exec(string name, CommandType type, params DbParameter[] parameters)
        {
            var _command = this.GetCommand(name, type, parameters);
            try
            {
                return Convert.ToInt32(_command.ExecuteNonQuery());
            }
            catch (Exception ex)
            {
                this.Close();
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        protected void Open()
        {
            if (this._conn == null)
            {
                this._conn = this.GetConnection(this._connectionString);
            }
            if ((this._conn != null) && (this._conn.State == ConnectionState.Closed))
            {
                this._conn.Open();
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
