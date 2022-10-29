using FriedChicken.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MySql.Data.MySqlClient;
using System.Data;

namespace FriedChicken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanLoaiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private static string sqlDataSource;

        public PhanLoaiController(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlDataSource = _configuration.GetConnectionString("FriedChickenAppCon");
        }

        [HttpGet]
        public JsonResult Get()
        {
            string sql = "Select * from phanloai";
            DataTable dataTable = new DataTable();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            dataTable.Load(reader);
                            reader.Close();
                        }
                    }
                    conn.Close();
                }
            } catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            return new JsonResult(dataTable);
        }

        [HttpPost("Insert")]
        public JsonResult Post(PhanLoai pl)
        {
            string sql = @"Insert into phanloai(maPL, tenPL, anhPhanLoai) values(@maPL, @tenPL, @anhPhanLoai)";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maPL", pl.maPL);
                        cmd.Parameters.AddWithValue("@tenPL", pl.tenPL);
                        cmd.Parameters.AddWithValue("@anhPhanLoai", pl.anhPhanLoai);
                        cmd.ExecuteReader();

                    }
                    conn.Close();
                }
            } catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            return new JsonResult("Insert thành công");
        }

        [HttpPost("Upload"), DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = file.FileName.Trim();
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                return BadRequest();
            } catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPut]
        public JsonResult Put(PhanLoai pl)
        {
            string sql = @"Update phanLoai set tenPL = @tenPL, anhPhanLoai = @anhPhanLoai where maPL = @maPL";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@tenPL", pl.tenPL);
                        cmd.Parameters.AddWithValue("@anhPhanLoai", pl.anhPhanLoai);
                        cmd.Parameters.AddWithValue("@maPL", pl.maPL);
                        cmd.ExecuteReader();

                    }
                    conn.Close();
                }
            } catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            return new JsonResult("Update thành công!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string sql = @"Delete from phanloai where maPL = @maPL";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maPL", id);
                        cmd.ExecuteReader();

                    }
                    conn.Close();
                }
            } catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            return new JsonResult("Delete thành công");
        }
    }
}
