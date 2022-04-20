using AlbumSinav.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumSinav.DAL.Repository
{
    public class UserRepo : BaseRepo, IRepository<User>
    {
        public void ConClose()
        {
            if (con.State == System.Data.ConnectionState.Open) con.Close();
        }

        public void ConOpen()
        {
            if (con.State == System.Data.ConnectionState.Closed) con.Open();
        }

        public int Create(User entity)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_User_InsertUpdate", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserName", entity.UserName);
                command.Parameters.AddWithValue("@UserLastName", entity.UserLastName);
                command.Parameters.AddWithValue("@UserPhone", entity.UserPhone);
                command.Parameters.AddWithValue("@UserEmail", entity.UserEmail);
                command.Parameters.AddWithValue("@UserPassword", entity.UserPassword);
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
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            User user = new User();
            try
            {
                SqlCommand command = new SqlCommand("Sp_User_Getir", this.con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", id);
                ConOpen();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user=(Mapping(reader));
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
            return user;
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            try
            {
                SqlCommand command = new SqlCommand("Sp_User_Getir", this.con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                ConOpen();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(Mapping(reader));
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
            return users;
        }

        public User Mapping(SqlDataReader reader)
        {
            User user = new User();
            user.UserId = Convert.ToInt32(reader["UserId"]);
            user.UserName = reader["UserName"].ToString();
            user.UserLastName = reader["UserLastName"].ToString();
            user.UserPhone = reader["UserPhone"].ToString();
            user.UserEmail = reader["UserEmail"].ToString();
            user.UserPassword = reader["UserPassword"].ToString();
            return user;
        }

        public int Update(User entity)
        {
            int id = 0;
            try
            {
                SqlCommand command = new SqlCommand("Sp_User_InsertUpdate", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", entity.UserId);
                command.Parameters.AddWithValue("@UserName", entity.UserName);
                command.Parameters.AddWithValue("@UserLastName", entity.UserLastName);
                command.Parameters.AddWithValue("@UserPhone", entity.UserPhone);
                command.Parameters.AddWithValue("@UserEmail", entity.UserEmail);
                command.Parameters.AddWithValue("@UserPassword", entity.UserPassword);
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
