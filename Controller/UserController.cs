using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Backend.Metodi_statici;
using Backend.Models;
using Newtonsoft.Json;
using System.Web.Http.Cors;

namespace Backend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        // GET: api/User
        public string Get(int id)
        {
            User user = new User();
            int aux = 0;
            String jstring = "";
            MySqlCommand cmd = DBMethod.Connect_database();
            cmd.CommandText = "select * from User where IDuser='" + id + "';";
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                user.name = reader.GetString("name");
                user.surname = reader.GetString("surname");
                user.email = reader.GetString("email");
                Int32.TryParse(reader.GetString("idCity"), out aux);
                user.idCity = aux;
                user.address = reader.GetString("Address");
                user.tel = reader.GetString("tel");
                user.user_image = reader.GetString("user_image");

                //Adesso dobbiamo serializzare in json l'oggetto user
                jstring=JsonConvert.SerializeObject(user);


            }
               
            
            return jstring;
        }

        
        // POST: api/User
        public void Post([FromBody]string value)
        {
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
