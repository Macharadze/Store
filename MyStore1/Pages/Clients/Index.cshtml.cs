using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Mystore.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClien = new List<ClientInfo>();
        public void OnGet()
        {
            string connection = "Data Source=.\\sqlexpress;Initial Catalog=store;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            try
            {
                using (SqlConnection connection1 = new SqlConnection(connection))
                {
                    connection1.Open();
                    string sql = "select * from clients";
                    using (SqlCommand command1 = new SqlCommand(sql, connection1))
                    {
                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5).ToString();

                                listClien.Add(clientInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.ToString());
            }

        }
    }
    public class ClientInfo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string created_at;
    }
}