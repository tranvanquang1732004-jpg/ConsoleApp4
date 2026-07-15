using System;
using System.Collections.Generic;
using System.Linq;

namespace HeThongTrangBi
{
    enum ViTriTrangBi
    {
        Weapon,
        Helmet,
        Armor,
        Shoes
    }

    class TrangBi
    {
        public string Ma { get; set; }
        public string Ten { get; set; }
        public ViTriTrangBi Loai { get; set; }
        public int TanCongCongThem { get; set; }
        public int PhongThuCongThem { get; set; }
        public double GiaTri { get; set; }

        public void HienThi()
        {
            Console.WriteLine($"{Ma,-6}{Ten,-18}{Loai,-10}{TanCongCongThem,-8}{PhongThuCongThem,-8}{GiaTri,-10:N0}");
        }
    }

    class Program
    {
        static List<TrangBi> tuiDo = new List<TrangBi>();
        static Dictionary<ViTriTrangBi, TrangBi> dangTrangBi = new Dictionary<ViTriTrangBi, TrangBi>();

        static void Main(string[] args)
        {
            int luaChon;
            do
            {
                HienThiMenu();
                luaChon = NhapSoNguyen("Chọn chức năng: ");
                switch (luaChon)
                {
                    case 1: ThemVaoTuiDo(); break;
                    case 2: HienThiTuiDo(); break;
                    case 3: TrangBiVatPham(); break;
                    case 4: ThaoTrangBi(); break;
                    case 5: TinhTongChiSo(); break;
                    case 6: HienThiDangTrangBi(); break;
                    case 7: TimTrangBiTotNhatMoiViTri(); break;
                    case 8: TinhTongGiaTriTaiSan(); break;
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
            Console.WriteLine("===== HỆ THỐNG TRANG BỊ NHÂN VẬT =====");
            Console.WriteLine("1. Thêm trang bị vào túi đồ");
            Console.WriteLine("2. Hiển thị túi đồ");
            Console.WriteLine("3. Trang bị một vật phẩm");
            Console.WriteLine("4. Tháo trang bị");
            Console.WriteLine("5. Tính tổng chỉ số nhân vật sau khi trang bị");
            Console.WriteLine("6. Hiển thị trang bị đang sử dụng");
            Console.WriteLine("7. Tìm trang bị tốt nhất cho từng vị trí");
            Console.WriteLine("8. Tính tổng giá trị toàn bộ tài sản");
            Console.WriteLine("0. Thoát");
        }

        static void InTieuDe()
        {
            Console.WriteLine($"{"Mã",-6}{"Tên",-18}{"Vị trí",-10}{"ATK+",-8}{"DEF+",-8}{"Giá trị",-10}");
        }

        static void ThemVaoTuiDo()
        {
            TrangBi tb = new TrangBi();
            tb.Ma = NhapMaKhongTrung();
            tb.Ten = NhapChuoiKhongRong("Tên trang bị: ");
            tb.Loai = NhapEnum<ViTriTrangBi>("Vị trí (Weapon/Helmet/Armor/Shoes): ");
            tb.TanCongCongThem = NhapSoNguyenKhongAm("Tấn công cộng thêm: ");
            tb.PhongThuCongThem = NhapSoNguyenKhongAm("Phòng thủ cộng thêm: ");
            tb.GiaTri = NhapSoThucKhongAm("Giá trị: ");
            tuiDo.Add(tb);
            Console.WriteLine("-> Thêm trang bị vào túi đồ thành công!");
        }

        static void HienThiTuiDo()
        {
            if (tuiDo.Count == 0) { Console.WriteLine("Túi đồ đang trống!"); return; }
            InTieuDe();
            foreach (var tb in tuiDo) tb.HienThi();
        }

        static void TrangBiVatPham()
        {
            if (tuiDo.Count == 0) { Console.WriteLine("Túi đồ đang trống!"); return; }
            string ma = NhapChuoiKhongRong("Nhập mã trang bị muốn mặc: ");
            TrangBi tb = tuiDo.FirstOrDefault(x => x.Ma.Equals(ma, StringComparison.OrdinalIgnoreCase));
            if (tb == null) { Console.WriteLine("Không tìm thấy trang bị này trong túi đồ!"); return; }

            // Nếu vị trí đó đã có trang bị -> trả về túi đồ
            if (dangTrangBi.ContainsKey(tb.Loai))
            {
                TrangBi cu = dangTrangBi[tb.Loai];
                tuiDo.Add(cu);
                Console.WriteLine($"-> {cu.Ten} đã được tháo ra và trả về túi đồ.");
            }

            dangTrangBi[tb.Loai] = tb;
            tuiDo.Remove(tb);
            Console.WriteLine($"-> Đã trang bị {tb.Ten} vào vị trí {tb.Loai}.");
        }

        static void ThaoTrangBi()
        {
            if (dangTrangBi.Count == 0) { Console.WriteLine("Bạn chưa trang bị vật phẩm nào!"); return; }
            ViTriTrangBi viTri = NhapEnum<ViTriTrangBi>("Nhập vị trí muốn tháo (Weapon/Helmet/Armor/Shoes): ");

            if (!dangTrangBi.ContainsKey(viTri))
            {
                Console.WriteLine("Vị trí này chưa trang bị vật phẩm nào!");
                return;
            }

            TrangBi tb = dangTrangBi[viTri];
            tuiDo.Add(tb);
            dangTrangBi.Remove(viTri);
            Console.WriteLine($"-> Đã tháo {tb.Ten}, trả về túi đồ.");
        }

        static void TinhTongChiSo()
        {
            int tongAtk = dangTrangBi.Values.Sum(x => x.TanCongCongThem);
            int tongDef = dangTrangBi.Values.Sum(x => x.PhongThuCongThem);
            Console.WriteLine($"Tổng tấn công cộng thêm từ trang bị: {tongAtk}");
            Console.WriteLine($"Tổng phòng thủ cộng thêm từ trang bị: {tongDef}");
        }

        static void HienThiDangTrangBi()
        {
            if (dangTrangBi.Count == 0) { Console.WriteLine("Bạn chưa trang bị vật phẩm nào!"); return; }
            InTieuDe();
            foreach (var tb in dangTrangBi.Values) tb.HienThi();
        }

        static void TimTrangBiTotNhatMoiViTri()
        {
            var tatCa = tuiDo.Concat(dangTrangBi.Values).ToList();
            if (tatCa.Count == 0) { Console.WriteLine("Chưa có trang bị nào!"); return; }

            InTieuDe();
            foreach (ViTriTrangBi viTri in Enum.GetValues(typeof(ViTriTrangBi)))
            {
                var cungViTri = tatCa.Where(x => x.Loai == viTri).ToList();
                if (cungViTri.Count == 0) continue;

                // "Tốt nhất" tính theo tổng ATK+DEF cộng thêm
                TrangBi totNhat = cungViTri.OrderByDescending(x => x.TanCongCongThem + x.PhongThuCongThem).First();
                totNhat.HienThi();
            }
        }

        static void TinhTongGiaTriTaiSan()
        {
            double tong = tuiDo.Sum(x => x.GiaTri) + dangTrangBi.Values.Sum(x => x.GiaTri);
            Console.WriteLine($"Tổng giá trị toàn bộ tài sản (túi đồ + đang trang bị): {tong:N0}");
        }

        static string NhapMaKhongTrung()
        {
            string ma;
            while (true)
            {
                ma = NhapChuoiKhongRong("Mã trang bị: ");
                bool trung = tuiDo.Any(x => x.Ma.Equals(ma, StringComparison.OrdinalIgnoreCase))
                          || dangTrangBi.Values.Any(x => x.Ma.Equals(ma, StringComparison.OrdinalIgnoreCase));
                if (trung) Console.WriteLine("Mã đã tồn tại! Vui lòng nhập mã khác.");
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

        static T NhapEnum<T>(string thongBao) where T : struct, Enum
        {
            while (true)
            {
                Console.Write(thongBao);
                string input = Console.ReadLine();
                if (Enum.TryParse<T>(input, true, out T ketQua) && Enum.IsDefined(typeof(T), ketQua))
                    return ketQua;
                string cacGiaTri = string.Join(", ", Enum.GetNames(typeof(T)));
                Console.WriteLine($"Giá trị không hợp lệ! Vui lòng chọn trong: {cacGiaTri}");
            }
        }
    }
}
