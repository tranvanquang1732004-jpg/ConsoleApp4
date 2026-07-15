using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyKhoVatPham
{

    enum LoaiVatPham
    {
        Weapon,
        Armor,
        Potion,
        Material
    }

    enum DoHiem
    {
        Common,
        Uncommon,
        Rare,
        Legendary
    }


    class VatPham
    {
        public string MaVatPham { get; set; }
        public string TenVatPham { get; set; }
        public LoaiVatPham Loai { get; set; }
        public int SoLuong { get; set; }
        public double Gia { get; set; }
        public DoHiem DoHiem { get; set; }

        public double TinhTongGiaTri()
        {
            return Gia * SoLuong;
        }

        public void HienThi()
        {
            Console.WriteLine(
                $"{MaVatPham,-8}{TenVatPham,-18}{Loai,-10}{SoLuong,-8}{Gia,-10:N0}{DoHiem,-10}{TinhTongGiaTri(),-12:N0}"
            );
        }
    }

    class Program
    {
        static List<VatPham> khoVatPham = new List<VatPham>();

        static void Main(string[] args)
        {
            int luaChon;
            do
            {
                HienThiMenu();
                luaChon = NhapSoNguyen("Chọn chức năng: ");

                switch (luaChon)
                {
                    case 1: ThemVatPham(); break;
                    case 2: HienThiDanhSach(); break;
                    case 3: CapNhatSoLuong(); break;
                    case 4: XoaVatPhamHetHang(); break;
                    case 5: TimKiemGanDung(); break;
                    case 6: HienThiGiaCaoNhat(); break;
                    case 7: TinhTongGiaTriKho(); break;
                    case 8: SapXepTangDanTheoGia(); break;
                    case 9: ThongKeSoLuongTheoLoai(); break;
                    case 10: HienThiRareVaLegendary(); break;
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
            Console.WriteLine("===== QUẢN LÝ KHO VẬT PHẨM =====");
            Console.WriteLine("1. Thêm vật phẩm mới");
            Console.WriteLine("2. Hiển thị danh sách vật phẩm");
            Console.WriteLine("3. Cập nhật số lượng theo mã");
            Console.WriteLine("4. Xóa vật phẩm đã hết số lượng");
            Console.WriteLine("5. Tìm kiếm vật phẩm theo tên gần đúng");
            Console.WriteLine("6. Hiển thị vật phẩm có giá cao nhất");
            Console.WriteLine("7. Tính tổng giá trị kho");
            Console.WriteLine("8. Sắp xếp tăng dần theo giá");
            Console.WriteLine("9. Thống kê tổng số lượng theo loại");
            Console.WriteLine("10. Hiển thị vật phẩm Rare hoặc Legendary");
            Console.WriteLine("0. Thoát");
            Console.WriteLine("=================================");
        }

    
        static void ThemVatPham()
        {
            string ma = NhapChuoiKhongRong("Mã vật phẩm: ");
            VatPham daTonTai = khoVatPham.FirstOrDefault(x => x.MaVatPham.Equals(ma, StringComparison.OrdinalIgnoreCase));

            if (daTonTai != null)
            {
                int themSoLuong = NhapSoNguyenDuong("Mã đã tồn tại. Nhập số lượng muốn cộng thêm: ");
                daTonTai.SoLuong += themSoLuong;
                Console.WriteLine($"-> Đã cộng thêm {themSoLuong} vào vật phẩm '{daTonTai.TenVatPham}'. Số lượng hiện tại: {daTonTai.SoLuong}");
                return;
            }

            VatPham vp = new VatPham();
            vp.MaVatPham = ma;
            vp.TenVatPham = NhapChuoiKhongRong("Tên vật phẩm: ");
            vp.Loai = NhapEnum<LoaiVatPham>("Loại vật phẩm (Weapon/Armor/Potion/Material): ");
            vp.SoLuong = NhapSoNguyenDuong("Số lượng: ");
            vp.Gia = NhapSoThucKhongAm("Giá: ");
            vp.DoHiem = NhapEnum<DoHiem>("Độ hiếm (Common/Uncommon/Rare/Legendary): ");

            khoVatPham.Add(vp);
            Console.WriteLine("-> Thêm vật phẩm thành công!");
        }

 
        static void HienThiDanhSach()
        {
            if (!KiemTraKhoRong()) return;

            InTieuDe();
            foreach (var vp in khoVatPham)
            {
                vp.HienThi();
            }
        }

        static void InTieuDe()
        {
            Console.WriteLine($"{"Mã",-8}{"Tên",-18}{"Loại",-10}{"SL",-8}{"Giá",-10}{"Độ hiếm",-10}{"Tổng giá trị",-12}");
            Console.WriteLine(new string('-', 76));
        }


        static void CapNhatSoLuong()
        {
            if (!KiemTraKhoRong()) return;

            string ma = NhapChuoiKhongRong("Nhập mã vật phẩm cần cập nhật: ");
            VatPham vp = TimTheoMa(ma);

            if (vp == null)
            {
                Console.WriteLine("Không tìm thấy vật phẩm có mã: " + ma);
                return;
            }

            int soLuongMoi = NhapSoNguyenKhongAm("Nhập số lượng mới: ");
            vp.SoLuong = soLuongMoi;
            Console.WriteLine("-> Cập nhật thành công!");
        }

        static void XoaVatPhamHetHang()
        {
            if (!KiemTraKhoRong()) return;

            int soLuongXoa = khoVatPham.RemoveAll(x => x.SoLuong == 0);

            if (soLuongXoa == 0)
                Console.WriteLine("Không có vật phẩm nào hết hàng.");
            else
                Console.WriteLine($"-> Đã xóa {soLuongXoa} vật phẩm hết hàng.");
        }


        static void TimKiemGanDung()
        {
            if (!KiemTraKhoRong()) return;

            string tuKhoa = NhapChuoiKhongRong("Nhập tên (hoặc một phần tên) cần tìm: ");
            var ketQua = khoVatPham
                .Where(x => x.TenVatPham.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (ketQua.Count == 0)
            {
                Console.WriteLine("Không tìm thấy vật phẩm nào phù hợp.");
                return;
            }

            InTieuDe();
            foreach (var vp in ketQua)
            {
                vp.HienThi();
            }
        }

        static void HienThiGiaCaoNhat()
        {
            if (!KiemTraKhoRong()) return;

            VatPham caoNhat = khoVatPham.OrderByDescending(x => x.Gia).First();
            InTieuDe();
            caoNhat.HienThi();
        }


        static void TinhTongGiaTriKho()
        {
            if (!KiemTraKhoRong()) return;

            double tong = khoVatPham.Sum(x => x.TinhTongGiaTri());
            Console.WriteLine($"Tổng giá trị toàn bộ kho: {tong:N0}");
        }


        static void SapXepTangDanTheoGia()
        {
            if (!KiemTraKhoRong()) return;

            khoVatPham = khoVatPham.OrderBy(x => x.Gia).ToList();
            Console.WriteLine("Đã sắp xếp tăng dần theo giá:");
            HienThiDanhSach();
        }


        static void ThongKeSoLuongTheoLoai()
        {
            if (!KiemTraKhoRong()) return;

            var thongKe = khoVatPham
                .GroupBy(x => x.Loai)
                .Select(g => new { Loai = g.Key, TongSoLuong = g.Sum(x => x.SoLuong) });

            Console.WriteLine("Thống kê tổng số lượng theo loại:");
            foreach (var item in thongKe)
            {
                Console.WriteLine($"- {item.Loai}: {item.TongSoLuong}");
            }
        }


        static void HienThiRareVaLegendary()
        {
            if (!KiemTraKhoRong()) return;

            var ketQua = khoVatPham
                .Where(x => x.DoHiem == DoHiem.Rare || x.DoHiem == DoHiem.Legendary)
                .ToList();

            if (ketQua.Count == 0)
            {
                Console.WriteLine("Không có vật phẩm Rare hoặc Legendary nào.");
                return;
            }

            InTieuDe();
            foreach (var vp in ketQua)
            {
                vp.HienThi();
            }
        }

        static VatPham TimTheoMa(string ma)
        {
            return khoVatPham.FirstOrDefault(x => x.MaVatPham.Equals(ma, StringComparison.OrdinalIgnoreCase));
        }

        static bool KiemTraKhoRong()
        {
            if (khoVatPham.Count == 0)
            {
                Console.WriteLine("Kho vật phẩm đang trống!");
                return false;
            }
            return true;
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
                    Console.WriteLine("Giá trị không được âm!");
                else
                    return kq;
            }
        }

        static int NhapSoNguyenDuong(string thongBao)
        {
            int kq;
            while (true)
            {
                kq = NhapSoNguyen(thongBao);
                if (kq <= 0)
                    Console.WriteLine("Giá trị phải lớn hơn 0!");
                else
                    return kq;
            }
        }

        static double NhapSoThucKhongAm(string thongBao)
        {
            double kq;
            while (true)
            {
                Console.Write(thongBao);
                string input = Console.ReadLine();
                if (double.TryParse(input, out kq) && kq >= 0)
                {
                    return kq;
                }
                Console.WriteLine("Vui lòng nhập một số hợp lệ (không âm)!");
            }
        }

        static T NhapEnum<T>(string thongBao) where T : struct, Enum
        {
            while (true)
            {
                Console.Write(thongBao);
                string input = Console.ReadLine();
                if (Enum.TryParse<T>(input, true, out T ketQua) && Enum.IsDefined(typeof(T), ketQua))
                {
                    return ketQua;
                }
                string cacGiaTri = string.Join(", ", Enum.GetNames(typeof(T)));
                Console.WriteLine($"Giá trị không hợp lệ! Vui lòng chọn trong: {cacGiaTri}");
            }
        }
    }
}
