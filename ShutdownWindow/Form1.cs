using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;   
namespace ShutdownWindow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private int hours;
        private int mins;
        private int seconds;
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblRTimeNow.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbbMethod.SelectedIndex = 0;
            numTimeAlert.Visible = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            numTimeAlert.Visible = checkBox1.Checked;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(numMin.Value<1 && numHour.Value==0)
            {
                MessageBox.Show("Bạn chưa chọn thời gian.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }    
            else
            {
                hours = (int)numHour.Value;
                mins = (int)numMin.Value;

                timer2.Start();
                timer1.Enabled = true;
                btnStart.Enabled = false;
                btnCancel.Enabled = true;

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (hours > 0 | mins > 0 | seconds > 0)
            {
                if (mins == 0 && hours > 0) { mins = 59; hours = hours - 1; }
                if (seconds == 0 && mins > 0) { seconds = 59; mins = mins - 1; }
                seconds = seconds - 1;
            }
            numHour.Value = hours;
            numMin.Value = mins;
            //hien thi thoi gian con lai
            lblTimeLeft.Text = string.Format("{0}:{1}:{2}", formatHour(hours), formatHour(mins), formatHour(seconds));

            if (checkBox1.Checked)
            {
                string msg = string.Format("Máy tính của bạn sẽ {0} sau {1} phút nữa. Vui lòng lưu tài liệu đang dở và đóng cửa sổ làm việc. ",cbbMethod,mins);
                if (mins == numTimeAlert.Value && seconds == 0) MessageBox.Show(msg, "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // thuc hien chuc nang khi het gio
            if(hours==0 && seconds==0 && mins == 0)
            {
                timer2.Stop();
                int select = cbbMethod.SelectedIndex;
                switch(select)
                {
                    default:
                        case 0:
                        MessageBox.Show("Shutdown");
                        Process.Start("shutdown", "/s /t 0");
                        break;
                                       
                }    
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            btnStart.Enabled = true;
            btnCancel.Enabled = false;
            lblTimeLeft.Text = "00:00:00";
        }
        //dinh dang gio
        private string formatHour(int s)
        {
            string t = s.ToString();
            return s < 10 ? "0" + t : t;
        }
    }
}
