//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：Base64 编码帮助类
// 作    者：王义波
// 创建时间：2014/6/3 16:26:30
// CLR 版本：1.3
//=====================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLR.Security
{
    /// <summary>
    /// Base64 编码帮助类
    /// </summary>
    public sealed class Base64Helper : SecurityUtility
    {
        #region 私有字段

        private string _codeTable = @"ABCDEFGHIJKLMNOPQRSTUVWXYZbacdefghijklmnopqrstu_wxyz0123456789*-";

        private string _pad = "v";

        private Dictionary<int, char> _mT1 = new Dictionary<int, char>();

        private Dictionary<char, int> _mT2 = new Dictionary<char, int>();

        #endregion

        #region 公共属性

        /// <summary>
        /// 设置或获取并验证密码表合法性
        /// </summary>
        public string CodeTable
        {
            get { return _codeTable; }
            set
            {
                if (value == null)
                {
                    throw new Exception("密码表不能为null");
                }
                if (value.Length < 64)
                {
                    throw new Exception("密码表长度必须至少为64");
                }
                this.ValidateRepeat(value);
                this.ValidateEqualPad(value, _pad);
                _codeTable = value;
                this.InitDict();
            }
        }

        /// <summary>
        /// 设置或获取并验证补码合法性
        /// </summary>
        public string Pad
        {
            get { return _pad; }
            set
            {
                if (value == null)
                {
                    throw new Exception("密码表的补码不能为null");
                }
                if (value.Length != 1)
                {
                    throw new Exception("密码表的补码长度必须为1");
                }
                this.ValidateEqualPad(_codeTable, value);
                _pad = value;
                this.InitDict();
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化字典
        /// </summary>
        public Base64Helper()
        {
            InitDict();
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 获取具有标准的Base64密码表的加密类
        /// </summary>
        /// <returns>Base64密码表的加密类</returns>
        public Base64Helper GetStandardBase64()
        {
            return new Base64Helper
            {
                Pad = "=",
                CodeTable = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"
            };
        }

        /// <summary>
        /// 使用默认的密码表（双向哈西字典）加密字符串
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public override string Encrypt(string input)
        {
            return this.Encode(input);
        }

        /// <summary>
        /// 使用默认的密码表（双向哈西字典）解密字符串
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public override string Decrypt(string input)
        {
            return this.Decode(input);
        }

        #endregion
        #region 私有方法
        /// <summary>
        /// 使用默认的密码表（双向哈西字典）加密字符串
        /// </summary>
        /// <param name="source">需要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        private string Encode(string source)
        {
            if (this.BaseHelperInstance.IsNull(source))
                return "";
            var _stringBuilder = new StringBuilder();
            byte[] _tmp = Encoding.UTF8.GetBytes(source);
            int _remain = _tmp.Length % 3;
            int _patch = 3 - _remain;
            if (_remain != 0)
            {
                Array.Resize(ref _tmp, _tmp.Length + _patch);
            }
            for (int i = 0; i < (int)Math.Ceiling(_tmp.Length * 1.0 / 3); i++)
            {
                _stringBuilder.Append(EncodeUnit(_tmp[i * 3], _tmp[i * 3 + 1], _tmp[i * 3 + 2]));
            }
            if (_remain != 0)
            {
                _stringBuilder.Remove(_stringBuilder.Length - _patch, _patch);
                for (int i = 0; i < _patch; i++)
                {
                    _stringBuilder.Append(_pad);
                }
            }
            return _stringBuilder.ToString();
        }

        private string EncodeUnit(params byte[] unit)
        {
            var _obj = new int[4];
            _obj[0] = (unit[0] & 0xfc) >> 2;
            _obj[1] = ((unit[0] & 0x03) << 4) + ((unit[1] & 0xf0) >> 4);
            _obj[2] = ((unit[1] & 0x0f) << 2) + ((unit[2] & 0xc0) >> 6);
            _obj[3] = unit[2] & 0x3f;
            var _stringBuilder = new StringBuilder();
            foreach (int t in _obj)
            {
                _stringBuilder.Append(_mT1[t]);
            }
            return _stringBuilder.ToString();
        }

        /// <summary>
        /// 使用默认的密码表（双向哈西字典）解密字符串
        /// </summary>
        /// <param name="source">需要加密的字符串</param>
        /// <returns>解密后的字符串</returns>
        private string Decode(string source)
        {
            if (this.BaseHelperInstance.IsNull(source))
                return "";
            var _list = new List<byte>();
            char[] _tmp = source.ToCharArray();
            int _remain = _tmp.Length % 4;
            if (_remain != 0)
                Array.Resize(ref _tmp, _tmp.Length - _remain);
            int _patch = source.IndexOf(_pad);
            if (_patch != -1)
                _patch = source.Length - _patch;
            for (int i = 0; i < _tmp.Length / 4; i++)
            {
                DecodeUnit(_list, _tmp[i * 4], _tmp[i * 4 + 1], _tmp[i * 4 + 2], _tmp[i * 4 + 3]);
            }
            for (int i = 0; i < _patch; i++)
            {
                _list.RemoveAt(_list.Count - 1);
            }
            return Encoding.UTF8.GetString(_list.ToArray());
        }

        private void DecodeUnit(List<byte> byteArr, params char[] chArray)
        {
            var _res = new int[3];
            var _unit = new byte[chArray.Length];
            for (int i = 0; i < chArray.Length; i++)
            {
                _unit[i] = (byte)_mT2[chArray[i]];
            }
            _res[0] = (_unit[0] << 2) + ((_unit[1] & 0x30) >> 4);
            _res[1] = ((_unit[1] & 0xf) << 4) + ((_unit[2] & 0x3c) >> 2);
            _res[2] = ((_unit[2] & 0x3) << 6) + _unit[3];
            byteArr.AddRange(_res.Select(t => (byte)t));
        }

        /// <summary>
        /// 初始化字典
        /// </summary>
        public void InitDict()
        {
            _mT1.Clear();
            _mT2.Clear();
            _mT2.Add(_pad[0], -1);
            for (int i = 0; i < _codeTable.Length; i++)
            {
                _mT1.Add(i, _codeTable[i]);
                _mT2.Add(_codeTable[i], i);
            }
        }

        private void ValidateRepeat(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input.LastIndexOf(input[i]) > i)
                    throw new Exception("密码表中含有重复字符：" + input[i]);
            }
        }

        private void ValidateEqualPad(string input, string pad)
        {
            if (input.IndexOf(pad) > -1)
                throw new Exception("密码表中包含了补码字符：" + pad);
        }
        #endregion
    }
}
