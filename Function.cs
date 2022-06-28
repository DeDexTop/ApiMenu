using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MenuAPI
{
    public class Function
    {
        public static string url = @"Data Source=localhost;Initial Catalog=resto_android;Integrated Security=True";
        SqlConnection con = new SqlConnection(url);
        public DataRowCollection GetData(string query)
        {
            SqlDataAdapter sda = new SqlDataAdapter(query, url);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt.Rows;
        }

        public string GenerateToken()
        {
            byte xorConstant = 0x53;
            
            byte[] data = Encoding.UTF8.GetBytes(DateTime.Now.Millisecond.ToString());
            for(int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(data[i] ^ xorConstant);
            }
            string result = Convert.ToBase64String(data)[..4];
            return result.Trim();
        }
    }
}
