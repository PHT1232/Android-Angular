using FriedChicken.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace FriedChicken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private static string? sqlDataSource;

        public HoaDonController(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlDataSource = _configuration.GetConnectionString("FriedChickenAppCon");
        }

        [HttpGet]
        public JsonResult Get()
        {
            string sql = @"Select * from hoadon";

            DataTable data = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    using (MySqlDataReader msql = cmd.ExecuteReader())
                    {
                        data.Load(msql);
                        msql.Close();
                    }
                }
                conn.Close();
            }
            return new JsonResult(data);
        }

        [HttpPost]
        public JsonResult Post(HoaDon hd)
        {
            string sql = @"Insert into hoadon (maHD, maKH, ngayLap, tongTien) values (@maHD, @maKH, @ngayLap, @tongTien);";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHD", hd.maHD);
                        cmd.Parameters.AddWithValue("@maKH", hd.maKH);
                        cmd.Parameters.AddWithValue("@ngayLap", hd.ngayLap);
                        cmd.Parameters.AddWithValue("@tongTien", hd.tongTien);

                        cmd.ExecuteReader();
                    }
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

            return new JsonResult("Insert thành công");
        }

        [HttpPut]
        public JsonResult Put(HoaDon hd)
        {
            string sql = @"Update hoadon set maKH = @maKH, ngayLap = @ngayLap, tongTien = @tongTien where maHD = @maHD";
            try
            {
                using (MySqlConnection conn = new MySqlConnection())
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maKH", hd.maKH);
                        cmd.Parameters.AddWithValue("@ngayLap", hd.ngayLap);
                        cmd.Parameters.AddWithValue("@tongTien", hd.tongTien);
                        cmd.Parameters.AddWithValue("@maHD", hd.maHD);

                        cmd.ExecuteReader();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            return new JsonResult("Update thành công");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string sql = @"delete from hoadon where maHD = @maHD";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHD", id);
                        cmd.ExecuteReader();

                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            return new JsonResult("Delete bản ghi thành công");
        }
    }
}
