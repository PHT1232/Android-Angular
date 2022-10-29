namespace FriedChicken.Models
{
    public class PhanLoai
    {
        public string maPL { get; set; }
        public string tenPL { get; set; }
        public string anhPhanLoai { get; set; }

        public PhanLoai(string maPL, string tenPL, string anhPhanLoai)
        {
            this.maPL = maPL;
            this.tenPL = tenPL;
            this.anhPhanLoai = anhPhanLoai;
        }
    }
}
