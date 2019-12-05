using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using XLCCoin.Domain.Entities;

namespace XLCCoin.Application.NodeCommands.Queries
{
    public class NodeVM
    {
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public bool IsBehindNAT { get; set; }
        public string Geolocation { get; set; }


        public bool IsConnected { get; set; }



        public TcpClient Client { get; set; }

        public void SendMessage(string message)
        {
            Queue<string> _dataToSend = new Queue<string>();

            int _maxChar = 10;
            var _stream = Client.GetStream();

            for (int i = 0; i <= message.Length; i += _maxChar)
            {
                int _len = _maxChar;
                if (message.Length - i < _maxChar)
                {
                    _len = message.Length - i;
                }

                string _msg = message.Substring(i, _len);

                byte[] _bytesToSend = Encoding.ASCII.GetBytes(_msg);

                _stream.Write(_bytesToSend, 0, _bytesToSend.Length);
            }
        }

        public void SendCommand(string command)
        {

        }

        public void StartMessageReceive(Action<string> messageReceived)
        {
            byte[] _data = new byte[10];
            int i;

            StringBuilder _sb = new StringBuilder();
            string _finalMessage = null;
            var _myStream = Client.GetStream();

            new Thread(() =>
            {
                while ((i = _myStream.Read(_data, 0, _data.Length)) != 0)
                {

                    string _message = Encoding.ASCII.GetString(_data.Take(i).ToArray());

                    _sb.Append(_message);

                    if (_message.EndsWith("~"))
                    {
                        _finalMessage = _sb.ToString().TrimEnd('~');

                        if (messageReceived != null)
                        {
                            messageReceived(_finalMessage);
                        }

                        _sb = new StringBuilder();
                        _finalMessage = null;
                    }
                }
            }).Start();
        }
    }
}

