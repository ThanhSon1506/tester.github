using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BAL;
using BEL;

namespace do_an_wind
{
    public partial class FormDangKy : Form
    {
        private DateTime dateTimeNow = new DateTime();

        public FormDangKy()
        {
            InitializeComponent();
        }

        private void FormDangKy_Load(object sender, EventArgs e)
        {
            dateTimeNow = dateTimeNgaySinh.Value;
        }

        private bool kiemTraDuLieuNhap()
        {
            if (txtTenNhanVien.Text.Trim() != "")
            {
                foreach (char c in txtTenNhanVien.Text)
                {
                    if (char.IsDigit(c))
                    {
                        MessageBox.Show("Tên nhân viên không được có số !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Tên nhân viên không được bỏ trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (txtSDT.Text.Trim().Length == 10)
            {
                try
                {
                    long check = long.Parse(txtSDT.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("Số điện thoại phải có đủ mười chữ số và không được có chữ !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Số điện thoại phải có đủ mười chữ số !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (txtDiaChi.Text.Trim().Length < 10)
            {
                MessageBox.Show("Địa chỉ phải có ít nhất 10 ký tự !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (txtDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Địa chỉ không được bỏ trống !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (dateTimeNgaySinh.Value >= dateTimeNow)
            {
                MessageBox.Show("Ngày sinh không được bé hơn ngày hiện tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private void btnDK_Click(object sender, EventArgs e)
        {
            if (!kiemTraDuLieuNhap())
                return;
            try
            {
                BAL_nhanvien nv = new BAL_nhanvien();
                BEL_nhanvien nhanvien = new BEL_nhanvien
                {
                    Ten_nhan_vien = txtTenNhanVien.Text.Trim(),
                    Gioi_tinh = rdbNam.Checked,
                    Sdt = txtSDT.Text.Trim(),
                    Quan_ly = false,
                    Ngay_sinh = dateTimeNgaySinh.Value,
                    Dia_chi = txtDiaChi.Text.Trim(),
                    Da_xoa = false,
                };
                if (nv.Themnhanvien(nhanvien))
                {
                    BAL.BAL_nhanvien xuly_addTKNV = new BAL.BAL_nhanvien();
                    nhanvien.Ma_nhan_vien = xuly_addTKNV.Max_nhanvien();
                    string mahoa_manv = xuly_addTKNV.Mahoakitu(nhanvien.Ma_nhan_vien);
                    nhanvien.Sdt = xuly_addTKNV.Mahoakitu(nhanvien.Sdt);
                    xuly_addTKNV.add_tknv(nhanvien, mahoa_manv);
                    MessageBox.Show(
                        "Đăng ký thành công !\nTài khoảng : " + nhanvien.Ma_nhan_vien + "\n" +
                        "Mật khẩu : " + txtSDT.Text.Trim() + "\n",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Đăng ký khônng thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Đăng ký khônng thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
