using FriedChicken.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace FriedChicken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private static string sqlDataSource;

        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlDataSource = _configuration.GetConnectionString("FriedChickenAppCon");
        }

        [HttpGet]
        public JsonResult GetLogin(admin ad)
        {
            string sql = "Select * from username = @username where password = @password";
            
            string v_bool = "true";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", ad.username);
                        cmd.Parameters.AddWithValue("@password", ad.password);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                v_bool = "true";
                            }
                            else
                            {
                                v_bool = "false";
                            }
                        }
                    }
                }                
            } catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            return new JsonResult(v_bool);
        }
    }
}
