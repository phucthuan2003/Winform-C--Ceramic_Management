using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Ceramic_Management
{
    public class ProcessDataBase
    {
        string strConnect = @"Data Source=DESKTOP-R4RPQKD;Initial Catalog=BTL_6;Integrated Security=True";
        SqlConnection sqlConnect = null;
        // Hàm mở kết nối CSDL
        private void KetNoiCSDL()
        {
            sqlConnect = new SqlConnection(strConnect);
            if (sqlConnect.State != ConnectionState.Open) 
                sqlConnect.Open();
        }

        // Hàm đóng kết nối CSDL
        private void DongKetNoiCSDL()
        {
            if(sqlConnect.State != ConnectionState.Closed)
            {
                sqlConnect.Close();
            }
            sqlConnect.Dispose();
        }

        // Hàm thực thi câu lệnh dạng Select trả về một DataTable

        public DataTable DocBang(string sql)
        {
            DataTable dtBang = new DataTable();
            KetNoiCSDL();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql , sqlConnect);
            sqlDataAdapter.Fill(dtBang);
            DongKetNoiCSDL();
            return dtBang;
        }

        // Hàm thực hiện lệnh insert hoặc update hoặc delete
        public void CapNhatDuLieu(string sql) 
        {
            KetNoiCSDL();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnect; 
            sqlCommand.CommandText = sql;
            sqlCommand.ExecuteNonQuery();
            DongKetNoiCSDL();
        }
    }
}
