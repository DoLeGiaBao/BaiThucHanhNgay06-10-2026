using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Bai5_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Mật khẩu hiển thị bằng dấu *
            txtPassword.UseSystemPasswordChar = true;
            txtConfirmPassword.UseSystemPasswordChar = true;

            // Mặc định ngày sinh là ngày hiện tại
            dtpBirthDate.Value = DateTime.Today;
        }

        // ==============================
        // NÚT ĐĂNG KÝ
        // ==============================
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Xóa các lỗi cũ
            errorProvider1.Clear();

            bool isValid = true;

            // 1. Kiểm tra tên đăng nhập
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                errorProvider1.SetError(
                    txtUsername,
                    "Tên đăng nhập không được để trống!"
                );

                isValid = false;
            }

            // 2. Kiểm tra mật khẩu
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider1.SetError(
                    txtPassword,
                    "Mật khẩu không được để trống!"
                );

                isValid = false;
            }

            // 3. Kiểm tra xác nhận mật khẩu
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                errorProvider1.SetError(
                    txtConfirmPassword,
                    "Mật khẩu nhập lại không khớp!"
                );

                isValid = false;
            }

            // 4. Tính tuổi
            DateTime today = DateTime.Today;
            DateTime birthDate = dtpBirthDate.Value.Date;

            int age = today.Year - birthDate.Year;

            if (birthDate > today.AddYears(-age))
            {
                age--;
            }

            // 5. Kiểm tra tuổi >= 18
            if (age < 18)
            {
                errorProvider1.SetError(
                    dtpBirthDate,
                    "Người đăng ký phải đủ 18 tuổi!"
                );

                isValid = false;
            }

            // 6. Kiểm tra điều khoản dịch vụ
            if (!chkTerms.Checked)
            {
                errorProvider1.SetError(
                    chkTerms,
                    "Bạn phải đồng ý với điều khoản dịch vụ!"
                );

                isValid = false;
            }

            // 7. Nếu tất cả hợp lệ
            if (isValid)
            {
                MessageBox.Show(
                    "Đăng ký tài khoản thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        // ==============================
        // NÚT LÀM MỚI
        // ==============================
        private void btnReset_Click(object sender, EventArgs e)
        {
            // Xóa tên đăng nhập
            txtUsername.Clear();

            // Xóa mật khẩu
            txtPassword.Clear();

            // Xóa xác nhận mật khẩu
            txtConfirmPassword.Clear();

            // Đưa ngày sinh về ngày hiện tại
            dtpBirthDate.Value = DateTime.Today;

            // Bỏ chọn giới tính
            rdoMale.Checked = false;
            rdoFemale.Checked = false;

            // Bỏ chọn điều khoản
            chkTerms.Checked = false;

            // Xóa tất cả thông báo lỗi
            errorProvider1.Clear();

            // Đưa con trỏ về ô tên đăng nhập
            txtUsername.Focus();
        }

        // ==============================
        // CHECKBOX ĐIỀU KHOẢN
        // ==============================
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Nếu người dùng đã tích điều khoản
            // thì xóa thông báo lỗi của CheckBox
            if (chkTerms.Checked)
            {
                errorProvider1.SetError(chkTerms, "");
            }
        }

        // ==============================
        // FORM LOAD
        // ==============================
        private void lblConfirmPassword_Load(object sender, EventArgs e)
        {
            // Thiết lập mật khẩu dạng ký tự ẩn
            txtPassword.UseSystemPasswordChar = true;
            txtConfirmPassword.UseSystemPasswordChar = true;

            // Đặt ngày sinh mặc định
            dtpBirthDate.Value = DateTime.Today;
        }
    }
}
