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
    public partial class AlbumListForm : Form
    {
        AlbumRepo albumRepo;
        public AlbumListForm()
        {
            InitializeComponent();
            albumRepo = new AlbumRepo();
        }

        private void AlbumListForm_Load(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void FillGrid()
        {
            grid.DataSource = null;
            grid.DataSource = albumRepo.GetAll();
        }

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            var album = (grid.DataSource as List<Album>)[e.RowIndex];

            AlbumSaveForm form = new AlbumSaveForm();
            form.Tag = album.CategoryId;
            form.ShowDialog();
            FillGrid();
        }
    }
}
