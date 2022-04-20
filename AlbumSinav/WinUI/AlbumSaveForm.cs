using AlbumSinav.DAL.Entities;
using AlbumSinav.DAL.Repository;
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
    public partial class AlbumSaveForm : Form
    {
        AlbumRepo albumRepo;
        CategoryRepo categoryRepo;
        SingerRepo singerRepo;
        public AlbumSaveForm()
        {
            InitializeComponent();
            albumRepo = new AlbumRepo();
            categoryRepo = new CategoryRepo();  
            singerRepo = new SingerRepo();
        }
        Album selectedAlbum;
        private void AlbumSaveForm_Load(object sender, EventArgs e)
        {
            FillControl();
            FillData();
        }

        private void FillControl()
        {
            FillSinger();
            FillCategory();
        }

        private void FillCategory()
        {
            var categories = categoryRepo.GetAll();
            cmbCategory.DataSource =categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryId";
        }

        private void FillSinger()
        {
            var singers=singerRepo.GetAll();
            cmbSinger.DataSource = singers;
            cmbSinger.DisplayMember = "SingerName";
            cmbSinger.ValueMember ="SingerId";
        }

        private void FillData()
        {
            int albumId = Convert.ToInt32(this.Tag);
            if (albumId > 0)
            {
                var album = albumRepo.Get(albumId);
                if (album != null)
                {
                    selectedAlbum = album;
                    txtAlbumName.Text = album.AlbumName;
                    cmbCategory.SelectedValue = album.CategoryId;
                    cmbSinger.SelectedValue = album.SingerId;
                    nuUnitPrice.Value=album.UnitPrice;
                    nuDiscount.Value = album.Discount;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FormSave();
            this.Close();
        }

        private void FormSave()
        {
            if (selectedAlbum == null)
            {
                selectedAlbum = new Album();
            }
            selectedAlbum.AlbumName = txtAlbumName.Text;
            selectedAlbum.CategoryId = Convert.ToInt32(cmbCategory.SelectedValue);
            selectedAlbum.SingerId = Convert.ToInt32(cmbSinger.SelectedValue);
            selectedAlbum.UnitPrice=nuUnitPrice.Value;
            selectedAlbum.Discount=nuDiscount.Value;
            selectedAlbum.AlbumAddDate= DateTime.Now;

            if (Convert.ToInt32(this.Tag) > 0)
            {
                //Update
                albumRepo.Update(selectedAlbum);

            }
            else
            {
                //Insert
                selectedAlbum.AlbumId = albumRepo.Create(selectedAlbum);
                this.Tag = selectedAlbum.AlbumId;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedAlbum != null)
            {
                albumRepo.Delete(selectedAlbum.AlbumId);
                this.Close();
            }
        }
    }
}
