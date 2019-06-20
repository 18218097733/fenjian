using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SortCommon;

namespace UI.IUI
{
    public partial class FormSetting : Form
    {
        #region variable
        IniHelper iniHelper = new IniHelper("ParaSeting.ini");
        Comm serialComm1 = new Comm();
        #endregion
        public FormSetting()
        {
            InitializeComponent();
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {
            string[] portName;
            int i;
            //serialComm1.DateReceived += new Comm.EventHandle(OnDataReceived);
            //post请求地址
            IniHelper.GetAllKeyValues("Http_Setting", out string[] keys, out string[] values, iniHelper.FileName);
            if(values.Length>0)
            {
                tbURL.Text = values[0];
            }
            //串口初始化设置
            cBCom.Items.Clear();
            cBWeightComm.Items.Clear();
            IniHelper.GetAllKeyValues("SerialPort_Setting", out string[] keys2, out string[] values2, iniHelper.FileName);
            IniHelper.GetAllKeyValues("WeightPort_Setting", out string[] keys3, out string[] values3, iniHelper.FileName);
            portName = System.IO.Ports.SerialPort.GetPortNames();
            if (portName.Length > 0)
            {
                for (i = 0; i < portName.Length; i++)
                {
                    cBCom.Items.Add(portName[i]);
                    cBWeightComm.Items.Add(portName[i]);
                    if (values2.Length > 1)
                    {
                        if (values2[0] == portName[i])
                        {
                            cBCom.Text = values2[0];
                            cBBaud.Text = values2[1];
                            serialComm1.Close();
                            serialComm1._serialPort.PortName = values2[0];
                            serialComm1._serialPort.BaudRate = Convert.ToInt32(values2[1]);
                            serialComm1.Open();
                        }
                    }
                    if (values3.Length > 1)
                    {
                        if (values3[0] == portName[i])
                        {
                            cBWeightComm.Text = values3[0];
                            cBWeightBaund.Text = values3[1];
                            cbWorkSole.Text = values3[2];
                        }
                    }
                }
            }
        }


        private void btHttp_Click(object sender, EventArgs e)
        {
            IniHelper.Write("Http_Setting", "请求地址", tbURL.Text, iniHelper.FileName);
            MessageBox.Show("保存成功!!");
        }

        private void btRun_Click(object sender, EventArgs e)
        {
            IniHelper.Write("SerialPort_Setting", "串口", cBCom.Text, iniHelper.FileName);
            IniHelper.Write("SerialPort_Setting", "波特率", cBBaud.Text, iniHelper.FileName);
            MessageBox.Show("保存成功!!");
        }



        private void btWeight_Click(object sender, EventArgs e)
        {
            IniHelper.Write("WeightPort_Setting", "串口", cBWeightComm.Text, iniHelper.FileName);
            IniHelper.Write("WeightPort_Setting", "波特率", cBWeightBaund.Text, iniHelper.FileName);
            IniHelper.Write("WeightPort_Setting", "称台号", cbWorkSole.Text, iniHelper.FileName);
            MessageBox.Show("保存成功!!");
        }

        private void FormSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                serialComm1.Close();
            }
            catch
            { }
        }


        private void buttonOther_Click(object sender, EventArgs e)
        {
            if(checkBoxSaveCode.Checked)
            {
                IniHelper.Write("Other_Setting", "SaveCode", "true", iniHelper.FileName);
            }
            else
            {
                IniHelper.Write("Other_Setting", "SaveCode", "false", iniHelper.FileName);
            }
            MessageBox.Show("保存成功!!");
        }
    }
}
