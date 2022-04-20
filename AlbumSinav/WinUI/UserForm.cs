using AlbumSinav.WinUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlbumSinav
{
    using DAL.Repository;
    using DAL.Entities;
    public partial class UserForm : Form
    {
        UserRepo repo;
        public User selectedUser;
        public UserForm()
        {
            InitializeComponent();
            repo=new UserRepo();
        }
        private void UserForm_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
        }

        //Password show - hide settings
        public void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            //checkBox işaretli ise
            if (checkBox1.Checked)
            {
                //karakteri göster.
                textBox2.PasswordChar = '\0';
            }
            //değilse karakterlerin yerine * koy.
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var users = repo.GetAll();
            foreach (var user in users)
            {
                if (user.UserEmail==textBox1.Text && user.UserPassword==textBox2.Text)
                {
                    MessageBox.Show("Giriş işlemi başarılı");
                    MainForm form = new MainForm();
                    selectedUser=user;
                    form.Tag = user.UserId;
                    form.ShowDialog();
                    return;
                }
            }
            MessageBox.Show("Kullanıcı adı veya şifre hatalı");



        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            UserSaveForm form=new UserSaveForm();
            form.ShowDialog();
        }

        
    }
}
