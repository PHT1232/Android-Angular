using FriedChicken.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System.Data;

namespace FriedChicken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private static string? sqlDataSource;

        public KhachHangController(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlDataSource = _configuration.GetConnectionString("FriedChickenAppCon");
        }

        private string GenerateID()
        {
            return Guid.NewGuid().ToString();
        }

        [HttpGet]
        public JsonResult Get()
        {
            string sql = @"Select * from KhachHang";

            DataTable dataTable = new DataTable();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        using (MySqlDataReader msql = cmd.ExecuteReader())
                        {
                            dataTable.Load(msql);
                            msql.Close();
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

        [HttpPost]
        public JsonResult Post(KhachHang kh)
        {
            string sql = @"Insert into khachhang (maKH, tenKH, GT, email, sdt, diaChi, ngaySinh) values (@maKH, @tenKH, @GT, @email, @sdt, @diaChi, @ngaySinh);";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maKH", GenerateID());
                        cmd.Parameters.AddWithValue("@tenKH", kh.tenKH);
                        cmd.Parameters.AddWithValue("@GT", kh.GT);
                        cmd.Parameters.AddWithValue("@email", kh.email);
                        cmd.Parameters.AddWithValue("@sdt", kh.sdt);
                        cmd.Parameters.AddWithValue("@diaChi", kh.diaChi);
                        cmd.Parameters.AddWithValue("@ngaySinh", kh.ngaySinh);
                        cmd.ExecuteReader();

                    }
                    conn.Close();
                }
            } catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            return new JsonResult("Success fully added");
        }

        [HttpPut]
        public JsonResult Put(KhachHang kh)
        {
            string sql = @"Update khachhang set tenKH = @tenKH, GT = @GT, email = @email, sdt = @sdt, diaChi = @diaChi, ngaySinh = @ngaySinh where maKH = @maKH";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maKH", kh.maKH);
                        cmd.Parameters.AddWithValue("@tenKH", kh.tenKH);
                        cmd.Parameters.AddWithValue("@GT", kh.GT);
                        cmd.Parameters.AddWithValue("@email", kh.email);
                        cmd.Parameters.AddWithValue("@sdt", kh.sdt);
                        cmd.Parameters.AddWithValue("@diaChi", kh.diaChi);
                        cmd.Parameters.AddWithValue("@ngaySinh", kh.ngaySinh);
                        cmd.ExecuteReader();

                    }
                    conn.Close();
                }
            } catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            
            return new JsonResult("Update success fully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string sql = @"delete from khachhang where maKH = @maKH";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maKH", id);
                        cmd.ExecuteReader();

                    }
                    conn.Close();
                }
            } catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            
            return new JsonResult("Delete success fully");
        }

    }
}
