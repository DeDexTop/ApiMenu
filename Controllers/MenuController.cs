using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MenuAPI.Controllers
{
    public class MenuController : Controller
    {
        public static string url = @"Data Source=localhost;Initial Catalog=resto_android;Integrated Security=True";
        SqlConnection con = new SqlConnection(url);

        // GET: api/<MenuController>
        [HttpGet]
        [Route("GetData")]
        public object Get()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM MsMenu", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                return Ok(JsonConvert.SerializeObject(dt));
            }
            else
            {
                return BadRequest();
            }
        }

        // GET api/<MenuController>/5
        [HttpPost]
        [Route("InsertData")]
        [Consumes("application/x-www-form-urlencoded")]
        public object Insert([FromForm] Model.Menu menu)
        {
            con.Open();
            if(menu == null)
            {
                return BadRequest();
            }
            SqlCommand com = new SqlCommand("INSERT INTO MsMenu VALUES ('" + menu.Name + "', '" + menu.Description + "', '" + menu.Price + "')", con);
            com.ExecuteNonQuery();
            return Ok();
        }

        // POST api/<MenuController>
        [HttpPut]      
        [Route("UpdateData")]
        [Consumes("application/x-www-form-urlencoded")]
        public object Update([FromBody] Model.Menu menu)
        {
            con.Open();
            if(menu == null)
            {
                return BadRequest();
            }
            else
            {
                SqlCommand com = new SqlCommand("UPDATE MsMenu SET name = '" + menu.Name + "', description = '" + menu.Description + "', price = '" + menu.Price + "' WHERE id LIKE '" + menu.Id + "'", con);
                com.ExecuteNonQuery();

                return Ok("Data berhasil di ubah");
            }
        }

        // DELETE api/<MenuController>/5
        [HttpDelete]
        [Route("DeleteData")]
        public object Delete([FromForm] int? id)
        {
            con.Open();
            if (id == null)
            {
                return BadRequest();
            }
            else
            {
                SqlCommand com = new SqlCommand("DELETE FROM MsMenu WHERE id = '" + id + "'", con);
                com.ExecuteNonQuery();

                return Ok(com);
            }
        }

        [HttpPost]
        [Route("Login")]
        [Consumes("application/x-www-form-urlencoded")]
        public object Login([FromForm] string name,[FromForm] string password)
        {
            con.Open();
            if (name == null && password == null)
            {
                return BadRequest();
            }
            else
            {
                SqlDataAdapter sda = new SqlDataAdapter("SELECT name, token FROM MsAdmin WHERE name = '" + name + "' AND password = '" + password + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    return Ok(JsonConvert.SerializeObject(dt));    
                }
                else
                {
                    return BadRequest();
                }
            }
        }
        [HttpPost]
        [Route("Register")]
        [Consumes("application/x-www-form-urlencoded")]
        public object Register([FromForm] string name, [FromForm] string password)
        {
            con.Open();
            if (name == null && password == null)
            {
                return BadRequest();
            }
            else
            {
                Function function = new Function();
                SqlCommand com = new SqlCommand("INSERT INTO MsAdmin VALUES ('" + name + "', '" + password + "', @token)", con);
                com.Parameters.AddWithValue("@token", function.GenerateToken());
                com.ExecuteNonQuery();

                return Ok();
            }
        }
    }   
}
