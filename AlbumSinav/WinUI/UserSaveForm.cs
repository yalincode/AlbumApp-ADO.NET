using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlbumSinav.WinUI
{
    using DAL.Entities;
    using DAL.Repository;
    public partial class UserSaveForm : Form
    {
        UserRepo repo;
        public UserSaveForm()
        {
            InitializeComponent();
            repo =new UserRepo();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "" || txtEmail.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Lütfen düzgün kullanıcı kayıt girişi yapınız");
                return;
            }
            User user = new User();
            user.UserName=txtUserName.Text;
            user.UserLastName=txtUserLastName.Text;
            user.UserPhone=txtPhone.Text;
            user.UserEmail=txtEmail.Text;
            user.UserPassword=txtPassword.Text;

            repo.Create(user);

            MessageBox.Show("Kullanıcı kaydı başarıyla yapıldı");
        }
    }
}
