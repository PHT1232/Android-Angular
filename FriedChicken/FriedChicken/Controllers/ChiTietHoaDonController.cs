using FriedChicken.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace FriedChicken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietHoaDonController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private static string? sqlDataSource;

        public ChiTietHoaDonController(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlDataSource = _configuration.GetConnectionString("FriedChickenAppCon");
        }

        [HttpGet]
        public JsonResult Get()
        {
            string sql = @"Select * from chitiethoadon";

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
        public JsonResult Post(ChiTietHoaDon kh)
        {
            string sql = @"Insert into chitiethoadon (maHD, maKH, soLuong, donGia, thanhTien) values (@maHD, @maKH, @soLuong, @donGia, @thanhTien);";
            
            try 
            {
                using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@maHD", kh.maHD);
                        cmd.Parameters.AddWithValue("@maKH", kh.maKH);
                        cmd.Parameters.AddWithValue("@soLuong", kh.soLuong);
                        cmd.Parameters.AddWithValue("@donGia", kh.donGia);
                        cmd.Parameters.AddWithValue("@thanhTien", kh.thanhTien);
                        cmd.ExecuteReader();

                    }
                    conn.Close();
                }
            } catch(Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            
            return new JsonResult("Success fully added");
        }

        //[HttpPut]
        //public JsonResult Put(ChiTietHoaDon kh)
        //{
        //    string sql = @"Update ChiTietHoaDon set maHD = @maHD, maKH = @maKH, soLuong = @soLuong, donGia = @donGia, thanhTien = @thanhTien where maHD = @maHD";

        //    DataTable data = new DataTable();
        //    string sqlDataSource = _configuration.GetConnectionString("FriedChickenAppCon");
        //    MySqlDataReader msql;
        //    using (MySqlConnection conn = new MySqlConnection(sqlDataSource))
        //    {
        //        conn.Open();
        //        using (MySqlCommand cmd = new MySqlCommand(sql, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@maKH", kh.maKH);
        //            cmd.Parameters.AddWithValue("@tenKH", kh.tenKH);
        //            cmd.Parameters.AddWithValue("@GT", kh.GT);
        //            cmd.Parameters.AddWithValue("@email", kh.email);
        //            cmd.Parameters.AddWithValue("@sdt", kh.sdt);
        //            cmd.Parameters.AddWithValue("@diaChi", kh.diaChi);
        //            cmd.Parameters.AddWithValue("@ngaySinh", kh.ngaySinh);
        //            msql = cmd.ExecuteReader();
        //            data.Load(msql);

        //            msql.Close();
        //            conn.Close();
        //        }
        //    }
        //    return new JsonResult("Update success fully");
        //}

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string sql = @"delete from ChiTietHoaDon where maHD = @maHD";
            
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
            } catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            
            return new JsonResult("Delete success fully");
        }
    }
}
