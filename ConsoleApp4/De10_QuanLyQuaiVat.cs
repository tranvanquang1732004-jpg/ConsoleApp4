using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyQuaiVat
{
    class QuaiVat
    {
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string Loai { get; set; } // Loài
        public int CapDo { get; set; }
        public double Mau { get; set; }
        public double SatThuong { get; set; }
        public double KinhNghiemThuong { get; set; }

        public void HienThi()
        {
            Console.WriteLine($"{Ma,-6}{Ten,-15}{Loai,-12}{CapDo,-6}{Mau,-10:N0}{SatThuong,-10:N0}{KinhNghiemThuong,-10:N0}");
        }
    }

    class Program
    {
        static List<QuaiVat> danhSach = new List<QuaiVat>();
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
                    case 1: ThemQuaiVat(); break;
                    case 2: HienThiDanhSach(); break;
                    case 3: TimTheoMa(); break;
                    case 4: TimTheoLoai(); break;
                    case 5: CapNhatCapDo(); break;
                    case 6: XoaQuaiHetMau(); break;
                    case 7: SapXepGiamDanTheoCapDo(); break;
                    case 8: HienThiQuaiManhNhat(); break;
                    case 9: ChonQuaiPhuHop(); break;
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
            Console.WriteLine("===== QUẢN LÝ ĐỘI QUÂN QUÁI VẬT =====");
            Console.WriteLine("1. Thêm quái vật");
            Console.WriteLine("2. Hiển thị danh sách");
            Console.WriteLine("3. Tìm quái theo mã");
            Console.WriteLine("4. Tìm quái theo loài");
            Console.WriteLine("5. Cập nhật cấp độ (tăng cấp)");
            Console.WriteLine("6. Xóa các quái có máu bằng 0");
            Console.WriteLine("7. Sắp xếp theo cấp độ giảm dần");
            Console.WriteLine("8. Hiển thị quái mạnh nhất");
            Console.WriteLine("9. Chọn ngẫu nhiên quái phù hợp với cấp người chơi");
            Console.WriteLine("0. Thoát");
        }

        static void InTieuDe()
        {
            Console.WriteLine($"{"Mã",-6}{"Tên",-15}{"Loài",-12}{"Cấp",-6}{"Máu",-10}{"SátThương",-10}{"EXP",-10}");
        }

        static void ThemQuaiVat()
        {
            QuaiVat qv = new QuaiVat();
            qv.Ma = NhapMaKhongTrung();
            qv.Ten = NhapChuoiKhongRong("Tên: ");
            qv.Loai = NhapChuoiKhongRong("Loài: ");
            qv.CapDo = NhapSoNguyenDuong("Cấp độ: ");
            qv.Mau = NhapSoThucKhongAm("Máu: ");
            qv.SatThuong = NhapSoThucKhongAm("Sát thương: ");
            qv.KinhNghiemThuong = NhapSoThucKhongAm("Kinh nghiệm thưởng: ");
            danhSach.Add(qv);
            Console.WriteLine("-> Thêm quái vật thành công!");
        }

        static void HienThiDanhSach()
        {
            if (!KiemTraRong()) return;
            InTieuDe();
            foreach (var qv in danhSach) qv.HienThi();
        }

        static void TimTheoMa()
        {
            if (!KiemTraRong()) return;
            string ma = NhapChuoiKhongRong("Nhập mã cần tìm: ");
            QuaiVat qv = danhSach.FirstOrDefault(x => x.Ma.Equals(ma, StringComparison.OrdinalIgnoreCase));
            if (qv == null) { Console.WriteLine("Không tìm thấy quái vật!"); return; }
            InTieuDe();
            qv.HienThi();
        }

        static void TimTheoLoai()
        {
            if (!KiemTraRong()) return;
            string loai = NhapChuoiKhongRong("Nhập loài cần tìm: ");
            var ketQua = danhSach.Where(x => x.Loai.Equals(loai, StringComparison.OrdinalIgnoreCase)).ToList();
            if (ketQua.Count == 0) { Console.WriteLine("Không tìm thấy quái vật thuộc loài này!"); return; }
            InTieuDe();
            foreach (var qv in ketQua) qv.HienThi();
        }

        static void CapNhatCapDo()
        {
            if (!KiemTraRong()) return;
            string ma = NhapChuoiKhongRong("Nhập mã quái vật muốn tăng cấp: ");
            QuaiVat qv = danhSach.FirstOrDefault(x => x.Ma.Equals(ma, StringComparison.OrdinalIgnoreCase));
            if (qv == null) { Console.WriteLine("Không tìm thấy quái vật!"); return; }

            qv.CapDo += 1;
            qv.Mau *= 1.10;
            qv.SatThuong *= 1.05;
            qv.KinhNghiemThuong *= 1.08;

            Console.WriteLine($"-> Đã tăng cấp {qv.Ten} lên cấp {qv.CapDo}.");
            Console.WriteLine($"Máu mới: {qv.Mau:N0}, Sát thương mới: {qv.SatThuong:N0}, EXP mới: {qv.KinhNghiemThuong:N0}");
        }

        static void XoaQuaiHetMau()
        {
            if (!KiemTraRong()) return;
            int soXoa = danhSach.RemoveAll(x => x.Mau == 0);
            Console.WriteLine(soXoa == 0 ? "Không có quái vật nào có máu bằng 0." : $"-> Đã xóa {soXoa} quái vật.");
        }

        static void SapXepGiamDanTheoCapDo()
        {
            if (!KiemTraRong()) return;
            danhSach = danhSach.OrderByDescending(x => x.CapDo).ToList();
            Console.WriteLine("Đã sắp xếp giảm dần theo cấp độ:");
            HienThiDanhSach();
        }

        static void HienThiQuaiManhNhat()
        {
            if (!KiemTraRong()) return;
            QuaiVat manhNhat = danhSach.OrderByDescending(x => x.CapDo).ThenByDescending(x => x.SatThuong).First();
            InTieuDe();
            manhNhat.HienThi();
        }

        static void ChonQuaiPhuHop()
        {
            if (!KiemTraRong()) return;
            int capNguoiChoi = NhapSoNguyenDuong("Nhập cấp độ người chơi: ");

            var phuHop = danhSach.Where(x => Math.Abs(x.CapDo - capNguoiChoi) <= 2).ToList();
            if (phuHop.Count == 0)
            {
                Console.WriteLine("Không có quái vật nào phù hợp với cấp độ người chơi!");
                return;
            }

            QuaiVat chon = phuHop[rand.Next(phuHop.Count)];
            Console.WriteLine("Quái vật được chọn ngẫu nhiên phù hợp với cấp độ của bạn:");
            InTieuDe();
            chon.HienThi();
        }

        static bool KiemTraRong()
        {
            if (danhSach.Count == 0) { Console.WriteLine("Danh sách quái vật đang trống!"); return false; }
            return true;
        }

        static string NhapMaKhongTrung()
        {
            string ma;
            while (true)
            {
                ma = NhapChuoiKhongRong("Mã quái vật: ");
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

        static int NhapSoNguyenDuong(string thongBao)
        {
            int kq;
            while (true)
            {
                kq = NhapSoNguyen(thongBao);
                if (kq <= 0) Console.WriteLine("Giá trị phải lớn hơn 0!");
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
