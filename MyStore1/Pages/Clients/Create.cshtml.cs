using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Mystore.Pages.Clients;

namespace MyStore1.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo ClientInfo= new ClientInfo();
        public string errorMassage = "";
        public string succ = "";
        public void OnGet()
        {
        }
        public void OnPost() { 
          ClientInfo.name = Request.Form["name"];
          ClientInfo.email = Request.Form["email"];
            ClientInfo.phone = Request.Form["phone"];
            ClientInfo.address = Request.Form["address"];
            if(ClientInfo.name.Length == 0 || ClientInfo.email.Length == 0
               )
            {
                errorMassage = "all the are required";
                return;
            }
            try
            {
                string connection = "Data Source=.\\sqlexpress;Initial Catalog=store;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using(SqlConnection connection1 = new SqlConnection(connection))
                {
                    connection1.Open();
                    string sql = "INSERT INTO clients " +
                        "(name,email,phone,address) VALUES "+
                        "(@name,@email,@phone,@address);";
                using(SqlCommand sqlCommand = new SqlCommand(sql,connection1)) {
                        sqlCommand.Parameters.AddWithValue("@name", ClientInfo.name);
                        sqlCommand.Parameters.AddWithValue("@email", ClientInfo.email);
                        sqlCommand.Parameters.AddWithValue("@phone", ClientInfo.phone);
                        sqlCommand.Parameters.AddWithValue("@address", ClientInfo.address);
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }catch(Exception e)
            {
                errorMassage=e.Message;
                return;
            }







            ClientInfo.name = "";
            ClientInfo.email = "";
            ClientInfo.phone = "";
            ClientInfo.address = "";
            succ = " new client added";
        }
    }
}
