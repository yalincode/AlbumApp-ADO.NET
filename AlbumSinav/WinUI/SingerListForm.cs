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
    using DAL.Repository;
    using DAL.Entities;
    public partial class SingerListForm : Form
    {
        SingerRepo singerRepo;
        public SingerListForm()
        {
            InitializeComponent();
            singerRepo = new SingerRepo();
        }

        private void SingerListForm_Load(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void FillGrid()
        {
            grid.DataSource = null;
            grid.DataSource=singerRepo.GetAll();
        }

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            var singer=(grid.DataSource as List<Singer>)[e.RowIndex];

            SingerSaveForm form=new SingerSaveForm();
            form.Tag = singer.SingerId;
            form.ShowDialog();
            FillGrid();
        }
    }
}
