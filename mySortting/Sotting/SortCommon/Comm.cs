using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SortCommon
{
    public class Comm
    {
        public delegate void EventHandle(byte[] readBuffer);
        public event EventHandle DateReceived;

        public SerialPort _serialPort;
        Thread _thread;
        volatile bool _keepReading;
        public Comm()
        {
            _serialPort = new SerialPort();
            _thread = null;
            _keepReading = false;

            //serialPort.PortName = "单片机串口";
            //_serialPort.BaudRate = 115200;
            _serialPort.Parity = 0;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = (StopBits)1;
            _serialPort.ReadTimeout = 50;
            _serialPort.WriteTimeout = 50;
        }

        public bool IsOpen
        {
            get
            {
                return _serialPort.IsOpen;
            }
        }

        private void StartReading()
        {
            if (!_keepReading)
            {
                _keepReading = true;
                _thread = new Thread(new ThreadStart(ReadPort));
                _thread.Start();
            }
        }

        private void StopReading()
        {
            if (_keepReading)
            {
                _keepReading = false;
                _thread.Join();
                _thread = null;
            }
        }
        private void ReadPort()
        {
            while (_keepReading)
            {
                if (_serialPort.IsOpen)
                {
                    int count = _serialPort.BytesToRead;
                    if (count > 0)
                    {
                        byte[] readBuffer = new byte[count];
                        try
                        {
                            //Application.DoEvents();
                            _serialPort.Read(readBuffer, 0, count);
                            DateReceived?.Invoke(readBuffer);//这句是收到数据后的方法调用，委托自己写
                        }
                        catch (TimeoutException)
                        {
                        }
                    }
                }
            }
        }

        public void Open()
        {
            Close();
            try
            {
                _serialPort.Open();
                if (_serialPort.IsOpen)
                {
                    StartReading();
                }
                else
                {
                    //MessageBox.Show("串口打开失败!");
                }
            }
            catch
            {
                //MessageBox.Show("串口不存在，请更换串口");
            }
        }

        public void Close()
        {
            StopReading();
            _serialPort.Close();
        }

        public void WritePort(byte[] send, int offSet, int count)
        {
            if (IsOpen)
            {
                _serialPort.Write(send, offSet, count);
            }
        }
    }
}
