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
    public class CategoryRepo : BaseRepo, IRepository<Category>
    {
        public void ConClose()
        {
            if (con.State == System.Data.ConnectionState.Open) con.Close();
        }

        public void ConOpen()
        {
            if (con.State == System.Data.ConnectionState.Closed) con.Open();
        }

        public int Create(Category entity)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Category_InsertUpdate", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CategoryName", entity.CategoryName);
                command.Parameters.AddWithValue("@CategoryDescription", entity.CategoryDescription);

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
                    SqlCommand command = new SqlCommand("Sp_Category_Delete", con);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CategoryId", id);
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

        public Category Get(int id)
        {
            Category category = new Category();
            try
            {
                SqlCommand command = new SqlCommand("Sp_Category_Getir", this.con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CategoryId", id);
                ConOpen();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    category = (Mapping(reader));
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
            return category;
        }

        public List<Category> GetAll()
        {
            List<Category> categories = new List<Category>();
            try
            {
                SqlCommand command = new SqlCommand("Sp_Category_Getir", this.con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                ConOpen();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    categories.Add(Mapping(reader));
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
            return categories;
        }

        public Category Mapping(SqlDataReader reader)
        {
            Category category = new Category();
            category.CategoryId = Convert.ToInt32(reader["CategoryId"]);
            category.CategoryName = reader["CategoryName"].ToString();
            category.CategoryDescription = reader["CategoryDescription"].ToString();
            return category;
        }

        public int Update(Category entity)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_Category_InsertUpdate", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
                command.Parameters.AddWithValue("@CategoryName", entity.CategoryName);
                command.Parameters.AddWithValue("@CategoryDescription", entity.CategoryDescription);

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
