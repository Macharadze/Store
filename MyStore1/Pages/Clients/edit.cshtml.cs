using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Mystore.Pages.Clients;

namespace MyStore1.Pages.Clients
{
    public class editModel : PageModel
    {
        public ClientInfo ClientInfo = new ClientInfo();
        public string errorMassage = "";
        public string succ = "";

        public void OnGet()
        {
            string id = Request.Query["id"];
            try
            {
                string connection = "Data Source=.\\sqlexpress;Initial Catalog=store;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection1 = new SqlConnection(connection))
                {
                    connection1.Open();
                    string sql = "SELECT * FROM clients WHERE id=@id";
                    using (SqlCommand command1 = new SqlCommand(sql, connection1))
                    {
                        command1.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command1.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                             
                                ClientInfo.id = "" + reader.GetInt32(0);
                                ClientInfo.name = reader.GetString(1);
                                ClientInfo.email = reader.GetString(2);
                                ClientInfo.phone = reader.GetString(3);
                                ClientInfo.address = reader.GetString(4);

         
                            }
                        }
                    }
                    }

                }
            catch (Exception ex)
            {
                errorMassage = ex.Message;
            }
        }
        public void OnPost()
        {
           ClientInfo.id = Request.Form["id"];
            ClientInfo.name = Request.Form["name"];
            ClientInfo.email = Request.Form["email"];
            ClientInfo.phone = Request.Form["phone"];
            ClientInfo.address = Request.Form["address"];
            if (ClientInfo.name.Length == 0 || ClientInfo.email.Length == 0
              )
            {
                errorMassage = "all the are required";
                return;
            }
            try
            {
                string connection = "Data Source=.\\sqlexpress;Initial Catalog=store;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection1 = new SqlConnection(connection))
                {
                    connection1.Open();
                    string sql = "UPDATE clients " +
                        "SET name=@name, email=@email,phone=@phone,address=@address " +
                        "WHERE  id=@id";
                    using(SqlCommand sqlCommand = new SqlCommand(sql, connection1))
                    {
                        sqlCommand.Parameters.AddWithValue("@name", ClientInfo.name);
                        sqlCommand.Parameters.AddWithValue("@email", ClientInfo.email);
                        sqlCommand.Parameters.AddWithValue("@phone", ClientInfo.phone);
                        sqlCommand.Parameters.AddWithValue("@address", ClientInfo.address);
                      sqlCommand.Parameters.AddWithValue("@id", ClientInfo.id);
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMassage = ex.Message;
                return;
            }
            Response.Redirect("/Clients/Index");
        }
    }
}
