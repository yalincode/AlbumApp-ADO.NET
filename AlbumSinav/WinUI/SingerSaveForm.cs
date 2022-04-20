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
    public partial class SingerSaveForm : Form
    {
        SingerRepo singerRepo;
        public SingerSaveForm()
        {
            InitializeComponent();
            singerRepo = new SingerRepo();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            FormSave();
            this.Close();
        }

        Singer selectedSinger=null;
        private void FormSave()
        {
            if (selectedSinger==null)
            {
                selectedSinger = new Singer();
            }
            selectedSinger.SingerName = txtSingerName.Text;
            selectedSinger.SingerDescription=txtSingerDes.Text;

            if (Convert.ToInt32(this.Tag)>0)
            {
                //Update
                singerRepo.Update(selectedSinger);

            }
            else
            {
                //Insert
                selectedSinger.SingerId= singerRepo.Create(selectedSinger);
                this.Tag = selectedSinger.SingerId;
            }
        }

        private void SingerSaveForm_Load(object sender, EventArgs e)
        {
            FillData();
        }

        private void FillData()
        {
            int singerId = Convert.ToInt32(this.Tag);
            if (singerId>0)
            {
                var singer = singerRepo.Get(singerId);
                if (singer != null)
                {
                    selectedSinger=singer;
                    txtSingerName.Text=singer.SingerName;
                    txtSingerDes.Text = singer.SingerDescription;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedSinger!=null)
            {
                singerRepo.Delete(selectedSinger.SingerId);
                this.Close();
            }
        }
    }
}
