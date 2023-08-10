using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab04
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tb_Nhanvien Log_in = new Tb_Nhanvien();
            string MANV = "";
            MANV = Log_in.Login(tbUser.Text, tbPass.Text);
            if (MANV != "")
            {
                this.Hide();
                Management Manage = new Management();
                Manage.ShowDialog();
                this.Close();

            }
            else
                MessageBox.Show("Tên đăng nhập và mật khẩu không hợp lệ", "Đăng nhập thât bại", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
