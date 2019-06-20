using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThridLibray;

namespace UI.IUI
{
    public partial class Form_equipment : Form
    {
        public Form_equipment()
        {
            InitializeComponent();
        }

        private void Form_equipment_Load(object sender, EventArgs e)
        {
            //设备搜索
            List<IDeviceInfo> li = Enumerator.EnumerateDevices();
            if (li.Count > 0)
            {
                //禁用列表的自动生成
                dgvList.AutoGenerateColumns = true;
                dgvList.DataSource = new BindingList <IDeviceInfo>(li);

            }

        }

        private void dgvList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(dgvList.SelectedCells.Count!=0)
            {
                FormMain formMain = new FormMain();
                formMain.Tag = dgvList.SelectedCells[0].RowIndex;
                formMain.Show();
                this.Hide();
            }

        }
    }
}
