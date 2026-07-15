using System;
using System.Collections.Generic;
using System.Linq;

namespace HeThongNhiemVu
{
    enum TrangThaiNhiemVu
    {
        NotStarted,
        InProgress,
        Completed,
        Claimed
    }

    class NhiemVu
    {
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public int MucTieu { get; set; }
        public int TienDoHienTai { get; set; }
        public int PhanThuongVang { get; set; }
        public TrangThaiNhiemVu TrangThai { get; set; } = TrangThaiNhiemVu.NotStarted;

        public void HienThi()
        {
            Console.WriteLine($"{Ma,-6}{Ten,-20}{TienDoHienTai}/{MucTieu,-8}{PhanThuongVang,-10}{TrangThai,-12}");
        }
    }

    class Program
    {
        static List<NhiemVu> danhSach = new List<NhiemVu>();

        static void Main(string[] args)
        {
            int luaChon;
            do
            {
                HienThiMenu();
                luaChon = NhapSoNguyen("Chọn chức năng: ");
                switch (luaChon)
                {
                    case 1: ThemNhiemVu(); break;
                    case 2: HienThiDanhSach(); break;
                    case 3: NhanNhiemVu(); break;
                    case 4: CapNhatTienDo(); break;
                    case 5: NhanPhanThuong(); break;
                    case 6: HienThiDangThucHien(); break;
                    case 7: HienThiHoanThanhChuaNhanThuong(); break;
                    case 8: TinhTongVangDaNhan(); break;
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
            Console.WriteLine("===== HỆ THỐNG NHIỆM VỤ =====");
            Console.WriteLine("1. Thêm nhiệm vụ mới");
            Console.WriteLine("2. Hiển thị danh sách nhiệm vụ");
            Console.WriteLine("3. Nhận nhiệm vụ");
            Console.WriteLine("4. Cập nhật tiến độ");
            Console.WriteLine("5. Nhận phần thưởng");
            Console.WriteLine("6. Hiển thị nhiệm vụ đang thực hiện");
            Console.WriteLine("7. Hiển thị nhiệm vụ hoàn thành chưa nhận thưởng");
            Console.WriteLine("8. Tính tổng vàng đã nhận");
            Console.WriteLine("0. Thoát");
        }

        static void ThemNhiemVu()
        {
            NhiemVu nv = new NhiemVu();
            nv.Ma = NhapMaKhongTrung();
            nv.Ten = NhapChuoiKhongRong("Tên nhiệm vụ: ");
            nv.MoTa = NhapChuoiKhongRong("Mô tả: ");
            nv.MucTieu = NhapSoNguyenDuong("Mục tiêu (số): ");
            nv.PhanThuongVang = NhapSoNguyenKhongAm("Phần thưởng vàng: ");
            nv.TienDoHienTai = 0;
            nv.TrangThai = TrangThaiNhiemVu.NotStarted;
            danhSach.Add(nv);
            Console.WriteLine("-> Thêm nhiệm vụ thành công!");
        }

        static void InTieuDe()
        {
            Console.WriteLine($"{"Mã",-6}{"Tên",-20}{"Tiến độ",-8}{"Thưởng",-10}{"Trạng thái",-12}");
        }

        static void HienThiDanhSach()
        {
            if (!KiemTraRong()) return;
            InTieuDe();
            foreach (var nv in danhSach) nv.HienThi();
        }

        static void NhanNhiemVu()
        {
            if (!KiemTraRong()) return;
            string ma = NhapChuoiKhongRong("Nhập mã nhiệm vụ muốn nhận: ");
            NhiemVu nv = TimTheoMa(ma);
            if (nv == null) { Console.WriteLine("Không tìm thấy nhiệm vụ!"); return; }
            if (nv.TrangThai != TrangThaiNhiemVu.NotStarted)
            {
                Console.WriteLine("Nhiệm vụ này đã được nhận hoặc đã hoàn thành trước đó!");
                return;
            }
            nv.TrangThai = TrangThaiNhiemVu.InProgress;
            Console.WriteLine("-> Đã nhận nhiệm vụ!");
        }

        static void CapNhatTienDo()
        {
            if (!KiemTraRong()) return;
            string ma = NhapChuoiKhongRong("Nhập mã nhiệm vụ: ");
            NhiemVu nv = TimTheoMa(ma);
            if (nv == null) { Console.WriteLine("Không tìm thấy nhiệm vụ!"); return; }

            if (nv.TrangThai == TrangThaiNhiemVu.NotStarted)
            {
                Console.WriteLine("Không thể cập nhật nhiệm vụ chưa bắt đầu!");
                return;
            }
            if (nv.TrangThai == TrangThaiNhiemVu.Claimed)
            {
                Console.WriteLine("Nhiệm vụ đã nhận thưởng, không thể cập nhật tiếp!");
                return;
            }
            if (nv.TrangThai == TrangThaiNhiemVu.Completed)
            {
                Console.WriteLine("Nhiệm vụ đã hoàn thành, hãy nhận thưởng!");
                return;
            }

            int themTienDo = NhapSoNguyenDuong("Nhập số tiến độ muốn thêm: ");
            nv.TienDoHienTai = Math.Min(nv.MucTieu, nv.TienDoHienTai + themTienDo);
            Console.WriteLine($"-> Tiến độ hiện tại: {nv.TienDoHienTai}/{nv.MucTieu}");

            if (nv.TienDoHienTai >= nv.MucTieu)
            {
                nv.TrangThai = TrangThaiNhiemVu.Completed;
                Console.WriteLine("-> Nhiệm vụ đã hoàn thành! Hãy vào mục 5 để nhận thưởng.");
            }
        }

        static void NhanPhanThuong()
        {
            if (!KiemTraRong()) return;
            string ma = NhapChuoiKhongRong("Nhập mã nhiệm vụ muốn nhận thưởng: ");
            NhiemVu nv = TimTheoMa(ma);
            if (nv == null) { Console.WriteLine("Không tìm thấy nhiệm vụ!"); return; }

            if (nv.TrangThai == TrangThaiNhiemVu.Claimed)
            {
                Console.WriteLine("Bạn đã nhận thưởng nhiệm vụ này rồi!");
                return;
            }
            if (nv.TrangThai != TrangThaiNhiemVu.Completed)
            {
                Console.WriteLine("Nhiệm vụ chưa hoàn thành, không thể nhận thưởng!");
                return;
            }

            nv.TrangThai = TrangThaiNhiemVu.Claimed;
            Console.WriteLine($"-> Nhận thành công {nv.PhanThuongVang} vàng!");
        }

        static void HienThiDangThucHien()
        {
            if (!KiemTraRong()) return;
            var ketQua = danhSach.Where(x => x.TrangThai == TrangThaiNhiemVu.InProgress).ToList();
            if (ketQua.Count == 0) { Console.WriteLine("Không có nhiệm vụ nào đang thực hiện."); return; }
            InTieuDe();
            foreach (var nv in ketQua) nv.HienThi();
        }

        static void HienThiHoanThanhChuaNhanThuong()
        {
            if (!KiemTraRong()) return;
            var ketQua = danhSach.Where(x => x.TrangThai == TrangThaiNhiemVu.Completed).ToList();
            if (ketQua.Count == 0) { Console.WriteLine("Không có nhiệm vụ nào hoàn thành chưa nhận thưởng."); return; }
            InTieuDe();
            foreach (var nv in ketQua) nv.HienThi();
        }

        static void TinhTongVangDaNhan()
        {
            int tong = danhSach.Where(x => x.TrangThai == TrangThaiNhiemVu.Claimed).Sum(x => x.PhanThuongVang);
            Console.WriteLine($"Tổng số vàng đã nhận: {tong}");
        }

        static NhiemVu TimTheoMa(string ma) => danhSach.FirstOrDefault(x => x.Ma.Equals(ma, StringComparison.OrdinalIgnoreCase));

        static bool KiemTraRong()
        {
            if (danhSach.Count == 0) { Console.WriteLine("Danh sách nhiệm vụ đang trống!"); return false; }
            return true;
        }

        static string NhapMaKhongTrung()
        {
            string ma;
            while (true)
            {
                ma = NhapChuoiKhongRong("Mã nhiệm vụ: ");
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
    }
}
