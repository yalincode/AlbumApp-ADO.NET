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
    public class AlbumRepo : BaseRepo, IRepository<Album>
    {
        public void ConClose()
        {
            if (con.State == System.Data.ConnectionState.Open) con.Close();
        }

        public void ConOpen()
        {
            if (con.State == System.Data.ConnectionState.Closed) con.Open();
        }

        public int Create(Album entity)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Album_InsertUpdate", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AlbumName", entity.AlbumName);
                command.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
                command.Parameters.AddWithValue("@SingerId", entity.SingerId);
                command.Parameters.AddWithValue("@UnitPrice", entity.UnitPrice);
                command.Parameters.AddWithValue("@Discount", entity.Discount);
                command.Parameters.AddWithValue("@AlbumAddDate", entity.AlbumAddDate);

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
                    SqlCommand command = new SqlCommand("Sp_Album_Delete", con);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AlbumId", id);
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

        public Album Get(int id)
        {
            Album album = new Album();
            try
            {
                SqlCommand command = new SqlCommand("Sp_Album_Getir", this.con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AlbumId", id);
                ConOpen();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    album = (Mapping(reader));
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
            return album;
        }

        public List<Album> GetAll()
        {
            List<Album> albums = new List<Album>();
            try
            {
                SqlCommand command = new SqlCommand("Sp_Album_Getir", this.con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                ConOpen();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    albums.Add(Mapping(reader));
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
            return albums;
        }

        public Album Mapping(SqlDataReader reader)
        {
            Album album = new Album();
            album.AlbumId = Convert.ToInt32(reader["AlbumId"]);
            album.AlbumName = reader["AlbumName"].ToString();
            album.SingerId = Convert.ToInt32(reader["SingerId"]);
            album.CategoryId= Convert.ToInt32(reader["CategoryId"]);
            album.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);
            album.Discount = Convert.ToDecimal(reader["Discount"]);
            album.AlbumAddDate = Convert.ToDateTime(reader["AlbumAddDate"]);
            return album;
        }

        public int Update(Album entity)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Album_InsertUpdate", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AlbumId", entity.AlbumId);
                command.Parameters.AddWithValue("@AlbumName", entity.AlbumName);
                command.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
                command.Parameters.AddWithValue("@SingerId", entity.SingerId);
                command.Parameters.AddWithValue("@UnitPrice", entity.UnitPrice);
                command.Parameters.AddWithValue("@Discount", entity.Discount);
                command.Parameters.AddWithValue("@AlbumAddDate", entity.AlbumAddDate);

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
