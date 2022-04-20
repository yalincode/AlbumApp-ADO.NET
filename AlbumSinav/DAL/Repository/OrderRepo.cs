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
    public class OrderRepo : BaseRepo, IRepository<Order>
    {
        public void ConClose()
        {
            if (con.State == System.Data.ConnectionState.Open) con.Close();
        }

        public void ConOpen()
        {
            if (con.State == System.Data.ConnectionState.Closed) con.Open();
        }

        public int Create(Order entity)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Order_InsertUpdate", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OrderDate", entity.OrderDate);
                command.Parameters.AddWithValue("@Country", entity.Country);
                command.Parameters.AddWithValue("@City", entity.City);
                command.Parameters.AddWithValue("@County", entity.County);
                command.Parameters.AddWithValue("@UserId", entity.UserId);

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

        public void Delete(int id)
        {
            if (MessageBox.Show("Silmek İstediğine Emin misin?", "Silme İşlemi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    SqlCommand command = new SqlCommand("Sp_Order_Delete", con);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrderId", id);
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

        public Order Get(int id)
        {
            Order order = new Order();
            try
            {
                SqlCommand command = new SqlCommand("Sp_Order_Getir", this.con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OrderId", id);
                ConOpen();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    order = (Mapping(reader));
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
            return order;
        }

        public List<Order> GetAll()
        {
            List<Order> orders = new List<Order>();
            try
            {
                SqlCommand command = new SqlCommand("Sp_Order_Getir", this.con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                ConOpen();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    orders.Add(Mapping(reader));
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
            return orders;
        }

        public Order Mapping(SqlDataReader reader)
        {
            Order order = new Order();
            order.OrderId = Convert.ToInt32(reader["OrderId"]);
            order.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
            order.Country = (reader["Country"]).ToString();
            order.City = (reader["City"]).ToString();
            order.County = (reader["County"]).ToString();
            order.UserId= Convert.ToInt32(reader["UserId"]);
            
            return order;
        }

        public int Update(Order entity)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Order_InsertUpdate", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OrderId", entity.OrderId);
                command.Parameters.AddWithValue("@OrderDate", entity.OrderDate);
                command.Parameters.AddWithValue("@Country", entity.Country);
                command.Parameters.AddWithValue("@City", entity.City);
                command.Parameters.AddWithValue("@County", entity.County);
                command.Parameters.AddWithValue("@UserId", entity.UserId);

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
