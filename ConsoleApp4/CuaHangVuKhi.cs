using System;
using System.Collections.Generic;
using System.Linq;

namespace CuaHangVuKhi
{
    class VuKhi
    {
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string Loai { get; set; }
        public int SatThuong { get; set; }
        public double Gia { get; set; }
        public int SoLuongTonKho { get; set; }

        public void HienThi()
        {
            Console.WriteLine($"{Ma,-8}{Ten,-18}{Loai,-12}{SatThuong,-10}{Gia,-10:N0}{SoLuongTonKho,-8}");
        }
    }

    class NguoiChoi
    {
        public string Ten { get; set; }
        public double SoTien { get; set; }
        public List<VuKhi> TuiDo { get; set; } = new List<VuKhi>();

        public void HienThiTuiDo()
        {
            if (TuiDo.Count == 0)
            {
                Console.WriteLine("Túi đồ trống.");
                return;
            }
            Console.WriteLine($"{"Mã",-8}{"Tên",-18}{"Loại",-12}{"Sát thương",-10}{"Giá mua",-10}");
            foreach (var vk in TuiDo)
                Console.WriteLine($"{vk.Ma,-8}{vk.Ten,-18}{vk.Loai,-12}{vk.SatThuong,-10}{vk.Gia,-10:N0}");
        }
    }

    class Program
    {
        static List<VuKhi> cuaHang = new List<VuKhi>();
        static NguoiChoi nguoiChoi;

        static void Main(string[] args)
        {
            KhoiTaoCuaHang();
            TaoNguoiChoi();

            int luaChon;
            do
            {
                HienThiMenu();
                luaChon = NhapSoNguyen("Chọn chức năng: ");
                switch (luaChon)
                {
                    case 1: HienThiDanhSachVuKhi(); break;
                    case 2: TimKiemTheoTen(); break;
                    case 3: MuaVuKhi(); break;
                    case 4: nguoiChoi.HienThiTuiDo(); break;
                    case 5: BanLaiVuKhi(); break;
                    case 6: VuKhiManhNhatDuTien(); break;
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

        static void KhoiTaoCuaHang()
        {
            cuaHang.Add(new VuKhi { Ma = "VK01", Ten = "Kiếm Gỗ", Loai = "Kiếm", SatThuong = 10, Gia = 50, SoLuongTonKho = 5 });
            cuaHang.Add(new VuKhi { Ma = "VK02", Ten = "Kiếm Sắt", Loai = "Kiếm", SatThuong = 25, Gia = 150, SoLuongTonKho = 3 });
            cuaHang.Add(new VuKhi { Ma = "VK03", Ten = "Cung Ngắn", Loai = "Cung", SatThuong = 18, Gia = 100, SoLuongTonKho = 4 });
            cuaHang.Add(new VuKhi { Ma = "VK04", Ten = "Rìu Chiến", Loai = "Rìu", SatThuong = 30, Gia = 200, SoLuongTonKho = 2 });
            cuaHang.Add(new VuKhi { Ma = "VK05", Ten = "Trượng Phép", Loai = "Trượng", SatThuong = 22, Gia = 180, SoLuongTonKho = 3 });
        }

        static void TaoNguoiChoi()
        {
            nguoiChoi = new NguoiChoi();
            Console.Write("Nhập tên người chơi: ");
            nguoiChoi.Ten = Console.ReadLine();
            nguoiChoi.SoTien = NhapSoThucKhongAm("Nhập số tiền ban đầu: ");
        }

        static void HienThiMenu()
        {
            Console.Clear();
            Console.WriteLine($"===== CỬA HÀNG VŨ KHÍ ===== (Người chơi: {nguoiChoi.Ten} | Tiền: {nguoiChoi.SoTien:N0})");
            Console.WriteLine("1. Hiển thị danh sách vũ khí trong cửa hàng");
            Console.WriteLine("2. Tìm kiếm vũ khí theo tên");
            Console.WriteLine("3. Mua vũ khí");
            Console.WriteLine("4. Hiển thị túi đồ");
            Console.WriteLine("5. Bán lại vũ khí (60% giá mua)");
            Console.WriteLine("6. Vũ khí sát thương lớn nhất mà bạn đủ tiền mua");
            Console.WriteLine("0. Thoát");
        }

        static void HienThiDanhSachVuKhi()
        {
            Console.WriteLine($"{"Mã",-8}{"Tên",-18}{"Loại",-12}{"Sát thương",-10}{"Giá",-10}{"Tồn kho",-8}");
            foreach (var vk in cuaHang) vk.HienThi();
        }

        static void TimKiemTheoTen()
        {
            string tuKhoa = NhapChuoiKhongRong("Nhập tên cần tìm: ");
            var ketQua = cuaHang.Where(x => x.Ten.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)).ToList();
            if (ketQua.Count == 0)
            {
                Console.WriteLine("Không tìm thấy vũ khí phù hợp.");
                return;
            }
            Console.WriteLine($"{"Mã",-8}{"Tên",-18}{"Loại",-12}{"Sát thương",-10}{"Giá",-10}{"Tồn kho",-8}");
            foreach (var vk in ketQua) vk.HienThi();
        }

        static void MuaVuKhi()
        {
            string ma = NhapChuoiKhongRong("Nhập mã vũ khí muốn mua: ");
            VuKhi vk = cuaHang.FirstOrDefault(x => x.Ma.Equals(ma, StringComparison.OrdinalIgnoreCase));

            if (vk == null)
            {
                Console.WriteLine("Mã vũ khí không tồn tại!");
                return;
            }
            if (vk.SoLuongTonKho <= 0)
            {
                Console.WriteLine("Vũ khí đã hết hàng!");
                return;
            }
            if (nguoiChoi.SoTien < vk.Gia)
            {
                Console.WriteLine("Bạn không đủ tiền để mua vũ khí này!");
                return;
            }

            nguoiChoi.SoTien -= vk.Gia;
            vk.SoLuongTonKho--;
            nguoiChoi.TuiDo.Add(new VuKhi { Ma = vk.Ma, Ten = vk.Ten, Loai = vk.Loai, SatThuong = vk.SatThuong, Gia = vk.Gia, SoLuongTonKho = 1 });
            Console.WriteLine($"-> Mua thành công {vk.Ten}! Số tiền còn lại: {nguoiChoi.SoTien:N0}");
        }

        static void BanLaiVuKhi()
        {
            if (nguoiChoi.TuiDo.Count == 0)
            {
                Console.WriteLine("Bạn không sở hữu vũ khí nào để bán!");
                return;
            }

            string ma = NhapChuoiKhongRong("Nhập mã vũ khí muốn bán: ");
            VuKhi vk = nguoiChoi.TuiDo.FirstOrDefault(x => x.Ma.Equals(ma, StringComparison.OrdinalIgnoreCase));

            if (vk == null)
            {
                Console.WriteLine("Bạn không sở hữu vũ khí có mã này!");
                return;
            }

            double giaBan = vk.Gia * 0.6;
            nguoiChoi.SoTien += giaBan;
            nguoiChoi.TuiDo.Remove(vk);

            VuKhi trongKho = cuaHang.FirstOrDefault(x => x.Ma.Equals(ma, StringComparison.OrdinalIgnoreCase));
            if (trongKho != null) trongKho.SoLuongTonKho++;

            Console.WriteLine($"-> Đã bán {vk.Ten} với giá {giaBan:N0}. Số tiền hiện tại: {nguoiChoi.SoTien:N0}");
        }

        static void VuKhiManhNhatDuTien()
        {
            var duTien = cuaHang.Where(x => x.Gia <= nguoiChoi.SoTien && x.SoLuongTonKho > 0).ToList();
            if (duTien.Count == 0)
            {
                Console.WriteLine("Không có vũ khí nào bạn đủ tiền mua.");
                return;
            }
            VuKhi manhNhat = duTien.OrderByDescending(x => x.SatThuong).First();
            Console.WriteLine("Vũ khí sát thương lớn nhất bạn đủ tiền mua:");
            Console.WriteLine($"{"Mã",-8}{"Tên",-18}{"Loại",-12}{"Sát thương",-10}{"Giá",-10}{"Tồn kho",-8}");
            manhNhat.HienThi();
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
