using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HalconDotNet;
using SortCommon;
using ThridLibray;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace UI.IUI
{
    public partial class FormMain : Form
    {

        private FormSetting myFormSetting;
        IniHelper iniHelper = new IniHelper("ParaSeting.ini");
        Comm serialComm = new Comm();
        Comm weightComm = new Comm();
        bool STM32Ready = true;
        private bool mLinkConnect = false;
        private bool mLinkConnect2 = false;
        bool IsSavaCode=false;
        int mTimeOut = 0,mTimOut2=0;
        int mPeriodCheck = 0;
        string weightStr;
        string url;
        private string workConsole;
        DataTable dt = new DataTable("Datas");
        string pattern = @"[-|;|:|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']";
        Dictionary<int, List<string>> dic = new Dictionary<int, List<string>>();

        public FormMain()
        {
            InitializeComponent();
        }
        private void FormMain_Load(object sender, EventArgs e)
        {

            //禁止自动分列
            dgvData.AutoGenerateColumns = false;
            dgvData.DataSource = dt;
            dt.Columns.Add("单号", Type.GetType("System.String"));
            dt.Columns.Add("重量(Kg)", Type.GetType("System.String"));
            dt.Columns.Add("目的地", Type.GetType("System.String"));
            dt.Columns.Add("时间", Type.GetType("System.String"));
            dt.Columns.Add("扫描时间(ms)", Type.GetType("System.String"));

            ////条码规则
            //dic.Add(551, new List<string> { "合肥", "1" });
            //dic.Add(021, new List<string> { "上海", "2" });
            //dic.Add(025, new List<string> { "南京", "3" });
            //dic.Add(711, new List<string> { "杭州", "4" });
            //dic.Add(161, new List<string> { "徐州", "5" });
            //dic.Add(0, new List<string> { "错误", "0" });
            //Loadlist();
            LoadFormInfo();
            LoadSetting();
            x = this.Width;
            y = this.Height;
            setTag(this);

        }

        // 设备对象
        private IDevice m_dev;
        private HalconHelper halconHelper;

        private int CodeSum=0;
        private int time1;
        private void LoadFormInfo()
        {
            try
            {
                halconHelper = new HalconHelper(hWindowControl1.HalconWindow);
                List<IDeviceInfo> li = Enumerator.EnumerateDevices();
                //if (Convert.ToInt32(this.Tag) != -1)
                if (li.Count > 0)
                {
                    m_dev = Enumerator.GetDeviceByIndex(0);
                    //m_dev = Enumerator.GetDeviceByIndex(Convert.ToInt32(this.Tag));
                    // 注册链接时间

                    m_dev.CameraOpened += OnCameraOpen;
                    m_dev.ConnectionLost += OnConnectLoss;
                    m_dev.CameraClosed += OnCameraClose;
                    // 打开设备
                    if (!m_dev.Open())
                    {
                        MessageBox.Show(@"连接相机失败");
                        return;
                    }

                    // 关闭Trigger
                    //m_dev.TriggerSet.Close();
                    // 打开Software Trigger
                    m_dev.TriggerSet.Open(TriggerSourceEnum.Software);
                    // 设置图像格式
                    using (IEnumParameter p = m_dev.ParameterCollection[ParametrizeNameSet.ImagePixelFormat])
                    {
                        p.SetValue("Mono8");
                    }

                    // 注册码流回调事件
                    m_dev.StreamGrabber.ImageGrabbed += OnImageGrabbed;

                    // 开启码流
                    if (!m_dev.GrabUsingGrabLoopThread())
                    {
                        MessageBox.Show(@"开启码流失败");
                        return;
                    }
                    else
                    {
                        m_dev.ExecuteSoftwareTrigger();
                    }
                }
                else
                {                  
                }
                //注册一维码识别完成事件
                halconHelper.Complete += OnComplete;
                halconHelper.Error += OnError;
                //注册串口接收事件
                serialComm.DateReceived += new Comm.EventHandle(OnDataReceived);
                weightComm.DateReceived += new Comm.EventHandle(OnWeightDataReceived);
            }
            catch(Exception ex)
            {
                Catcher.Show(ex);
            }

        }

        private void OnWeightDataReceived(byte[] readBuffer)
        {
            try
            {
                mTimOut2 = 0;
                byte[] readstring=new byte[readBuffer.Length]; //= Encoding.UTF8.GetString(readBuffer);
                for (int i = 0; i < readBuffer.Length - 1; i++)
                { 
                    readstring[i] = readBuffer[readBuffer.Length - i - 1];
                }
                weightStr = Encoding.UTF8.GetString(readstring).TrimEnd('\0');
                if (InvokeRequired)
                {

                  BeginInvoke(new MethodInvoker(() =>
                  {
                      if (!mLinkConnect2)
                      {
                          lbWeighter.BackColor = Color.Lime;
                          mLinkConnect2 = true;
                      }
                      lbWeight.Text = "重量    " + weightStr+"Kg";
                  }));
                }
            }
            catch
            {
            }
        }

        private void OnDataReceived(byte[] readBuffer)
        {
            var readstring = Encoding.UTF8.GetString(readBuffer);
            if(readstring.Contains("AT"))
            {             
                mTimeOut = 0;

                BeginInvoke(new MethodInvoker(() =>
                {
                    if (!mLinkConnect)
                    {
                        lbMotor.BackColor = Color.Lime;
                        mLinkConnect = true;
                    }
                }));
            }
            if(readstring.Contains("win"))
            {
                STM32Ready = true;
            }
            else if (readstring.Contains("warning"))
            {
                BeginInvoke(new MethodInvoker(() =>
                {
                    DialogResult dr = MessageBox.Show("电机异常，请检查设备并重启", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (dr == DialogResult.OK)
                    {
                        System.Environment.Exit(0);
                    }
                }));
            }
            if (readstring.Contains("two"))
            {
                BeginInvoke(new MethodInvoker(() =>
                {
                    DialogResult dr = MessageBox.Show("跑步机1异常，请检查设备", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (dr == DialogResult.OK)
                    {
                        System.Environment.Exit(0);
                    }
                }));
            }
            if (readstring.Contains("five"))
            {
                BeginInvoke(new MethodInvoker(() =>
                {
                    DialogResult dr = MessageBox.Show("跑步机2异常，请检查设备", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (dr == DialogResult.OK)
                    {
                        System.Environment.Exit(0);
                    }
                }));
            }
        }

        private void OnError(object sender, OnErrorEventArgs e)
        {
            this.Invoke(new Action(() => {
                if(e.Num==0)
                {
                    lbCode.ForeColor = Color.DarkBlue;
                    lbCode.Text = "单号   无有效单号";
                }                     
                else
                {
                    lbCode.ForeColor = Color.Red;
                    lbCode.Text = "单号   多个单号";
                }
                    
            }));
            
        }


        long nowtick,firsttick;
        double rate;
        private void OnComplete(object sender, OnCompleteEventArgs e)
        {
            //string strPort="";
            string portNum ;
            string  httpResponse;
            //bool IsDestionation= dic.TryGetValue(portNum, out List<string> destination);
            //if (IsDestionation)
            //{
            //    strDes=destination[0];
            //    strPort=destination[1];
            //}
            //else
            //{
            //    strDes = "无";
            //    strPort = "0";
            //}
            string time =DateTime.Now.ToString();          
           

            if (CodeSum==0)//记录第一次扫到条码的时间
            {
                firsttick = TimeTransitionHelper.GetCurrentTimeUnix();
            }
            this.Invoke(new Action(() =>
            {
                lbCode.ForeColor = Color.DarkBlue;
                lbCode.Text = "单号    " + e.DataStrings;             
            }));
            try
            {
                if (STM32Ready && mLinkConnect)
                {
                    if (NetWork.IsConnectInternet())
                    {
                        string data = "[{\"ticketsNum\":\"" + e.DataStrings + "\",\"workConsole\":\"" + workConsole + "\"}]";
                        httpResponse = HttpHelper.PostDataGetHtml(url, data);
                        JObject array = (JObject)JsonConvert.DeserializeObject(httpResponse);
                        portNum = array["port"].ToString().Trim('"');

                    }
                    else
                    {
                        return;
                    }
                    CodeSum++;
                    nowtick = TimeTransitionHelper.GetCurrentTimeUnix();
                    if((nowtick - firsttick) / 3600<1)
                        rate = (int)(CodeSum / ((nowtick - firsttick) / 3600+1));
                    else
                        rate = CodeSum / ((nowtick - firsttick) / 3600.0);

                    this.Invoke(new Action(() =>
                    {
                        lbSum.Text = "总数    " + Convert.ToString(CodeSum) + "/件";
                        lbRate.Text = "速率    " + rate.ToString("#0.00") + "件/小时";
                        lbDestination.Text = "目的地    " + portNum+"号口";                   
                        if (dt != null)
                        {
                            if (dgvData.RowCount > 3000)
                            {
                                if (IsSavaCode)
                                {
                                    string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\条码记录";
                                    if (!System.IO.Directory.Exists(dir))
                                    {
                                        //创建pic文件夹
                                        System.IO.Directory.CreateDirectory(dir);
                                    }
                                    string time2 = Regex.Replace(time, pattern, string.Empty, RegexOptions.IgnoreCase);
                                    ExcelHelper.ExportToExcel(dt, dir + "\\" + time2 + ".xls");
                                }
                                dt.Clear();
                            }
                            DataRow dr = dt.NewRow();                          
                            dr["单号"] = e.DataStrings;
                            dr["重量(Kg)"] = weightStr;
                            dr["目的地"] = portNum + "号口";
                            dr["时间"] = time;
                            dr["扫描时间(ms)"] = e.UseTime;
                            dt.Rows.Add(dr);
                            dgvData.FirstDisplayedScrollingRowIndex = dgvData.Rows.Count - 1;
                        }
                    }));

                    string sendstr = "win" + portNum + "\r\n";
                    byte[] send = Encoding.Default.GetBytes(sendstr);
                    serialComm.WritePort(send, 0, send.Length);
                    STM32Ready = false;
                    time1 = Environment.TickCount;
                }
            }
            catch(Exception ex)
            {
            }
        }

       
        //相机回流事件
        private void OnImageGrabbed(object sender, GrabbedEventArgs e)
        {
            HObject image;   
            try
            {
                HOperatorSet.GenImage1(out image, "byte", e.GrabResult.Width, e.GrabResult.Height, e.GrabResult.Raw);
                halconHelper.BarRecognition(image);               
                m_dev.ExecuteSoftwareTrigger();
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }


        // 相机打开回调
        private void OnCameraOpen(object sender, EventArgs e)
        {          
            this.Invoke(new Action(() =>
            {
                lbCemra.BackColor = Color.Lime;
            }));
        }
        // 相机关闭回调
        private void OnCameraClose(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                lbCemra.BackColor = Color.Red;
            }));
        }

        // 相机丢失回调
        private void OnConnectLoss(object sender, EventArgs e)
        {
            m_dev.ShutdownGrab();
            m_dev.Dispose();
            m_dev = null;           
            this.Invoke(new Action(() =>
            {
                lbCemra.BackColor = Color.Red;
            }));

        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                m_dev.TriggerSet.Close();
            }
            catch(Exception ex)
            {
               // Catcher.Show(ex);
            }
            System.Environment.Exit(0);
        }

        /// <summary>
        /// 暂时用不上,会增加工作量
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="image"></param>
        // C# bitmap变量转为 halcon变量
        private void Bitmap2HObject(Bitmap bmp, out HObject image)
        {
            try
            {
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

                BitmapData srcBmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);

                HOperatorSet.GenImage1(out image, "byte", bmp.Width, bmp.Height, srcBmpData.Scan0);
                bmp.UnlockBits(srcBmpData);
            }
            catch (Exception ex)
            {
                image = null;
            }
        }


        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myFormSetting == null || myFormSetting.IsDisposed)
            {
                myFormSetting = new FormSetting();
                myFormSetting.ShowDialog();
            }
            else
                myFormSetting.ShowDialog();
            LoadSetting();
        }

        private void LoadSetting()
        {
            //串口设置
            IniHelper.GetAllKeyValues("SerialPort_Setting", out string[] keys2, out string[] values2, iniHelper.FileName);
            IniHelper.GetAllKeyValues("WeightPort_Setting", out string[] keys3, out string[] values3, iniHelper.FileName);
            IniHelper.GetAllKeyValues("Other_Setting", out string[] keys4, out string[] values4, iniHelper.FileName);
            //post请求地址
            IniHelper.GetAllKeyValues("Http_Setting", out string[] keys, out string[] values, iniHelper.FileName);

            try
            {
                for (int i = 0; i < keys.Length; i++)
                {
                    if (keys.Length == 1)
                    {
                        url = values[0];                     
                    }
                }
                if (values2.Length>1)
                {
                    serialComm.Close();
                    serialComm._serialPort.PortName = values2[0];
                    serialComm._serialPort.BaudRate = Convert.ToInt32(values2[1]);
                    serialComm.Open();

                }
                if (values3.Length > 1)
                {
                    weightComm.Close();
                    weightComm._serialPort.PortName = values3[0];
                    weightComm._serialPort.BaudRate = Convert.ToInt32(values3[1]);
                    if (values3.Length > 2) workConsole = values3[2];
                    weightComm.Open();

                }
                if(values4.Length>0)
                {
                    if(Convert.ToBoolean(values4[0]))
                    {
                        IsSavaCode = true;
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        /// <summary>
        /// 简单的映射关系
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public int Reflect(string num)
        {
            try
            {
                if(Regex.IsMatch(num, pattern))
                {
                    return 0;
                }
                string re= num.Substring(num.Length - 6, 3);
                return Convert.ToInt32(re);
            }
            catch
            {
                return 0;
            }
        }


        #region 控件大小随窗体大小等比例缩放
        private float x;//定义当前窗体的宽度
        private float y;//定义当前窗体的高度        

        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    setTag(con);
                }
            }
        }
        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                //获取控件的Tag属性值，并分割后存储字符串数组
                if (con.Tag != null)
                {
                    string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                    //根据窗体缩放的比例确定控件的值
                    con.Width = Convert.ToInt32(System.Convert.ToSingle(mytag[0]) * newx);//宽度
                    con.Height = Convert.ToInt32(System.Convert.ToSingle(mytag[1]) * newy);//高度
                    con.Left = Convert.ToInt32(System.Convert.ToSingle(mytag[2]) * newx);//左边距
                    con.Top = Convert.ToInt32(System.Convert.ToSingle(mytag[3]) * newy);//顶边距
                    Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字体大小
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        setControls(newx, newy, con);
                    }
                }
            }

        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                this.Show();
            }
        }

        private void notifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Initializenotifyicon();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            mTimeOut++;
            mTimOut2++;
            if (mTimeOut >= 3)
            {
                lbMotor.BackColor = Color.Red;
                mLinkConnect = false;
                mTimeOut = 3;
            }
            if(mTimOut2 >= 3)
            {
                lbWeighter.BackColor = Color.Red;
                mLinkConnect2 = false;
                mTimOut2 = 3;
            }
            if (NetWork.IsConnectInternet())
            {
                lbNetwork.BackColor = Color.Lime;
            }
            else
                lbNetwork.BackColor = Color.Red;
            if (mPeriodCheck++ >= 1) // 1秒发送1次
            {
                string sendstr = "AT\r\n";
                byte[] send = Encoding.Default.GetBytes(sendstr);
                serialComm.WritePort(send, 0, send.Length);
                mPeriodCheck = 0;
            }

            if (Environment.TickCount - time1 > 10000)
            {
                if (!STM32Ready)  {

                    timer1.Enabled = false;
                    DialogResult dr=MessageBox.Show("电机异常，请检查设备并重启", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if(dr== DialogResult.OK)
                    {
                        System.Environment.Exit(0);
                    }
                }
            }
            if (!STM32Ready && mLinkConnect&& CodeSum!=0)
            {
                nowtick = TimeTransitionHelper.GetCurrentTimeUnix();
                nowtick = TimeTransitionHelper.GetCurrentTimeUnix();
                if ((nowtick - firsttick) / 3600 < 1)
                    rate = (int)(CodeSum / ((nowtick - firsttick) / 3600 + 1));
                else
                    rate = CodeSum / ((nowtick - firsttick) / 3600.0);
                lbRate.Text = "速率    " + rate.ToString("#0.00") + "件/小时";
            }
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / x;
            float newy = (this.Height) / y;
            setControls(newx, newy, this);
            setControls(newx, newy, groupBox1);
            setControls(newx, newy, dgvData);
            setControls(newx, newy, panel1);
        }

        #endregion


        #region 最小化到任务栏
        private ContextMenu notifyiconMnu;
        private string port;

        /// <summary>
        /// 最小化到任务栏
        /// </summary>
        private void Initializenotifyicon()
        {
            //定义一个MenuItem数组，并把此数组同时赋值给ContextMenu对象 
            MenuItem[] mnuItms = new MenuItem[3];
            mnuItms[0] = new MenuItem();
            mnuItms[0].Text = "显示窗口";
            mnuItms[0].Click += new System.EventHandler(this.notifyIcon1_showfrom);

            mnuItms[1] = new MenuItem("-");

            mnuItms[2] = new MenuItem();
            mnuItms[2].Text = "退出系统";
            mnuItms[2].Click += new System.EventHandler(this.ExitSelect);
            mnuItms[2].DefaultItem = true;

            notifyiconMnu = new ContextMenu(mnuItms);
            notifyIcon1.ContextMenu = notifyiconMnu;
            //为托盘程序加入设定好的ContextMenu对象 
        }

        public void notifyIcon1_showfrom(object sender, System.EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;
                notifyIcon1.Visible = false;
            }
        }

        public void ExitSelect(object sender, System.EventArgs e)
        {
            //隐藏托盘程序中的图标 
            notifyIcon1.Visible = false;
            //关闭系统 
            this.Dispose(true);
            Environment.Exit(0);
        }
        #endregion
    }
}
