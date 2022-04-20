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
    public class SingerRepo : BaseRepo, IRepository<Singer>
    {
        public void ConClose()
        {
            if (con.State == System.Data.ConnectionState.Open) con.Close();
        }

        public void ConOpen()
        {
            if (con.State == System.Data.ConnectionState.Closed) con.Open();
        }

        public int Create(Singer entity)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Singer_InsertUpdate", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SingerName", entity.SingerName);
                command.Parameters.AddWithValue("@SingerDescription", entity.SingerDescription);
                
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
                    SqlCommand command = new SqlCommand("Sp_Singer_Delete", con);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SingerId", id);
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

        public Singer Get(int id)
        {
            Singer singer=new Singer();
            try
            {
                SqlCommand command = new SqlCommand("Sp_Singer_Getir", this.con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SingerId", id);
                ConOpen();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    singer=(Mapping(reader));
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
            return singer;
        }

        public List<Singer> GetAll()
        {
            List<Singer> singers = new List<Singer>();
            try
            {
                SqlCommand command = new SqlCommand("Sp_Singer_Getir", this.con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                ConOpen();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    singers.Add(Mapping(reader));
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
            return singers;
        }

        public Singer Mapping(SqlDataReader reader)
        {
            Singer singer = new Singer();
            singer.SingerId= Convert.ToInt32(reader["SingerId"]);
            singer.SingerName = reader["SingerName"].ToString();
            singer.SingerDescription = reader["SingerDescription"].ToString();
            return singer;
        }

        public int Update(Singer entity)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Singer_InsertUpdate", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SingerId", entity.SingerId);
                command.Parameters.AddWithValue("@SingerName", entity.SingerName);
                command.Parameters.AddWithValue("@SingerDescription", entity.SingerDescription);

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
