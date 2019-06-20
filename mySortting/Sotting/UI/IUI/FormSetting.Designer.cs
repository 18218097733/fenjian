namespace UI.IUI
{
    partial class FormSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageHttp = new System.Windows.Forms.TabPage();
            this.btHttp = new System.Windows.Forms.Button();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPageWalk = new System.Windows.Forms.TabPage();
            this.btRun = new System.Windows.Forms.Button();
            this.cBBaud = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cBCom = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageWeigher = new System.Windows.Forms.TabPage();
            this.cbWorkSole = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btWeight = new System.Windows.Forms.Button();
            this.cBWeightBaund = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cBWeightComm = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonOther = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxSaveCode = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPageHttp.SuspendLayout();
            this.tabPageWalk.SuspendLayout();
            this.tabPageWeigher.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageHttp);
            this.tabControl1.Controls.Add(this.tabPageWalk);
            this.tabControl1.Controls.Add(this.tabPageWeigher);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(634, 290);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageHttp
            // 
            this.tabPageHttp.Controls.Add(this.btHttp);
            this.tabPageHttp.Controls.Add(this.tbURL);
            this.tabPageHttp.Controls.Add(this.label3);
            this.tabPageHttp.Location = new System.Drawing.Point(4, 22);
            this.tabPageHttp.Name = "tabPageHttp";
            this.tabPageHttp.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHttp.Size = new System.Drawing.Size(626, 264);
            this.tabPageHttp.TabIndex = 0;
            this.tabPageHttp.Text = "Http设置";
            this.tabPageHttp.UseVisualStyleBackColor = true;
            // 
            // btHttp
            // 
            this.btHttp.Location = new System.Drawing.Point(526, 233);
            this.btHttp.Name = "btHttp";
            this.btHttp.Size = new System.Drawing.Size(75, 23);
            this.btHttp.TabIndex = 8;
            this.btHttp.Text = "确定";
            this.btHttp.UseVisualStyleBackColor = true;
            this.btHttp.Click += new System.EventHandler(this.btHttp_Click);
            // 
            // tbURL
            // 
            this.tbURL.Location = new System.Drawing.Point(75, 35);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(526, 21);
            this.tbURL.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "URL";
            // 
            // tabPageWalk
            // 
            this.tabPageWalk.Controls.Add(this.btRun);
            this.tabPageWalk.Controls.Add(this.cBBaud);
            this.tabPageWalk.Controls.Add(this.label2);
            this.tabPageWalk.Controls.Add(this.cBCom);
            this.tabPageWalk.Controls.Add(this.label1);
            this.tabPageWalk.Location = new System.Drawing.Point(4, 22);
            this.tabPageWalk.Name = "tabPageWalk";
            this.tabPageWalk.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWalk.Size = new System.Drawing.Size(626, 264);
            this.tabPageWalk.TabIndex = 1;
            this.tabPageWalk.Text = "跑步机设置";
            this.tabPageWalk.UseVisualStyleBackColor = true;
            // 
            // btRun
            // 
            this.btRun.Location = new System.Drawing.Point(516, 233);
            this.btRun.Name = "btRun";
            this.btRun.Size = new System.Drawing.Size(75, 23);
            this.btRun.TabIndex = 7;
            this.btRun.Text = "确定";
            this.btRun.UseVisualStyleBackColor = true;
            this.btRun.Click += new System.EventHandler(this.btRun_Click);
            // 
            // cBBaud
            // 
            this.cBBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBBaud.FormattingEnabled = true;
            this.cBBaud.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cBBaud.Location = new System.Drawing.Point(80, 54);
            this.cBBaud.Name = "cBBaud";
            this.cBBaud.Size = new System.Drawing.Size(95, 20);
            this.cBBaud.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "波特率";
            // 
            // cBCom
            // 
            this.cBCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBCom.FormattingEnabled = true;
            this.cBCom.Location = new System.Drawing.Point(80, 16);
            this.cBCom.Name = "cBCom";
            this.cBCom.Size = new System.Drawing.Size(95, 20);
            this.cBCom.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "串口设置";
            // 
            // tabPageWeigher
            // 
            this.tabPageWeigher.Controls.Add(this.cbWorkSole);
            this.tabPageWeigher.Controls.Add(this.label7);
            this.tabPageWeigher.Controls.Add(this.btWeight);
            this.tabPageWeigher.Controls.Add(this.cBWeightBaund);
            this.tabPageWeigher.Controls.Add(this.label5);
            this.tabPageWeigher.Controls.Add(this.cBWeightComm);
            this.tabPageWeigher.Controls.Add(this.label6);
            this.tabPageWeigher.Location = new System.Drawing.Point(4, 22);
            this.tabPageWeigher.Name = "tabPageWeigher";
            this.tabPageWeigher.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWeigher.Size = new System.Drawing.Size(626, 264);
            this.tabPageWeigher.TabIndex = 2;
            this.tabPageWeigher.Text = "称台号";
            this.tabPageWeigher.UseVisualStyleBackColor = true;
            // 
            // cbWorkSole
            // 
            this.cbWorkSole.AutoCompleteCustomSource.AddRange(new string[] {
            "SM01",
            "SM02",
            "SM03",
            "SM04",
            "SM05",
            "SM06",
            "SM07",
            "SM08",
            "SM09"});
            this.cbWorkSole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWorkSole.FormattingEnabled = true;
            this.cbWorkSole.Items.AddRange(new object[] {
            "SM01",
            "SM02",
            "SM03",
            "SM04",
            "SM05",
            "SM06",
            "SM07",
            "SM08",
            "SM09"});
            this.cbWorkSole.Location = new System.Drawing.Point(84, 99);
            this.cbWorkSole.Name = "cbWorkSole";
            this.cbWorkSole.Size = new System.Drawing.Size(95, 20);
            this.cbWorkSole.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "称台号";
            // 
            // btWeight
            // 
            this.btWeight.Location = new System.Drawing.Point(521, 233);
            this.btWeight.Name = "btWeight";
            this.btWeight.Size = new System.Drawing.Size(75, 23);
            this.btWeight.TabIndex = 8;
            this.btWeight.Text = "确定";
            this.btWeight.UseVisualStyleBackColor = true;
            this.btWeight.Click += new System.EventHandler(this.btWeight_Click);
            // 
            // cBWeightBaund
            // 
            this.cBWeightBaund.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBWeightBaund.FormattingEnabled = true;
            this.cBWeightBaund.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cBWeightBaund.Location = new System.Drawing.Point(84, 54);
            this.cBWeightBaund.Name = "cBWeightBaund";
            this.cBWeightBaund.Size = new System.Drawing.Size(95, 20);
            this.cBWeightBaund.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "波特率";
            // 
            // cBWeightComm
            // 
            this.cBWeightComm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBWeightComm.FormattingEnabled = true;
            this.cBWeightComm.Location = new System.Drawing.Point(84, 16);
            this.cBWeightComm.Name = "cBWeightComm";
            this.cBWeightComm.Size = new System.Drawing.Size(95, 20);
            this.cBWeightComm.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "串口设置";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonOther);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(626, 264);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "其它设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonOther
            // 
            this.buttonOther.Location = new System.Drawing.Point(527, 233);
            this.buttonOther.Name = "buttonOther";
            this.buttonOther.Size = new System.Drawing.Size(75, 23);
            this.buttonOther.TabIndex = 3;
            this.buttonOther.Text = "确定";
            this.buttonOther.UseVisualStyleBackColor = true;
            this.buttonOther.Click += new System.EventHandler(this.buttonOther_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxSaveCode);
            this.groupBox2.Location = new System.Drawing.Point(19, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(583, 58);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "条码记录";
            // 
            // checkBoxSaveCode
            // 
            this.checkBoxSaveCode.AutoSize = true;
            this.checkBoxSaveCode.Location = new System.Drawing.Point(23, 20);
            this.checkBoxSaveCode.Name = "checkBoxSaveCode";
            this.checkBoxSaveCode.Size = new System.Drawing.Size(96, 16);
            this.checkBoxSaveCode.TabIndex = 0;
            this.checkBoxSaveCode.Text = "保存条码记录";
            this.checkBoxSaveCode.UseVisualStyleBackColor = true;
            // 
            // FormSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 290);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSetting";
            this.Text = "设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSetting_FormClosing);
            this.Load += new System.EventHandler(this.FormSetting_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageHttp.ResumeLayout(false);
            this.tabPageHttp.PerformLayout();
            this.tabPageWalk.ResumeLayout(false);
            this.tabPageWalk.PerformLayout();
            this.tabPageWeigher.ResumeLayout(false);
            this.tabPageWeigher.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageHttp;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPageWalk;
        private System.Windows.Forms.ComboBox cBBaud;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageWeigher;
        private System.Windows.Forms.Button btHttp;
        private System.Windows.Forms.Button btRun;
        private System.Windows.Forms.ComboBox cBCom;
        private System.Windows.Forms.Button btWeight;
        private System.Windows.Forms.ComboBox cBWeightBaund;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cBWeightComm;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox checkBoxSaveCode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonOther;
        private System.Windows.Forms.ComboBox cbWorkSole;
        private System.Windows.Forms.Label label7;
    }
}