using System;
using System.Collections.Generic;
using System.Linq;

namespace MoPhongGiaiDau
{
    class Doi
    {
        public string Ma { get; set; }
        public string Ten { get; set; }
        public int SoTranThang { get; set; }
        public int SoTranHoa { get; set; }
        public int SoTranThua { get; set; }
        public int DiemSo { get; set; }

        public void TinhLaiDiem()
        {
            DiemSo = SoTranThang * 3 + SoTranHoa * 1 + SoTranThua * 0;
        }

        public int TongSoTran() => SoTranThang + SoTranHoa + SoTranThua;

        public void HienThi()
        {
            Console.WriteLine($"{Ma,-6}{Ten,-15}{SoTranThang,-6}{SoTranHoa,-6}{SoTranThua,-6}{DiemSo,-6}");
        }
    }

    class Program
    {
        static List<Doi> danhSachDoi = new List<Doi>();
        static Random rand = new Random();

        static void Main(string[] args)
        {
            int luaChon;
            do
            {
                HienThiMenu();
                luaChon = NhapSoNguyen("Chọn chức năng: ");
                switch (luaChon)
                {
                    case 1: NhapDanhSachDoi(); break;
                    case 2: NhapKetQuaTranDau(); break;
                    case 3: HienThiBangXepHang(); break;
                    case 4: HienThiDoiDauBang(); break;
                    case 5: HienThiDoiChuaThang(); break;
                    case 6: ThongKeTongSoTran(); break;
                    case 7: SinhNgauNhienTatCaTranDau(); break;
                    case 0: Console.WriteLine("Tạm biệt!"); break;
                    default: Console.WriteLine("Lựa chọn không hợp lệ!"); break;
                }
                if (luaChon != 0)
                {
                    Console.WriteLine("\nNhấn phím bất kỳ để tiếp tục...");
                    Console.ReadKey();
                }
            } while (luaChon != 0);
        }

        static void HienThiMenu()
        {
            Console.Clear();
            Console.WriteLine("===== MÔ PHỎNG GIẢI ĐẤU =====");
            Console.WriteLine("1. Nhập danh sách đội");
            Console.WriteLine("2. Nhập kết quả trận đấu giữa 2 đội");
            Console.WriteLine("3. Hiển thị bảng xếp hạng");
            Console.WriteLine("4. Hiển thị đội đứng đầu");
            Console.WriteLine("5. Hiển thị đội chưa thắng trận nào");
            Console.WriteLine("6. Thống kê tổng số trận đã diễn ra");
            Console.WriteLine("7. Sinh ngẫu nhiên kết quả tất cả các trận đấu");
            Console.WriteLine("0. Thoát");
        }

        static void NhapDanhSachDoi()
        {
            int soLuong = NhapSoNguyen("Nhập số lượng đội muốn thêm: ");
            for (int i = 0; i < soLuong; i++)
            {
                Console.WriteLine($"\n--- Đội thứ {i + 1} ---");
                Doi doi = new Doi();
                doi.Ma = NhapMaKhongTrung();
                doi.Ten = NhapChuoiKhongRong("Tên đội: ");
                danhSachDoi.Add(doi);
                Console.WriteLine("-> Thêm đội thành công!");
            }
        }

        static void InTieuDe()
        {
            Console.WriteLine($"{"Mã",-6}{"Tên",-15}{"Thắng",-6}{"Hòa",-6}{"Thua",-6}{"Điểm",-6}");
        }

        static void HienThiBangXepHang()
        {
            if (!KiemTraRong()) return;
            var bxh = danhSachDoi.OrderByDescending(x => x.DiemSo).ThenByDescending(x => x.SoTranThang).ToList();
            InTieuDe();
            foreach (var doi in bxh) doi.HienThi();
        }

        static void NhapKetQuaTranDau()
        {
            if (danhSachDoi.Count < 2) { Console.WriteLine("Cần ít nhất 2 đội để nhập kết quả!"); return; }

            string maDoi1 = NhapChuoiKhongRong("Mã đội 1: ");
            Doi doi1 = TimTheoMa(maDoi1);
            if (doi1 == null) { Console.WriteLine("Không tìm thấy đội 1!"); return; }

            string maDoi2 = NhapChuoiKhongRong("Mã đội 2: ");
            Doi doi2 = TimTheoMa(maDoi2);
            if (doi2 == null) { Console.WriteLine("Không tìm thấy đội 2!"); return; }

            if (doi1 == doi2) { Console.WriteLine("Hai đội không được trùng nhau!"); return; }

            Console.WriteLine("Kết quả: 1 - Đội 1 thắng | 2 - Hòa | 3 - Đội 2 thắng");
            int ketQua = NhapSoNguyen("Chọn kết quả: ");

            CapNhatKetQua(doi1, doi2, ketQua);
            Console.WriteLine("-> Đã cập nhật kết quả trận đấu!");
        }

        static void CapNhatKetQua(Doi doi1, Doi doi2, int ketQua)
        {
            switch (ketQua)
            {
                case 1:
                    doi1.SoTranThang++;
                    doi2.SoTranThua++;
                    break;
                case 2:
                    doi1.SoTranHoa++;
                    doi2.SoTranHoa++;
                    break;
                case 3:
                    doi2.SoTranThang++;
                    doi1.SoTranThua++;
                    break;
                default:
                    Console.WriteLine("Kết quả không hợp lệ, mặc định là hòa.");
                    doi1.SoTranHoa++;
                    doi2.SoTranHoa++;
                    break;
            }
            doi1.TinhLaiDiem();
            doi2.TinhLaiDiem();
        }

        static void HienThiDoiDauBang()
        {
            if (!KiemTraRong()) return;
            Doi dauBang = danhSachDoi.OrderByDescending(x => x.DiemSo).ThenByDescending(x => x.SoTranThang).First();
            InTieuDe();
            dauBang.HienThi();
        }

        static void HienThiDoiChuaThang()
        {
            if (!KiemTraRong()) return;
            var ketQua = danhSachDoi.Where(x => x.SoTranThang == 0).ToList();
            if (ketQua.Count == 0) { Console.WriteLine("Tất cả các đội đều đã có ít nhất 1 trận thắng."); return; }
            InTieuDe();
            foreach (var doi in ketQua) doi.HienThi();
        }

        static void ThongKeTongSoTran()
        {
            int tongTran = danhSachDoi.Sum(x => x.SoTranThang + x.SoTranHoa + x.SoTranThua) / 2;
            Console.WriteLine($"Tổng số trận đã diễn ra: {tongTran}");
        }

        static void SinhNgauNhienTatCaTranDau()
        {
            if (danhSachDoi.Count < 2) { Console.WriteLine("Cần ít nhất 2 đội!"); return; }

            for (int i = 0; i < danhSachDoi.Count; i++)
            {
                for (int j = i + 1; j < danhSachDoi.Count; j++)
                {
                    int ketQua = rand.Next(1, 4); // 1,2,3
                    CapNhatKetQua(danhSachDoi[i], danhSachDoi[j], ketQua);
                }
            }
            Console.WriteLine("-> Đã sinh ngẫu nhiên kết quả cho tất cả các cặp đấu!");
            HienThiBangXepHang();
        }

        static Doi TimTheoMa(string ma) => danhSachDoi.FirstOrDefault(x => x.Ma.Equals(ma, StringComparison.OrdinalIgnoreCase));

        static bool KiemTraRong()
        {
            if (danhSachDoi.Count == 0) { Console.WriteLine("Danh sách đội đang trống!"); return false; }
            return true;
        }

        static string NhapMaKhongTrung()
        {
            string ma;
            while (true)
            {
                ma = NhapChuoiKhongRong("Mã đội: ");
                if (danhSachDoi.Any(x => x.Ma.Equals(ma, StringComparison.OrdinalIgnoreCase)))
                    Console.WriteLine("Mã đã tồn tại! Vui lòng nhập mã khác.");
                else break;
            }
            return ma;
        }

        static string NhapChuoiKhongRong(string thongBao)
        {
            string kq;
            do
            {
                Console.Write(thongBao);
                kq = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(kq)) Console.WriteLine("Dữ liệu không được để trống!");
            } while (string.IsNullOrWhiteSpace(kq));
            return kq.Trim();
        }

        static int NhapSoNguyen(string thongBao)
        {
            int kq;
            while (true)
            {
                Console.Write(thongBao);
                if (int.TryParse(Console.ReadLine(), out kq)) return kq;
                Console.WriteLine("Vui lòng nhập một số nguyên hợp lệ!");
            }
        }
    }
}
