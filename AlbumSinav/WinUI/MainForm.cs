using AlbumSinav.DAL.Repository;
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
    public partial class MainForm : Form
    {
        SQLRepo sqlRepo;
        CategoryRepo categoryRepo;
        SingerRepo singerRepo;
        public MainForm()
        {
            InitializeComponent();
            sqlRepo = new SQLRepo();
            categoryRepo = new CategoryRepo();
            singerRepo = new SingerRepo();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = sqlRepo.top10AlbumSonEklenen();
        }

        private void sanatçıListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SingerListForm form=new SingerListForm();
            form.ShowDialog();
        }

        private void yeniSanatçıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SingerSaveForm form=new SingerSaveForm();
            form.ShowDialog();
        }

        private void kategoriListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CategoryListForm form=new CategoryListForm();
            form.ShowDialog();
        }

        private void yeniKategoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CategorySaveForm form=new CategorySaveForm();
            form.ShowDialog();
        }

        private void albümListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlbumListForm form=new AlbumListForm();
            form.ShowDialog();
        }

        private void yeniAlbümToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlbumSaveForm form = new AlbumSaveForm();   
            form.ShowDialog();
        }

        private void siparişListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrderListForm form =new OrderListForm();
            form.selectedUser = Convert.ToInt32(this.Tag);
            form.ShowDialog();
        }

        private void yeniSiparişToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrderSaveForm form=new OrderSaveForm();
            form.selectedUser = Convert.ToInt32(this.Tag);
            form.ShowDialog();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource= sqlRepo.top10AlbumSonEklenen();

            dataGridView2.DataSource = null;
            dataGridView2.DataSource = sqlRepo.indirimdeki15Album();

            var categories = categoryRepo.GetAll();
            cmbCategory.DataSource= categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryId";
            cmbCategory.SelectedIndexChanged += CmbCategory_SelectedIndexChanged;


            var singers = singerRepo.GetAll();
            cmbSinger.DataSource = singers;
            cmbSinger.DisplayMember = "SingerName";
            cmbSinger.ValueMember = "SingerId";
            cmbSinger.SelectedIndexChanged += CmbSinger_SelectedIndexChanged;

            dataGridView5.DataSource = null;
            dataGridView5.DataSource = sqlRepo.encoksatinalinmisAlbums();

            dataGridView6.DataSource = null;
            dataGridView6.DataSource = sqlRepo.encoksatanSanatcı();


        }

        private void CmbSinger_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView4.DataSource = null;
            dataGridView4.DataSource = sqlRepo.secilenSanatcıyaGoreAlbum(cmbSinger);
        }

        private void CmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView3.DataSource = null;
            dataGridView3.DataSource = sqlRepo.secilenTureGoreAlbum(cmbCategory);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
