using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyNhanVatGame
{

    class NhanVat
    {
        public string MaNhanVat { get; set; }
        public string TenNhanVat { get; set; }
        public string LoaiNhanVat { get; set; }
        public int CapDo { get; set; }
        public int Mau { get; set; }
        public int SucTanCong { get; set; }
        public int PhongThu { get; set; }

        // Power = Health + Attack * 2 + Defense
        public int TinhSucManh()
        {
            return Mau + SucTanCong * 2 + PhongThu;
        }

        public void HienThi()
        {
            Console.WriteLine(
                $"{MaNhanVat,-8}{TenNhanVat,-15}{LoaiNhanVat,-12}{CapDo,-6}{Mau,-6}{SucTanCong,-8}{PhongThu,-8}{TinhSucManh(),-8}"
            );
        }
    }

    class Program
    {
        static List<NhanVat> danhSachNhanVat = new List<NhanVat>();

        static void Main(string[] args)
        {
            int luaChon;
            do
            {
                HienThiMenu();
                luaChon = NhapSoNguyen("Chọn chức năng: ");

                switch (luaChon)
                {
                    case 1: NhapDanhSachNhanVat(); break;
                    case 2: HienThiDanhSach(); break;
                    case 3: TimTheoMa(); break;
                    case 4: TimSucTanCongLonNhat(); break;
                    case 5: SapXepGiamDanTheoCapDo(); break;
                    case 6: XoaTheoMa(); break;
                    case 7: ThongKeTheoLoai(); break;
                    case 8: HienThiNhanVatManhHonGiaTri(); break;
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
            Console.WriteLine("===== QUẢN LÝ NHÂN VẬT GAME =====");
            Console.WriteLine("1. Nhập danh sách nhân vật");
            Console.WriteLine("2. Hiển thị toàn bộ danh sách");
            Console.WriteLine("3. Tìm nhân vật theo mã");
            Console.WriteLine("4. Tìm nhân vật có sức tấn công lớn nhất");
            Console.WriteLine("5. Sắp xếp giảm dần theo cấp độ");
            Console.WriteLine("6. Xóa nhân vật theo mã");
            Console.WriteLine("7. Thống kê số lượng theo loại");
            Console.WriteLine("8. Hiển thị nhân vật có sức mạnh lớn hơn giá trị nhập");
            Console.WriteLine("0. Thoát");
            Console.WriteLine("==================================");
        }

        // ============================
// 1. NHẬP DANH SÁCH (SỬA LẠI)
// ============================
        static void NhapDanhSachNhanVat()
        {
            int soLuong = NhapSoNguyen("Nhập số lượng nhân vật muốn thêm: ");

            for (int i = 0; i < soLuong; i++)
            {
                Console.WriteLine($"\n--- Nhân vật thứ {i + 1} ---");
                NhanVat nv = new NhanVat();

                nv.MaNhanVat = NhapMaKhongTrung();
                nv.TenNhanVat = NhapChuoiKhongRong("Tên nhân vật: ");
                nv.LoaiNhanVat = NhapChuoiKhongRong("Loại nhân vật: ");
                nv.CapDo = NhapSoNguyenKhongAm("Cấp độ: ");
                nv.Mau = NhapSoNguyenKhongAm("Máu: ");
                nv.SucTanCong = NhapSoNguyenKhongAm("Sức tấn công: ");
                nv.PhongThu = NhapSoNguyenKhongAm("Phòng thủ: ");

                // Thay vì dùng .Add(nv), ta dùng .Insert(0, nv)
                // Điều này sẽ đẩy các nhân vật cũ xuống dưới và đưa người mới lên đầu
                danhSachNhanVat.Insert(0, nv); 
        
                Console.WriteLine("-> Thêm nhân vật thành công!");
            }
        }


        static void HienThiDanhSach()
        {
            if (!KiemTraDanhSachRong()) return;

            Console.WriteLine($"{"Mã",-8}{"Tên",-15}{"Loại",-12}{"Cấp",-6}{"Máu",-6}{"ATK",-8}{"DEF",-8}{"Power",-8}");
            Console.WriteLine(new string('-', 70));

            // Sử dụng .AsEnumerable().Reverse() để hiển thị nhân vật mới nhất lên trên
            foreach (var nv in danhSachNhanVat.AsEnumerable().Reverse())
            {
                nv.HienThi();
            }
        }


        static void TimTheoMa()
        {
            if (!KiemTraDanhSachRong()) return;

            string ma = NhapChuoiKhongRong("Nhập mã nhân vật cần tìm: ");
            NhanVat nv = danhSachNhanVat.FirstOrDefault(x => x.MaNhanVat.Equals(ma, StringComparison.OrdinalIgnoreCase));

            if (nv == null)
            {
                Console.WriteLine("Không tìm thấy nhân vật có mã: " + ma);
            }
            else
            {
                Console.WriteLine($"{"Mã",-8}{"Tên",-15}{"Loại",-12}{"Cấp",-6}{"Máu",-6}{"ATK",-8}{"DEF",-8}{"Power",-8}");
                nv.HienThi();
            }
        }


        static void TimSucTanCongLonNhat()
        {
            if (!KiemTraDanhSachRong()) return;

            NhanVat manhNhat = danhSachNhanVat.OrderByDescending(x => x.SucTanCong).First();

            Console.WriteLine("Nhân vật có sức tấn công lớn nhất:");
            Console.WriteLine($"{"Mã",-8}{"Tên",-15}{"Loại",-12}{"Cấp",-6}{"Máu",-6}{"ATK",-8}{"DEF",-8}{"Power",-8}");
            manhNhat.HienThi();
        }

 
        static void SapXepGiamDanTheoCapDo()
        {
            if (!KiemTraDanhSachRong()) return;

            danhSachNhanVat = danhSachNhanVat.OrderByDescending(x => x.CapDo).ToList();
            Console.WriteLine("Đã sắp xếp giảm dần theo cấp độ:");
            HienThiDanhSach();
        }


        static void XoaTheoMa()
        {
            if (!KiemTraDanhSachRong()) return;

            string ma = NhapChuoiKhongRong("Nhập mã nhân vật cần xóa: ");
            NhanVat nv = danhSachNhanVat.FirstOrDefault(x => x.MaNhanVat.Equals(ma, StringComparison.OrdinalIgnoreCase));

            if (nv == null)
            {
                Console.WriteLine("Không tìm thấy nhân vật có mã: " + ma);
                return;
            }

            danhSachNhanVat.Remove(nv);
            Console.WriteLine("Đã xóa nhân vật thành công!");
        }

   
        static void ThongKeTheoLoai()
        {
            if (!KiemTraDanhSachRong()) return;

            var thongKe = danhSachNhanVat
                .GroupBy(x => x.LoaiNhanVat)
                .Select(g => new { Loai = g.Key, SoLuong = g.Count() });

            Console.WriteLine("Thống kê số lượng nhân vật theo loại:");
            foreach (var item in thongKe)
            {
                Console.WriteLine($"- {item.Loai}: {item.SoLuong} nhân vật");
            }
        }


        static void HienThiNhanVatManhHonGiaTri()
        {
            if (!KiemTraDanhSachRong()) return;

            int giaTri = NhapSoNguyen("Nhập giá trị sức mạnh (Power) để so sánh: ");
            var ketQua = danhSachNhanVat.Where(x => x.TinhSucManh() > giaTri).ToList();

            if (ketQua.Count == 0)
            {
                Console.WriteLine($"Không có nhân vật nào có Power lớn hơn {giaTri}.");
                return;
            }

            Console.WriteLine($"Các nhân vật có Power > {giaTri}:");
            Console.WriteLine($"{"Mã",-8}{"Tên",-15}{"Loại",-12}{"Cấp",-6}{"Máu",-6}{"ATK",-8}{"DEF",-8}{"Power",-8}");
            foreach (var nv in ketQua)
            {
                nv.HienThi();
            }
        }


        static bool KiemTraDanhSachRong()
        {
            if (danhSachNhanVat.Count == 0)
            {
                Console.WriteLine("Danh sách nhân vật đang trống!");
                return false;
            }
            return true;
        }

        static string NhapMaKhongTrung()
        {
            string ma;
            while (true)
            {
                ma = NhapChuoiKhongRong("Mã nhân vật: ");
                bool trung = danhSachNhanVat.Any(x => x.MaNhanVat.Equals(ma, StringComparison.OrdinalIgnoreCase));
                if (trung)
                {
                    Console.WriteLine("Mã đã tồn tại! Vui lòng nhập mã khác.");
                }
                else
                {
                    break;
                }
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
                if (string.IsNullOrWhiteSpace(kq))
                {
                    Console.WriteLine("Dữ liệu không được để trống!");
                }
            } while (string.IsNullOrWhiteSpace(kq));
            return kq.Trim();
        }

        static int NhapSoNguyen(string thongBao)
        {
            int kq;
            while (true)
            {
                Console.Write(thongBao);
                string input = Console.ReadLine();
                if (int.TryParse(input, out kq))
                {
                    return kq;
                }
                Console.WriteLine("Vui lòng nhập một số nguyên hợp lệ!");
            }
        }

        static int NhapSoNguyenKhongAm(string thongBao)
        {
            int kq;
            while (true)
            {
                kq = NhapSoNguyen(thongBao);
                if (kq < 0)
                {
                    Console.WriteLine("Giá trị không được âm!");
                }
                else
                {
                    return kq;
                }
            }
        }
    }
}
