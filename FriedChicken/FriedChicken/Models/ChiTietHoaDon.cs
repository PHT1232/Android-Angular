using System.ComponentModel.DataAnnotations.Schema;

namespace FriedChicken.Models
{
    public class ChiTietHoaDon
    {
        public string maHD { get; set; }
        public string maKH { get; set; }
        public int soLuong { get; set; }
        public float donGia { get; set; }
        public float thanhTien { get; set; }
    }
}
