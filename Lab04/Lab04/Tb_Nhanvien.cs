using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab04
{

    class Tb_Nhanvien
    {
        DataConnection dc;
        public string Login(string User, string Pass)
        {
            dc = new DataConnection();
            string data = "";
            SqlConnection con = dc.GetConnect();
            con.Open();
            try
            {
                string str_Pass = "0x" + BitConverter.ToString(DataEncryption.EncryptStringToBytes_SHA1(Pass)).Replace("-", "");
                SqlCommand sql = new SqlCommand("select *from NHANVIEN where TENDN ='" + User + "' AND MATKHAU = " + str_Pass, con);
                SqlDataAdapter da = new SqlDataAdapter(sql);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                        data = dr["MANV"].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }


            return data;
        }

        public bool KT_Nhanvien(string MANV, string TENDN)
        {
            string Manv = "";
            dc = new DataConnection();
            SqlConnection con = dc.GetConnect();
            con.Open();
            SqlCommand sql = new SqlCommand("select*from NHANVIEN where MANV = '" + MANV + "' or TENDN = '" + TENDN + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(sql);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
                Manv = dr["MANV"].ToString();
            if (Manv != "")
                return false;
            return true;
        }
        public void ins_Nhanvien(string MANV, string HOTEN, string EMAIL, string LUONG, string TENDN, string MATKHAU)
        {
            if (MANV == "" || TENDN == "" || MATKHAU == "")
                MessageBox.Show("Mã nhân viên hoặc tên đăng nhập hoặc mật khẩu không thể để trống", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (KT_Nhanvien(MANV, TENDN))
            {
                dc = new DataConnection();
                SqlConnection con = dc.GetConnect();
                byte[] LUONGMH = DataEncryption.EncryptStringToBytes_Aes256(LUONG);
                byte[] MK = DataEncryption.EncryptStringToBytes_SHA1(MATKHAU);
                SqlCommand ins_Proc = new SqlCommand("SP_INS_ENCRYPT_NHANVIEN", con);
                ins_Proc.CommandType = CommandType.StoredProcedure;
                ins_Proc.Parameters.Add("@MANV", SqlDbType.VarChar).Value = MANV;
                ins_Proc.Parameters.Add("@HOTEN", SqlDbType.Text).Value = HOTEN;
                ins_Proc.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = EMAIL;
                ins_Proc.Parameters.Add("@LUONG", SqlDbType.VarBinary).Value = LUONGMH;
                ins_Proc.Parameters.Add("@TENDN", SqlDbType.NVarChar).Value = TENDN;
                ins_Proc.Parameters.Add("@MATKHAU", SqlDbType.VarBinary).Value = MK;

                con.Open();
                int n = ins_Proc.ExecuteNonQuery();
                if (n > 0)
                    MessageBox.Show("Thêm thành công", "Thành công!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Thêm thất bại", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
            else
                MessageBox.Show("Tên đăng nhập hay mã nhân viên đã tồn tại", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public DataTable Sel_Nhanvien()
        {
            DataTable data = new DataTable();
            dc = new DataConnection();
            SqlConnection con = dc.GetConnect();
            SqlCommand sql = new SqlCommand("SP_SEL_ENCRYPT_NHANVIEN", con);
            sql.CommandType = CommandType.StoredProcedure;
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter(sql);
            data.Load(sql.ExecuteReader());
            con.Close();
            data.Columns.Add("LUONGMH", typeof(string));
            data.Columns.Add("MK", typeof(string));

            foreach (DataRow dr in data.Rows)
            {
                string LuongMH = "";
                LuongMH = DataEncryption.DecryptStringFromBytes_Aes256((byte[])dr["LUONG"]);
                dr["LUONGMH"] = LuongMH;
            }
            return data;
        }
        public void Del_Nhanvien(string Manv)
        {
            if (Manv != "lpthanh")
            {
                string query = "delete from NHANVIEN where MANV like '" + Manv + "'";
                dc = new DataConnection();
                SqlConnection con = dc.GetConnect();
                SqlCommand sql = new SqlCommand(query, con);
                con.Open();
                sql.ExecuteNonQuery();

            }
            else
            {
                MessageBox.Show("Không thể xóa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool KT_TenDn_Nhanvien(string MANV, string TENDN)
        {
            string Manv = "";
            dc = new DataConnection();
            SqlConnection con = dc.GetConnect();
            con.Open();
            SqlCommand sql = new SqlCommand("select*from NHANVIEN where MANV != '" + MANV + "' and TENDN = '" + TENDN + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(sql);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
                Manv = dr["MANV"].ToString();
            if (Manv != "")
                return false;
            return true;

        }
        public void Upd_Nhanvien(string MANV, string HOTEN, string EMAIL, string LUONG, string TENDN, string MATKHAU)
        {
            if (TENDN == "" || MATKHAU == "")
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không thể để trống", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (KT_TenDn_Nhanvien(MANV, TENDN))
            {
                dc = new DataConnection();
                SqlConnection con = dc.GetConnect();
                byte[] LUONGMH = DataEncryption.EncryptStringToBytes_Aes256(LUONG);
                string query = "SP_UPD_ENCRYPT_NHANVIEN";
                SqlCommand upd_Proc = new SqlCommand(query, con);
                upd_Proc.CommandType = CommandType.StoredProcedure;
                upd_Proc.Parameters.Add("@MANV", SqlDbType.VarChar).Value = MANV;
                upd_Proc.Parameters.Add("@HOTEN", SqlDbType.Text).Value = HOTEN;
                upd_Proc.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = EMAIL;
                upd_Proc.Parameters.Add("@LUONG", SqlDbType.VarBinary).Value = LUONGMH;
                upd_Proc.Parameters.Add("@TENDN", SqlDbType.NVarChar).Value = TENDN;
                con.Open();
                int n = upd_Proc.ExecuteNonQuery();
                if (n > 0)
                    MessageBox.Show("Sửa thành công", "Thành công!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Sửa thất bại", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
            else
                MessageBox.Show("Tên đăng nhập hay mã nhân viên đã tồn tại", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        ~Tb_Nhanvien()
        {
            dc = null;
        }
    }
}
