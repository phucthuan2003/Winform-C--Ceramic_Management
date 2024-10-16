using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Ceramic_Management
{
    public partial class Login : Form
    {
        private ProcessDataBase conn;   
        public Login()
        {
            InitializeComponent();
            conn = new ProcessDataBase();   
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string UserName = txtUser.Text;
            string Password = txtPassword.Text;

            // Kiểm tra các trường xem có rỗng hay không
            if (UserName.Trim() == "")
            {
                MessageBox.Show("Requied enter username!", "Announce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (Password.Trim() == "")
            {
                MessageBox.Show("Requied enter password!", "Announce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else 
            {
                // Gọi hàm verifyUser để kiểm tra thông tin đăng nhập và lấy mã nhân viên
                string IdStaff = VerifyUser(UserName, Password);
                if (IdStaff != null)
                {
                    // Thông báo đăng nhập thành công
                    guna2MessageDialogSuccess.Show();
                   

                    // Chuyển sang trang UserDetails và truyền mã nhân viên 
                    this.Hide();
                    HomePage homePage = new HomePage(IdStaff);
                    homePage.Show();
                }
                else 
                {
                    // Thông báo đăng nhập thất bại
                    MessageBox.Show("Username or Password incorrect.", "Announce", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private string VerifyUser(string username, string password) 
        {
            string query = $"SELECT MaNV FROM TaiKhoan WHERE TenDangNhap = '{username}' AND MatKhau =  '{password}'";
            DataTable result = conn.DocBang(query);
            if (result.Rows.Count > 0) 
            {
                return result.Rows[0]["MaNV"].ToString(); // Lấy mã nhân viên
            }
            return null; // trả về null nếu không tìm thấy
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '•';
        }

        private bool isPasswordVisible = false;

        private void opShow_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            if (isPasswordVisible)
            {
                txtPassword.PasswordChar = '\0'; // Show password
            }
            else 
            {
                txtPassword.PasswordChar = '•';
            }
        }
    }
}
