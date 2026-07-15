using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyDiemNguoiChoi
{
    class NguoiChoi
    {
        public string Ma { get; set; }
        public string Ten { get; set; }
        public int Diem { get; set; }
        public int SoTranThang { get; set; }
        public int SoTranThua { get; set; }
        public double ThoiGianChoi { get; set; } // giờ

        public int TongSoTran() => SoTranThang + SoTranThua;

        public double TyLeThang()
        {
            if (TongSoTran() == 0) return 0;
            return (double)SoTranThang / TongSoTran() * 100;
        }

        public string XepHang()
        {
            if (Diem >= 5000) return "Diamond";
            if (Diem >= 3000) return "Gold";
            if (Diem >= 1000) return "Silver";
            return "Bronze";
        }

        public void HienThi()
        {
            Console.WriteLine($"{Ma,-6}{Ten,-15}{Diem,-8}{SoTranThang,-6}{SoTranThua,-6}{TyLeThang(),-8:N1}{XepHang(),-10}");
        }
    }

    class Program
    {
        static List<NguoiChoi> danhSach = new List<NguoiChoi>();

        static void Main(string[] args)
        {
            int luaChon;
            do
            {
                HienThiMenu();
                luaChon = NhapSoNguyen("Chọn chức năng: ");
                switch (luaChon)
                {
                    case 1: NhapDanhSach(); break;
                    case 2: HienThiDanhSach(); break;
                    case 3: CapNhatDiem(); break;
                    case 4: TimTheoTen(); break;
                    case 5: Top5DiemCao(); break;
                    case 6: NguoiTyLeThangCaoNhat(); break;
                    case 7: ThongKeTheoHang(); break;
                    case 8: XoaChuaThamGia(); break;
                    case 9: SapXepGiamDanTheoDiem(); break;
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
            Console.WriteLine("===== QUẢN LÝ ĐIỂM NGƯỜI CHƠI =====");
            Console.WriteLine("1. Nhập danh sách người chơi");
            Console.WriteLine("2. Hiển thị danh sách");
            Console.WriteLine("3. Cập nhật điểm theo mã");
            Console.WriteLine("4. Tìm người chơi theo tên");
            Console.WriteLine("5. Top 5 người chơi điểm cao nhất");
            Console.WriteLine("6. Người chơi có tỷ lệ thắng cao nhất");
            Console.WriteLine("7. Thống kê số người theo hạng");
            Console.WriteLine("8. Xóa người chơi chưa tham gia trận nào");
            Console.WriteLine("9. Sắp xếp giảm dần theo điểm");
            Console.WriteLine("0. Thoát");
        }

        static void NhapDanhSach()
        {
            int soLuong = NhapSoNguyen("Nhập số lượng người chơi muốn thêm: ");
            for (int i = 0; i < soLuong; i++)
            {
                Console.WriteLine($"\n--- Người chơi thứ {i + 1} ---");
                NguoiChoi nc = new NguoiChoi();
                nc.Ma = NhapMaKhongTrung();
                nc.Ten = NhapChuoiKhongRong("Tên: ");
                nc.Diem = NhapSoNguyenKhongAm("Điểm: ");
                nc.SoTranThang = NhapSoNguyenKhongAm("Số trận thắng: ");
                nc.SoTranThua = NhapSoNguyenKhongAm("Số trận thua: ");
                nc.ThoiGianChoi = NhapSoThucKhongAm("Thời gian chơi (giờ): ");
                danhSach.Add(nc);
                Console.WriteLine("-> Thêm thành công!");
            }
        }

        static void InTieuDe()
        {
            Console.WriteLine($"{"Mã",-6}{"Tên",-15}{"Điểm",-8}{"Thắng",-6}{"Thua",-6}{"TyLệ%",-8}{"Hạng",-10}");
        }

        static void HienThiDanhSach()
        {
            if (!KiemTraRong()) return;
            InTieuDe();
            foreach (var nc in danhSach) nc.HienThi();
        }

        static void CapNhatDiem()
        {
            if (!KiemTraRong()) return;
            string ma = NhapChuoiKhongRong("Nhập mã người chơi: ");
            NguoiChoi nc = danhSach.FirstOrDefault(x => x.Ma.Equals(ma, StringComparison.OrdinalIgnoreCase));
            if (nc == null) { Console.WriteLine("Không tìm thấy người chơi!"); return; }
            nc.Diem = NhapSoNguyenKhongAm("Nhập điểm mới: ");
            Console.WriteLine("-> Cập nhật thành công!");
        }

        static void TimTheoTen()
        {
            if (!KiemTraRong()) return;
            string ten = NhapChuoiKhongRong("Nhập tên cần tìm: ");
            var ketQua = danhSach.Where(x => x.Ten.Contains(ten, StringComparison.OrdinalIgnoreCase)).ToList();
            if (ketQua.Count == 0) { Console.WriteLine("Không tìm thấy!"); return; }
            InTieuDe();
            foreach (var nc in ketQua) nc.HienThi();
        }

        static void Top5DiemCao()
        {
            if (!KiemTraRong()) return;
            var top5 = danhSach.OrderByDescending(x => x.Diem).Take(5).ToList();
            InTieuDe();
            foreach (var nc in top5) nc.HienThi();
        }

        static void NguoiTyLeThangCaoNhat()
        {
            if (!KiemTraRong()) return;
            var coTran = danhSach.Where(x => x.TongSoTran() > 0).ToList();
            if (coTran.Count == 0) { Console.WriteLine("Chưa có người chơi nào tham gia trận đấu."); return; }
            NguoiChoi caoNhat = coTran.OrderByDescending(x => x.TyLeThang()).First();
            InTieuDe();
            caoNhat.HienThi();
        }

        static void ThongKeTheoHang()
        {
            if (!KiemTraRong()) return;
            var thongKe = danhSach.GroupBy(x => x.XepHang()).Select(g => new { Hang = g.Key, SoLuong = g.Count() });
            foreach (var item in thongKe)
                Console.WriteLine($"- {item.Hang}: {item.SoLuong} người");
        }

        static void XoaChuaThamGia()
        {
            if (!KiemTraRong()) return;
            int soXoa = danhSach.RemoveAll(x => x.TongSoTran() == 0);
            Console.WriteLine(soXoa == 0 ? "Không có người chơi nào chưa tham gia trận nào." : $"-> Đã xóa {soXoa} người chơi.");
        }

        static void SapXepGiamDanTheoDiem()
        {
            if (!KiemTraRong()) return;
            danhSach = danhSach.OrderByDescending(x => x.Diem).ThenByDescending(x => x.SoTranThang).ToList();
            Console.WriteLine("Đã sắp xếp giảm dần theo điểm (ưu tiên số trận thắng khi bằng điểm):");
            HienThiDanhSach();
        }

        static bool KiemTraRong()
        {
            if (danhSach.Count == 0) { Console.WriteLine("Danh sách đang trống!"); return false; }
            return true;
        }

        static string NhapMaKhongTrung()
        {
            string ma;
            while (true)
            {
                ma = NhapChuoiKhongRong("Mã người chơi: ");
                if (danhSach.Any(x => x.Ma.Equals(ma, StringComparison.OrdinalIgnoreCase)))
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

        static double NhapSoThucKhongAm(string thongBao)
        {
            double kq;
            while (true)
            {
                Console.Write(thongBao);
                if (double.TryParse(Console.ReadLine(), out kq) && kq >= 0) return kq;
                Console.WriteLine("Vui lòng nhập một số hợp lệ (không âm)!");
            }
        }
    }
}
