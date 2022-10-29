using FriedChicken.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Data;

namespace FriedChicken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private static string sqlDataSource;

        public SanPhamController(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlDataSource = configuration.GetConnectionString("FriedChickenAppCon");
        }


        [HttpGet("/Image/{imageName}")]
        public IActionResult GetImages(string imageName)
        {
            string filePath = "Resources/Images/" + imageName;
            Byte[] image = System.IO.File.ReadAllBytes(filePath);
            return File(image, "image/jpeg");
        }

        [HttpGet]
        public JsonResult Get()
        {
            string sql = "Select * from SanPham;";

            DataTable data = new DataTable();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            data.Load(reader);
                            reader.Close();
                        }
                       
                    }
                    conn.Close();
                }
            } catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            return new JsonResult(data);
        }    
        
        [HttpGet("{page}")]
        public IActionResult GetPage(int page)
        {
            string sql = $"SELECT * FROM SanPham LIMIT 5 OFFSET {page * 5};";

            SanPham[] data = new SanPham[5];
            List<SanPham> list = new List<SanPham>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //sp = new SanPham();
                                //sp.maPL = reader.GetString("maPL");
                                //sp.tenSP = reader.GetString("tenSP");
                                //sp.maSP = reader.GetString("maSP");
                                //sp.soLuong = reader.GetInt32("soLuong");
                                //sp.dongGia = reader.GetInt32("dongia");
                                //sp.chiTiet = reader.GetString("chiTiet");
                                //sp.anhSanPham = reader.GetString("anhSanPham");
                                SanPham sp = new SanPham();
                                sp.maPL = reader.GetString("maPL");
                                sp.tenSP = reader.GetString("tenSP");
                                sp.maSP = reader.GetString("maSP");
                                sp.soLuong = reader.GetInt32("soLuong");
                                sp.dongGia = reader.GetInt32("dongia");
                                sp.chiTiet = reader.GetString("chiTiet");
                                sp.anhSanPham = reader.GetString("anhSanPham");
                                list.Add(sp);
                            }
                            
                            reader.Close();
                        }
                       
                    }
                    conn.Close();
                }
            } catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            return new JsonResult(list);
        }

        [HttpGet("Count")]
        public IActionResult GetCount()
        {
            string sql = $"SELECT count(maSP) as 'so' FROM SanPham;";
            int count = 0;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                count = reader.GetInt16("so");
                            }

                            reader.Close();
                        }

                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            return new JsonResult(count);
        }

        [HttpPost("Insert")]
        public JsonResult Post(SanPham sp)
        {
            string sql = "Insert into SanPham (maSP, tenSP, soLuong, chiTiet, maPL, dongia, anhSanPham) values (@maSP, @tenSP, @soLuong, @chiTiet, @maPL, @dongia, @anhSanPham);";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maSP", sp.maSP);
                        cmd.Parameters.AddWithValue("@tenSP", sp.tenSP);
                        cmd.Parameters.AddWithValue("@soLuong", sp.soLuong);
                        cmd.Parameters.AddWithValue("@chiTiet", sp.chiTiet);
                        cmd.Parameters.AddWithValue("@maPL", sp.maPL);
                        cmd.Parameters.AddWithValue("@dongia", sp.dongGia);
                        cmd.Parameters.AddWithValue("@anhSanPham", sp.anhSanPham);
                        cmd.ExecuteReader();
                    }
                    conn.Close();
                }
            } catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            return new JsonResult("Insert thanh cong");
        }

        [HttpPut]
        public JsonResult Put(SanPham sp)
        {
            string sql = "Update SanPham set tenSP = @tenSP, soluong = @soLuong, chiTiet = @chiTiet, maPL = @maPL, dongia = @dongia, anhSanPham = @anhSanPham where maSP = @maSP;";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maSP", sp.maSP);
                        cmd.Parameters.AddWithValue("@tenSP", sp.tenSP);
                        cmd.Parameters.AddWithValue("@soLuong", sp.soLuong);
                        cmd.Parameters.AddWithValue("@chiTiet", sp.chiTiet);
                        cmd.Parameters.AddWithValue("@maPL", sp.maPL);
                        cmd.Parameters.AddWithValue("@dongia", sp.dongGia);
                        cmd.Parameters.AddWithValue("@anhSanPham", sp.anhSanPham);
                        cmd.ExecuteReader();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            return new JsonResult("Update thanh cong");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string sql = "Delete from SanPham where maSP = @maSP";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.ExecuteReader();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            return new JsonResult("Delete thanh cong");
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
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
