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
    public partial class Management : Form
    {
        public event System.Windows.Forms.DataGridViewCellEventHandler CellClick;

        public Management()
        {
            InitializeComponent();
            Show_Nhanvien();
        }

        public void Show_Nhanvien()
        {
            Tb_Nhanvien Data = new Tb_Nhanvien();
            DataTable dt = Data.Sel_Nhanvien();
            dtgvShow.AutoGenerateColumns = false;
            dtgvShow.DataSource = dt;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Tb_Nhanvien Nhanvien = new Tb_Nhanvien();
            Nhanvien.Upd_Nhanvien(tbManv.Text, tbHoten.Text, tbEmail.Text, tbLuong.Text, tbTenDn.Text, tbMatkhau.Text);
            Show_Nhanvien();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Tb_Nhanvien AddNhanvien = new Tb_Nhanvien();
            AddNhanvien.ins_Nhanvien(tbManv.Text, tbHoten.Text, tbEmail.Text, tbLuong.Text, tbTenDn.Text, tbMatkhau.Text);
            Show_Nhanvien();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dtgvShow_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbManv.ReadOnly = true;
            tbMatkhau.ReadOnly = true;
            int t = dtgvShow.CurrentCell.RowIndex;
            tbManv.Text = dtgvShow.Rows[t].Cells["MANV"].Value.ToString();
            tbHoten.Text = dtgvShow.Rows[t].Cells["HOTEN"].Value.ToString();
            tbEmail.Text = dtgvShow.Rows[t].Cells["EMAIL"].Value.ToString();
            tbLuong.Text = dtgvShow.Rows[t].Cells["LUONGMH"].Value.ToString();
            tbTenDn.Text = dtgvShow.Rows[t].Cells["TENDN"].Value.ToString();
            tbMatkhau.Text = dtgvShow.Rows[t].Cells["MATKHAU"].Value.ToString();
            if (tbManv.Text == "")
            {
                tbManv.ReadOnly = false;
            }

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            Tb_Nhanvien nhanvien = new Tb_Nhanvien();
            nhanvien.Del_Nhanvien(tbManv.Text);
            Show_Nhanvien();
        }
    }
}
