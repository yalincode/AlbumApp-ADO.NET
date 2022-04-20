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
    public partial class CategoryListForm : Form
    {
        CategoryRepo categoryRepo;
        public CategoryListForm()
        {
            InitializeComponent();
            categoryRepo = new CategoryRepo();
        }

        private void CategoryListForm_Load(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void FillGrid()
        {
            grid.DataSource = null;
            grid.DataSource = categoryRepo.GetAll();
        }

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            var category = (grid.DataSource as List<Category>)[e.RowIndex];

            CategorySaveForm form = new CategorySaveForm();
            form.Tag = category.CategoryId;
            form.ShowDialog();
            FillGrid();
        }
    }
}
