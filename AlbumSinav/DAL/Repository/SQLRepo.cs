using AlbumSinav.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlbumSinav.DAL.Repository
{
    public class SQLRepo:BaseRepo
    {
        public void ConClose()
        {
            if (con.State == System.Data.ConnectionState.Open) con.Close();
        }

        public void ConOpen()
        {
            if (con.State == System.Data.ConnectionState.Closed) con.Open();
        }
        public DataTable top10AlbumSonEklenen()
        {
            SqlDataAdapter dap = new SqlDataAdapter("SELECT TOP 10 *FROM Albums order BY AlbumAddDate DESC", con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            return dt;
        }

        public DataTable indirimdeki15Album()
        {
            SqlDataAdapter dap = new SqlDataAdapter("SELECT TOP 10 * FROM Albums order BY Discount DESC", con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            return dt;
        }

        public DataTable secilenTureGoreAlbum(ComboBox combo)
        {
            SqlDataAdapter dap = new SqlDataAdapter("SELECT * FROM Albums WHERE CategoryId=@k", con);
            dap.SelectCommand.Parameters.AddWithValue("@k", (int)combo.SelectedValue);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            return dt;
        }

        public DataTable secilenSanatcıyaGoreAlbum(ComboBox combo)
        {
            SqlDataAdapter dap = new SqlDataAdapter("SELECT * FROM Albums WHERE SingerId=@k", con);
            dap.SelectCommand.Parameters.AddWithValue("@k", (int)combo.SelectedValue);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            return dt;
        }

        public DataTable encoksatinalinmisAlbums()
        {
            SqlDataAdapter dap = new SqlDataAdapter("select od.AlbumId,AlbumName,Sum(Quantity) as ToplamAdet FROM OrderDetails as od INNER JOIN Albums as a ON od.AlbumId = a.AlbumId GROUP BY od.AlbumId, AlbumName ORDER BY Sum(Quantity) DESC", con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            return dt;
        }
        public DataTable encoksatanSanatcı()
        {
            SqlDataAdapter dap = new SqlDataAdapter("select s.SingerId,SingerName,Sum(Quantity) as ToplamAdet FROM OrderDetails as od INNER JOIN Albums as a ON od.AlbumId = a.AlbumId INNER JOIN Singers AS s on s.SingerId = a.SingerId GROUP BY s.SingerId, SingerName ORDER BY Sum(Quantity) DESC", con);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            return dt;
        }
    }
}
