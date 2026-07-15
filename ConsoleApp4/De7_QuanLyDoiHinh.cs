using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyDoiHinh
{
    class ChienBinh
    {
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string LopNhanVat { get; set; }
        public int Mau { get; set; }
        public int TanCong { get; set; }
        public int PhongThu { get; set; }
        public int TocDo { get; set; }

        public void HienThi()
        {
            Console.WriteLine($"{Ma,-6}{Ten,-15}{LopNhanVat,-12}{Mau,-6}{TanCong,-6}{PhongThu,-6}{TocDo,-6}");
        }
    }

    class Program
    {
        const int SO_LUONG_TOI_DA_DOI_HINH = 5;
        static List<ChienBinh> danhSachTatCa = new List<ChienBinh>();
        static List<ChienBinh> doiHinh = new List<ChienBinh>();

        static void Main(string[] args)
        {
            int luaChon;
            do
            {
                HienThiMenu();
                luaChon = NhapSoNguyen("Chọn chức năng: ");
                switch (luaChon)
                {
                    case 1: ThemChienBinh(); break;
                    case 2: HienThiDanhSachTatCa(); break;
                    case 3: ChonVaoDoiHinh(); break;
                    case 4: XoaKhoiDoiHinh(); break;
                    case 5: HienThiDoiHinh(); break;
                    case 6: HienThiTongChiSoDoiHinh(); break;
                    case 7: TimChienBinhManhNhat(); break;
                    case 8: SapXepDoiHinhTheoTocDo(); break;
                    case 9: KiemTraDieuKienThamChien(); break;
                    case 10: ThongKeTheoLop(); break;
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
            Console.WriteLine("===== QUẢN LÝ ĐỘI HÌNH CHIẾN ĐẤU =====");
            Console.WriteLine("1. Thêm chiến binh vào danh sách");
            Console.WriteLine("2. Hiển thị danh sách tất cả chiến binh");
            Console.WriteLine("3. Chọn chiến binh vào đội hình");
            Console.WriteLine("4. Xóa chiến binh khỏi đội hình");
            Console.WriteLine("5. Hiển thị đội hình hiện tại");
            Console.WriteLine("6. Hiển thị tổng chỉ số đội hình");
            Console.WriteLine("7. Tìm chiến binh mạnh nhất");
            Console.WriteLine("8. Sắp xếp đội hình theo tốc độ giảm dần");
            Console.WriteLine("9. Kiểm tra đội hình có đủ điều kiện tham chiến");
            Console.WriteLine("10. Thống kê số chiến binh theo lớp nhân vật");
            Console.WriteLine("0. Thoát");
        }

        static void InTieuDe()
        {
            Console.WriteLine($"{"Mã",-6}{"Tên",-15}{"Lớp",-12}{"Máu",-6}{"ATK",-6}{"DEF",-6}{"SPD",-6}");
        }

        static void ThemChienBinh()
        {
            ChienBinh cb = new ChienBinh();
            cb.Ma = NhapMaKhongTrung();
            cb.Ten = NhapChuoiKhongRong("Tên: ");
            cb.LopNhanVat = NhapChuoiKhongRong("Lớp nhân vật: ");
            cb.Mau = NhapSoNguyenKhongAm("Máu: ");
            cb.TanCong = NhapSoNguyenKhongAm("Tấn công: ");
            cb.PhongThu = NhapSoNguyenKhongAm("Phòng thủ: ");
            cb.TocDo = NhapSoNguyenKhongAm("Tốc độ: ");
            danhSachTatCa.Add(cb);
            Console.WriteLine("-> Thêm chiến binh thành công!");
        }

        static void HienThiDanhSachTatCa()
        {
            if (danhSachTatCa.Count == 0) { Console.WriteLine("Danh sách chiến binh đang trống!"); return; }
            InTieuDe();
            foreach (var cb in danhSachTatCa) cb.HienThi();
        }

        static void ChonVaoDoiHinh()
        {
            if (danhSachTatCa.Count == 0) { Console.WriteLine("Chưa có chiến binh nào trong danh sách!"); return; }

            if (doiHinh.Count >= SO_LUONG_TOI_DA_DOI_HINH)
            {
                Console.WriteLine($"Đội hình đã đủ {SO_LUONG_TOI_DA_DOI_HINH} chiến binh, không thể thêm nữa!");
                return;
            }

            string ma = NhapChuoiKhongRong("Nhập mã chiến binh muốn thêm vào đội hình: ");
            ChienBinh cb = danhSachTatCa.FirstOrDefault(x => x.Ma.Equals(ma, StringComparison.OrdinalIgnoreCase));

            if (cb == null) { Console.WriteLine("Không tìm thấy chiến binh có mã này!"); return; }

            if (doiHinh.Any(x => x.Ma.Equals(ma, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Chiến binh này đã có trong đội hình!");
                return;
            }

            doiHinh.Add(cb);
            Console.WriteLine($"-> Đã thêm {cb.Ten} vào đội hình ({doiHinh.Count}/{SO_LUONG_TOI_DA_DOI_HINH}).");
        }

        static void XoaKhoiDoiHinh()
        {
            if (doiHinh.Count == 0) { Console.WriteLine("Đội hình đang trống!"); return; }
            string ma = NhapChuoiKhongRong("Nhập mã chiến binh muốn xóa khỏi đội hình: ");
            ChienBinh cb = doiHinh.FirstOrDefault(x => x.Ma.Equals(ma, StringComparison.OrdinalIgnoreCase));
            if (cb == null) { Console.WriteLine("Chiến binh này không có trong đội hình!"); return; }
            doiHinh.Remove(cb);
            Console.WriteLine("-> Đã xóa khỏi đội hình!");
        }

        static void HienThiDoiHinh()
        {
            if (doiHinh.Count == 0) { Console.WriteLine("Đội hình đang trống!"); return; }
            InTieuDe();
            foreach (var cb in doiHinh) cb.HienThi();
        }

        static void HienThiTongChiSoDoiHinh()
        {
            if (doiHinh.Count == 0) { Console.WriteLine("Đội hình đang trống!"); return; }
            Console.WriteLine($"Tổng máu: {doiHinh.Sum(x => x.Mau)}");
            Console.WriteLine($"Tổng tấn công: {doiHinh.Sum(x => x.TanCong)}");
            Console.WriteLine($"Tổng phòng thủ: {doiHinh.Sum(x => x.PhongThu)}");
            Console.WriteLine($"Tổng tốc độ: {doiHinh.Sum(x => x.TocDo)}");
        }

        static void TimChienBinhManhNhat()
        {
            if (danhSachTatCa.Count == 0) { Console.WriteLine("Danh sách chiến binh đang trống!"); return; }
            // "Mạnh nhất" tính theo tổng chỉ số Mau + TanCong*2 + PhongThu (tương tự công thức Power ở đề 1)
            ChienBinh manhNhat = danhSachTatCa.OrderByDescending(x => x.Mau + x.TanCong * 2 + x.PhongThu).First();
            InTieuDe();
            manhNhat.HienThi();
        }

        static void SapXepDoiHinhTheoTocDo()
        {
            if (doiHinh.Count == 0) { Console.WriteLine("Đội hình đang trống!"); return; }
            doiHinh = doiHinh.OrderByDescending(x => x.TocDo).ToList();
            Console.WriteLine("Đã sắp xếp đội hình theo tốc độ giảm dần:");
            HienThiDoiHinh();
        }

        static void KiemTraDieuKienThamChien()
        {
            if (doiHinh.Count == 0) { Console.WriteLine("Đội hình đang trống!"); return; }

            int tongMau = doiHinh.Sum(x => x.Mau);
            int tongTanCong = doiHinh.Sum(x => x.TanCong);
            int soLuong = doiHinh.Count;

            bool duMau = tongMau >= 1000;
            bool duTanCong = tongTanCong >= 300;
            bool duSoLuong = soLuong >= 3;

            Console.WriteLine($"Tổng máu: {tongMau} (yêu cầu >=1000) - {(duMau ? "Đạt" : "Chưa đạt")}");
            Console.WriteLine($"Tổng tấn công: {tongTanCong} (yêu cầu >=300) - {(duTanCong ? "Đạt" : "Chưa đạt")}");
            Console.WriteLine($"Số chiến binh: {soLuong} (yêu cầu >=3) - {(duSoLuong ? "Đạt" : "Chưa đạt")}");

            if (duMau && duTanCong && duSoLuong)
                Console.WriteLine("=> Đội hình ĐỦ điều kiện tham chiến!");
            else
                Console.WriteLine("=> Đội hình CHƯA đủ điều kiện tham chiến!");
        }

        static void ThongKeTheoLop()
        {
            if (danhSachTatCa.Count == 0) { Console.WriteLine("Danh sách chiến binh đang trống!"); return; }
            var thongKe = danhSachTatCa.GroupBy(x => x.LopNhanVat).Select(g => new { Lop = g.Key, SoLuong = g.Count() });
            foreach (var item in thongKe)
                Console.WriteLine($"- {item.Lop}: {item.SoLuong} chiến binh");
        }

        static string NhapMaKhongTrung()
        {
            string ma;
            while (true)
            {
                ma = NhapChuoiKhongRong("Mã chiến binh: ");
                if (danhSachTatCa.Any(x => x.Ma.Equals(ma, StringComparison.OrdinalIgnoreCase)))
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

        static int NhapSoNguyenKhongAm(string thongBao)
        {
            int kq;
            while (true)
            {
                kq = NhapSoNguyen(thongBao);
                if (kq < 0) Console.WriteLine("Giá trị không được âm!");
                else return kq;
            }
        }
    }
}
