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
    public partial class OrderSaveForm : Form
    {
        public int selectedUser { get; set; }

        AlbumRepo albumRepo;
        UserRepo userRepo;
        OrderRepo orderRepo;
        OrderDetailRepo orderDetailRepo;

        public OrderSaveForm()
        {
            InitializeComponent();
            albumRepo = new AlbumRepo();
            userRepo = new UserRepo();
            orderRepo=new OrderRepo();
            orderDetailRepo=new OrderDetailRepo();
        }

        private void OrderSaveForm_Load(object sender, EventArgs e)
        {
            FillControl();
            FillData();
            
        }

        private void FillControl()
        {
            var albums=albumRepo.GetAll();
            cmbAlbum.DataSource = albums;
            cmbAlbum.DisplayMember = "AlbumName";
            cmbAlbum.ValueMember = "AlbumId";
        }
        Order selectedOrder;
        private void FillData()
        {
            txtUser.Text = userRepo.Get(this.selectedUser).UserName;

            int orderıd = Convert.ToInt32(this.Tag);
            if (orderıd > 0)
            {
                var order = orderRepo.Get(orderıd);
                if (order != null)
                {
                    selectedOrder = order;
                    txtCountry.Text = order.Country;
                    txtCity.Text = order.City;
                    txtCounty.Text = order.County;
                    dtOrderDate.Value = order.OrderDate;
                    
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FormSave();
        }

        private void FormSave()
        {
            if (selectedOrder == null)
            {
                selectedOrder = new Order();
            }
            
            selectedOrder.UserId = this.selectedUser;
            selectedOrder.Country = txtCountry.Text;
            selectedOrder.City = txtCity.Text;
            selectedOrder.County = txtCountry.Text;
            selectedOrder.OrderDate = dtOrderDate.Value;
            

            if (Convert.ToInt32(this.Tag) > 0)
            {
                //Update
                orderRepo.Update(selectedOrder);

            }
            else
            {
                //Insert
                selectedOrder.OrderId = orderRepo.Create(selectedOrder);
                this.Tag = selectedOrder.OrderId;
            }
            MessageBox.Show("İşlem Başarıylar Gerçekleşti");
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (selectedOrder != null)
            {
                orderRepo.Delete(selectedOrder.OrderId);
                this.Close();
            }
        }

        OrderDetail selectedOrderDetail;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool isAdded = false;

            if (selectedOrderDetail==null)
            {
                selectedOrderDetail = new OrderDetail();
                isAdded = true;
            }
            if (selectedOrder == null)
            {
                throw new Exception("Lütfen Önce Sipariş Kaydı Oluşturun.");

            }
            selectedOrderDetail.OrderId= selectedOrder.OrderId;
            selectedOrderDetail.AlbumId = Convert.ToInt32(cmbAlbum.SelectedValue);
            selectedOrderDetail.UnitPrice = nuUnitPrice.Value;
            selectedOrderDetail.Discount = nuDiscount.Value;
            selectedOrderDetail.Quantity= (int)nuQuantity.Value;
            if (isAdded)
            {
                //Insert
                orderDetailRepo.Create(selectedOrderDetail);

            }
            else
            {
                //Update
                orderDetailRepo.Update(selectedOrderDetail);
            }
            selectedOrderDetail=null;
            FormClear(panel4);
            FillDataOrderDetail(selectedOrder.OrderId);
            
        }

        private void FillDataOrderDetail(int id)
        {
            if (id > 0)
            {
                var orderDetail = orderDetailRepo.GetAll(id);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = orderDetail;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var orderDetail=(dataGridView1.DataSource as List<OrderDetail>)[e.RowIndex];
            if (orderDetail != null)
            {
                cmbAlbum.SelectedValue=orderDetail.AlbumId;
                nuDiscount.Value=orderDetail.Discount;  
                nuQuantity.Value=orderDetail.Quantity;
                nuUnitPrice.Value=orderDetail.UnitPrice;    
                selectedOrderDetail=new OrderDetail()
                {
                    OrderId=orderDetail.OrderId,
                    Discount = orderDetail.Discount,
                    AlbumId = orderDetail.AlbumId,
                    Quantity = orderDetail.Quantity,
                    UnitPrice = orderDetail.UnitPrice
                };
            }
        }

        public void FormClear(Control controls = null)
        {
            foreach (var item in controls.Controls)
            {
                if (item is TextBox)
                {
                    (item as TextBox).Clear();
                }
                else if (item is MaskedTextBox)
                {
                    (item as MaskedTextBox).Clear();
                }
                else if (item is Panel)
                {
                    FormClear(item as Panel);
                }
                else if (item is NumericUpDown)
                {
                    (item as NumericUpDown).Value = 0;
                }
                else if (item is ComboBox)
                {
                    ((ComboBox)item).SelectedIndex = -1;
                }
                else if (item is CheckBox)
                {
                    ((CheckBox)item).Checked = false;
                }
                else if (item is GroupBox)
                {
                    FormClear(item as GroupBox);
                }
                else if (item is DateTimePicker)
                {
                    (item as DateTimePicker).Value = DateTime.Now;
                }

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedOrderDetail != null)
            {
                orderDetailRepo.Delete(selectedOrderDetail.OrderDetailId);
                selectedOrderDetail = null;
                FormClear(panel4);
            }
        }
    }
}
