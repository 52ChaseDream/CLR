//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：上传文件
// 作    者：王义波
// 创建时间：2014/7/30 10:29:33
// CLR 版本：1.1
//=====================================================
using System.Net.Sockets;
using System;

namespace CLR.UpDownLoad
{
    public class SocketUpLoad
    {
        public int SendData(Socket socket, byte[] data)
        {
            int _total = 0;
            int _size = data.Length;
            int _dataleft = _size;
            int _sent;
            while (_total < _size)
            {
                _sent = socket.Send(data, _total, _dataleft, SocketFlags.None);
                _total += _sent;
                _dataleft -= _sent;
            }
            return _total;
        }

        public byte[] ReceiveData(Socket socket, int size) 
        {
            int _total = 0;
            int _dataleft = size;
            byte[] _data = new byte[size];
            int _recv;
            while (_total < size)
            {
                _recv = socket.Receive(_data, _total, _dataleft, SocketFlags.None);
                if (_recv == 0)
                {
                    _data = null;
                    break;
                }

                _total += _recv;
                _dataleft -= _recv;
            }
            return _data; 
        }

        public int SendVarData(Socket socket, byte[] data) 
        {
            int _total = 0;
            int _size = data.Length;
            int _dataleft = _size;
            int _sent;
            byte[] _datasize = new byte[4];
            _datasize = BitConverter.GetBytes(_size);
            _sent = socket.Send(_datasize);

            while (_total < _size)
            {
                _sent = socket.Send(data, _total, _dataleft, SocketFlags.None);
                _total += _sent;
                _dataleft -= _sent;
            }

            return _total; 
        }

        public byte[] ReceiveVarData(Socket socket)
        {
            int _total = 0;
            int _recv;
            byte[] _datasize = new byte[4];
            _recv = socket.Receive(_datasize, 0, 4, SocketFlags.None);
            int _size = BitConverter.ToInt32(_datasize, 0);
            int _dataleft = _size;
            byte[] _data = new byte[_size];
            while (_total < _size)
            {
                _recv = socket.Receive(_data, _total, _dataleft, SocketFlags.None);
                if (_recv == 0)
                {
                    _data = null;
                    break;
                }
                _total += _recv;
                _dataleft -= _recv;
            }
            return _data;
        }  
    }
}
