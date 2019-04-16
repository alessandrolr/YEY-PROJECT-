using Backend.Metodi_statici;
using Backend.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;



namespace Backend.Controllers
{
    
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        

        
        // GET: api/User
        public int Get(String username, String password)
        {

            int id = 0;
            string aux;
            MySqlCommand cmd = DBMethod.Connect_database();
            cmd.CommandText = "select * from account where username='" + username + "' and password='" + password + "';";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                aux = reader.GetString("IdAccount");
                Int32.TryParse(aux, out id);
                
                
            }

            if (id != 0)
            {
                return id;
                
            }

            return id;
        }

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }

        
        // POST: api/User
        public String Post([FromBody]User user)
        {
            String id = "";
            MySqlCommand cmd = DBMethod.Connect_database();

            try
            {

                cmd.CommandText = "Start Transaction;";
                cmd.ExecuteNonQuery();

                //Inserisco un account
                cmd.CommandText = "INSERT INTO account (IDAccount, Username, Password) VALUES(null,'" + user.username + "', '" + user.password + "');";
                cmd.ExecuteNonQuery();

                //Seleziono l'id dell'account appena inserito.
                cmd.CommandText = "select * from account where username='" + user.username + "';";
                cmd.ExecuteNonQuery();

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetString("IDAccount");
                }
                reader.Close();
                //Inserisco l'user
                cmd.CommandText = "Insert into User (IDUser,Name,Surname, Email, IDCity,Address,Tel,user_image) values('" + id + "','" + user.name + "','" + user.surname + "','" + user.email + "','1','" + user.address + "','" + user.tel + "','" + user.image + "');";
                cmd.ExecuteNonQuery();

                //Faccio l'upload dell'immagine
                //DBMethod.UploadFile(user.image);

                //Confermo la transaction
                cmd.CommandText = "Commit work;";
                cmd.ExecuteNonQuery();
                return "ok";
                
            }
            catch (Exception)
            {
                return "Bad Registration";
            }

        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
