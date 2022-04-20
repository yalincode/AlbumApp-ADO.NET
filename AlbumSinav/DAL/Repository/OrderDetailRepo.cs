using AlbumSinav.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlbumSinav.DAL.Repository
{
    public class OrderDetailRepo : BaseRepo 
    {
        public void ConClose()
        {
            if (con.State == System.Data.ConnectionState.Open) con.Close();
        }

        public void ConOpen()
        {
            if (con.State == System.Data.ConnectionState.Closed) con.Open();
        }

        public int Create(OrderDetail entity)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_OrderDetail_InsertUpdate", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OrderId", entity.OrderId);
                command.Parameters.AddWithValue("@AlbumId", entity.AlbumId);
                command.Parameters.AddWithValue("@Quantity", entity.Quantity);
                command.Parameters.AddWithValue("@UnitPrice", entity.UnitPrice);
                command.Parameters.AddWithValue("@Discount", entity.Discount);
                

                ConOpen();
                id = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                ConClose();
            }
            return id;
        }

        public void Delete(int OrderDetailId)
        {
            if (MessageBox.Show("Silmek İstediğine Emin misin?", "Silme İşlemi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    SqlCommand command = new SqlCommand("Sp_OrderDetail_Delete", con);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrderDetailId", OrderDetailId);
                    ConOpen();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                finally
                {
                    ConClose();
                }
            }
        }

        public OrderDetail Get(int OrderId, int AlbumId)
        {
            OrderDetail orderDetail = new OrderDetail();
            try
            {
                SqlCommand command = new SqlCommand("Sp_OrderDetail_Getir", this.con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OrderId", OrderId);
                command.Parameters.AddWithValue("@AlbumId", AlbumId);
                ConOpen();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    orderDetail = (Mapping(reader));
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                ConClose();
            }
            return orderDetail;
        }

        public List<OrderDetail> GetAll(int OrderId)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            try
            {
                SqlCommand command = new SqlCommand("Sp_OrderDetail_Getir", this.con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OrderId", OrderId);
                ConOpen();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    orderDetails.Add(Mapping(reader));
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                ConClose();
            }
            return orderDetails;
        }

        public OrderDetail Mapping(SqlDataReader reader)
        {
            OrderDetail orderDetail = new OrderDetail();
            orderDetail.OrderDetailId= Convert.ToInt32(reader["OrderDetailId"]);
            orderDetail.OrderId = Convert.ToInt32(reader["OrderId"]);
            orderDetail.AlbumId = Convert.ToInt32(reader["AlbumId"]);
            orderDetail.UnitPrice = (decimal)reader["UnitPrice"];
            orderDetail.Discount = (decimal)reader["Discount"];
            orderDetail.Quantity = (int)reader["Quantity"];
            return orderDetail;
        }

        public int Update(OrderDetail entity)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_OrderDetail_InsertUpdate", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OrderDetailId", entity.OrderDetailId);
                command.Parameters.AddWithValue("@OrderId", entity.OrderId);
                command.Parameters.AddWithValue("@AlbumId", entity.AlbumId);
                command.Parameters.AddWithValue("@Quantity", entity.Quantity);
                command.Parameters.AddWithValue("@UnitPrice", entity.UnitPrice);
                command.Parameters.AddWithValue("@Discount", entity.Discount);


                ConOpen();
                id = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                ConClose();
            }
            return id;
        }
    }
}
