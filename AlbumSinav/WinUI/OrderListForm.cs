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
    public partial class OrderListForm : Form
    {
        OrderRepo orderRepo;
        public int selectedUser { get; set; }
        public OrderListForm()
        {
            InitializeComponent();
            orderRepo = new OrderRepo();
        }

        private void OrderListForm_Load(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void FillGrid()
        {
            grid.DataSource = null;
            grid.DataSource = orderRepo.GetAll();
        }

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            var orders = (grid.DataSource as List<Order>)[e.RowIndex];

            OrderSaveForm form = new OrderSaveForm();
            form.Tag = orders.OrderId;
            form.selectedUser=this.selectedUser;
            form.ShowDialog();
            FillGrid();
        }
    }
}
