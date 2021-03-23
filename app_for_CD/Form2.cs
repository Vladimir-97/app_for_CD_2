using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app_for_CD
{

    public partial class filter : Form
    {
        public filter()
        {
            InitializeComponent();
        }
        private void CRM_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.f_c = 1;
            }
            else
            {
                Data.f_c = 0;
            }
        }

        private void Серия_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.f_s = 1;
            }
            else
            {
                Data.f_s = 0;
            }
        }
        string change_format_dateTime(string dt)
        {
            string tmp;
            tmp = "20" + dt[9] + dt[10] + dt[6] + dt[7] + dt[3] + dt[4];
            return tmp;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Data.st_data_orig = change_format_dateTime(dateTimePicker1.Value.ToString());
            Data.end_data_orig = change_format_dateTime(dateTimePicker2.Value.ToString());
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                Data.f_d = 1;
            }
            else
            {
                Data.f_d = 0;
            }
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
