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
    public partial class CategorySaveForm : Form
    {
        CategoryRepo categoryRepo;
        public CategorySaveForm()
        {
            InitializeComponent();
            categoryRepo = new CategoryRepo();
        }

        private void CategorySaveForm_Load(object sender, EventArgs e)
        {
            FillData();
        }
        Category selectedCategory;
        private void FillData()
        {
            int categoryId = Convert.ToInt32(this.Tag);
            if (categoryId > 0)
            {
                var category = categoryRepo.Get(categoryId);
                if (category != null)
                {
                    selectedCategory = category;
                    txtCategoryName.Text = category.CategoryName;
                    txtCategoryDes.Text = category.CategoryDescription;
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
            if (selectedCategory == null)
            {
                selectedCategory = new Category();
            }
            selectedCategory.CategoryName = txtCategoryName.Text;
            selectedCategory.CategoryDescription = txtCategoryDes.Text;

            if (Convert.ToInt32(this.Tag) > 0)
            {
                //Update
                categoryRepo.Update(selectedCategory);

            }
            else
            {
                //Insert
                selectedCategory.CategoryId = categoryRepo.Create(selectedCategory);
                this.Tag = selectedCategory.CategoryId;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedCategory != null)
            {
                categoryRepo.Delete(selectedCategory.CategoryId);
                this.Close();
            }
        }
    }
}
