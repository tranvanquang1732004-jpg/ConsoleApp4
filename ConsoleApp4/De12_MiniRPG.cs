using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniRPGConsole
{
    class NguoiChoi
    {
        public string Ten { get; set; }
        public int Level { get; set; } = 1;
        public int MaxHealth { get; set; } = 100;
        public int Health { get; set; } = 100;
        public int Attack { get; set; } = 10;
        public int Defense { get; set; } = 5;
        public int Experience { get; set; } = 0;
        public int Gold { get; set; } = 50;
        public int SoThuoc { get; set; } = 2;
        public string VuKhiDangTrangBi { get; set; } = "Tay không";
        public int SoQuaiDaTieuDiet { get; set; } = 0;

        public bool ConSong() => Health > 0;

        public int KinhNghiemCanDe() => Level * 100;

        public void HienThiThongTin()
        {
            Console.WriteLine($"\n=== {Ten} (Lv.{Level}) ===");
            Console.WriteLine($"HP: {Health}/{MaxHealth} | ATK: {Attack} | DEF: {Defense}");
            Console.WriteLine($"EXP: {Experience}/{KinhNghiemCanDe()} | Vàng: {Gold} | Thuốc: {SoThuoc}");
            Console.WriteLine($"Vũ khí đang trang bị: {VuKhiDangTrangBi}");
            Console.WriteLine($"Số quái đã tiêu diệt: {SoQuaiDaTieuDiet}");
        }
    }

    class QuaiVat
    {
        public string Ten { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int GoldThuong { get; set; }
        public int ExpThuong { get; set; }
        public bool ConSong() => Health > 0;
    }

    class VatPhamCuaHang
    {
        public string Ten { get; set; }
        public string Loai { get; set; } // "Thuoc" hoặc "VuKhi"
        public int Gia { get; set; }
        public int GiaTriCongThem { get; set; } // sát thương nếu là vũ khí
    }

    class Program
    {
        static Random rand = new Random();
        static NguoiChoi nguoiChoi;
        static List<VatPhamCuaHang> cuaHang = new List<VatPhamCuaHang>
        {
            new VatPhamCuaHang { Ten = "Thuốc hồi máu", Loai = "Thuoc", Gia = 20, GiaTriCongThem = 40 },
            new VatPhamCuaHang { Ten = "Kiếm Sắt", Loai = "VuKhi", Gia = 60, GiaTriCongThem = 8 },
            new VatPhamCuaHang { Ten = "Kiếm Thép", Loai = "VuKhi", Gia = 150, GiaTriCongThem = 18 },
            new VatPhamCuaHang { Ten = "Đại Kiếm Rồng", Loai = "VuKhi", Gia = 400, GiaTriCongThem = 35 },
        };

        static void Main(string[] args)
        {
            Console.WriteLine("===== MINI RPG CONSOLE =====");
            Console.Write("Nhập tên nhân vật: ");
            string ten = Console.ReadLine();
            nguoiChoi = new NguoiChoi { Ten = string.IsNullOrWhiteSpace(ten) ? "Anh Hùng" : ten };

            Console.WriteLine($"Chào mừng {nguoiChoi.Ten}! Bạn bắt đầu ở cấp 1.");

            int luaChon;
            do
            {
                if (!nguoiChoi.ConSong())
                {
                    Console.WriteLine("\nBạn đã gục ngã. GAME OVER!");
                    break;
                }

                HienThiMenu();
                luaChon = NhapSoNguyen("Chọn: ");

                switch (luaChon)
                {
                    case 1: nguoiChoi.HienThiThongTin(); break;
                    case 2: ChienDau(); break;
                    case 3: MoTuiDo(); break;
                    case 4: VaoCuaHang(); break;
                    case 5: NghiNgoi(); break;
                    case 6: XemThongKe(); break;
                    case 0: Console.WriteLine("Tạm biệt!"); break;
                    default: Console.WriteLine("Lựa chọn không hợp lệ!"); break;
                }

                if (luaChon != 0 && nguoiChoi.ConSong())
                {
                    Console.WriteLine("\nNhấn phím bất kỳ để tiếp tục...");
                    Console.ReadKey();
                }

            } while (luaChon != 0 && nguoiChoi.ConSong());
        }

        static void HienThiMenu()
        {
            Console.Clear();
            Console.WriteLine($"===== MINI RPG - {nguoiChoi.Ten} (Lv.{nguoiChoi.Level}) HP:{nguoiChoi.Health}/{nguoiChoi.MaxHealth} Vàng:{nguoiChoi.Gold} =====");
            Console.WriteLine("1. Xem thông tin nhân vật");
            Console.WriteLine("2. Chiến đấu");
            Console.WriteLine("3. Mở túi đồ");
            Console.WriteLine("4. Vào cửa hàng");
            Console.WriteLine("5. Nghỉ ngơi");
            Console.WriteLine("6. Xem thống kê");
            Console.WriteLine("0. Thoát");
        }

        static QuaiVat TaoQuaiNgauNhien()
        {
            string[] tenQuai = { "Goblin", "Sói Hoang", "Xác Sống", "Nhện Độc", "Yêu Tinh" };
            int lv = nguoiChoi.Level;
            return new QuaiVat
            {
                Ten = tenQuai[rand.Next(tenQuai.Length)],
                Health = 30 + lv * 15 + rand.Next(0, 10),
                Attack = 5 + lv * 3 + rand.Next(0, 5),
                Defense = 2 + lv * 1,
                GoldThuong = 10 + lv * 5 + rand.Next(0, 10),
                ExpThuong = 20 + lv * 10
            };
        }

        static void ChienDau()
        {
            QuaiVat quai = TaoQuaiNgauNhien();
            Console.WriteLine($"\nMột {quai.Ten} xuất hiện! (HP: {quai.Health}, ATK: {quai.Attack}, DEF: {quai.Defense})");

            while (nguoiChoi.ConSong() && quai.ConSong())
            {
                Console.WriteLine("\n1. Tấn công  2. Dùng thuốc  3. Bỏ chạy");
                int hanhDong = NhapSoNguyen("Chọn: ");

                if (hanhDong == 1)
                {
                    int satThuong = Math.Max(1, nguoiChoi.Attack - quai.Defense);
                    quai.Health = Math.Max(0, quai.Health - satThuong);
                    Console.WriteLine($"Bạn gây {satThuong} sát thương. HP quái còn: {quai.Health}");
                }
                else if (hanhDong == 2)
                {
                    if (nguoiChoi.SoThuoc <= 0) { Console.WriteLine("Bạn đã hết thuốc!"); continue; }
                    nguoiChoi.SoThuoc--;
                    nguoiChoi.Health = Math.Min(nguoiChoi.MaxHealth, nguoiChoi.Health + 40);
                    Console.WriteLine($"Bạn hồi 40 máu. HP hiện tại: {nguoiChoi.Health}/{nguoiChoi.MaxHealth}");
                }
                else if (hanhDong == 3)
                {
                    Console.WriteLine("Bạn đã bỏ chạy khỏi trận đấu!");
                    return;
                }
                else
                {
                    Console.WriteLine("Lựa chọn không hợp lệ!");
                    continue;
                }

                if (!quai.ConSong())
                {
                    Console.WriteLine($"\nBạn đã tiêu diệt {quai.Ten}! Nhận {quai.GoldThuong} vàng và {quai.ExpThuong} EXP.");
                    nguoiChoi.Gold += quai.GoldThuong;
                    nguoiChoi.Experience += quai.ExpThuong;
                    nguoiChoi.SoQuaiDaTieuDiet++;
                    KiemTraTangCap();
                    return;
                }

                // Quái đánh trả
                int satThuongQuai = Math.Max(1, quai.Attack - nguoiChoi.Defense);
                nguoiChoi.Health = Math.Max(0, nguoiChoi.Health - satThuongQuai);
                Console.WriteLine($"{quai.Ten} gây {satThuongQuai} sát thương lên bạn. HP còn: {nguoiChoi.Health}/{nguoiChoi.MaxHealth}");

                if (!nguoiChoi.ConSong())
                {
                    Console.WriteLine("\nBạn đã bị đánh bại!");
                    return;
                }
            }
        }

        static void KiemTraTangCap()
        {
            while (nguoiChoi.Experience >= nguoiChoi.KinhNghiemCanDe())
            {
                nguoiChoi.Experience -= nguoiChoi.KinhNghiemCanDe();
                nguoiChoi.Level++;
                nguoiChoi.MaxHealth += 20;
                nguoiChoi.Attack += 5;
                nguoiChoi.Defense += 3;
                nguoiChoi.Health = nguoiChoi.MaxHealth; // hồi đầy máu khi lên cấp
                Console.WriteLine($"\n*** CHÚC MỪNG! Bạn đã lên cấp {nguoiChoi.Level}! ***");
                Console.WriteLine($"MaxHP +20, ATK +5, DEF +3. HP đã được hồi đầy.");
            }
        }

        static void MoTuiDo()
        {
            Console.WriteLine($"\nSố thuốc hồi máu: {nguoiChoi.SoThuoc}");
            Console.WriteLine($"Vũ khí đang trang bị: {nguoiChoi.VuKhiDangTrangBi}");
        }

        static void VaoCuaHang()
        {
            Console.WriteLine($"\n===== CỬA HÀNG ===== (Vàng của bạn: {nguoiChoi.Gold})");
            for (int i = 0; i < cuaHang.Count; i++)
            {
                var vp = cuaHang[i];
                string moTa = vp.Loai == "Thuoc" ? $"Hồi {vp.GiaTriCongThem} máu" : $"Sát thương +{vp.GiaTriCongThem}";
                Console.WriteLine($"{i + 1}. {vp.Ten} - Giá: {vp.Gia} vàng ({moTa})");
            }
            Console.WriteLine("0. Quay lại");

            int chon = NhapSoNguyen("Chọn vật phẩm muốn mua: ");
            if (chon == 0) return;
            if (chon < 1 || chon > cuaHang.Count) { Console.WriteLine("Lựa chọn không hợp lệ!"); return; }

            VatPhamCuaHang vatPham = cuaHang[chon - 1];
            if (nguoiChoi.Gold < vatPham.Gia)
            {
                Console.WriteLine("Bạn không đủ vàng để mua vật phẩm này!");
                return;
            }

            nguoiChoi.Gold -= vatPham.Gia;

            if (vatPham.Loai == "Thuoc")
            {
                nguoiChoi.SoThuoc++;
                Console.WriteLine($"-> Đã mua {vatPham.Ten}. Số thuốc hiện tại: {nguoiChoi.SoThuoc}");
            }
            else
            {
                nguoiChoi.Attack += vatPham.GiaTriCongThem;
                nguoiChoi.VuKhiDangTrangBi = vatPham.Ten;
                Console.WriteLine($"-> Đã mua và trang bị {vatPham.Ten}. ATK hiện tại: {nguoiChoi.Attack}");
            }
        }

        static void NghiNgoi()
        {
            nguoiChoi.Health = nguoiChoi.MaxHealth;
            Console.WriteLine($"-> Bạn đã nghỉ ngơi và hồi đầy máu. HP: {nguoiChoi.Health}/{nguoiChoi.MaxHealth}");
        }

        static void XemThongKe()
        {
            Console.WriteLine($"\nSố quái đã tiêu diệt: {nguoiChoi.SoQuaiDaTieuDiet}");
            Console.WriteLine($"Cấp độ hiện tại: {nguoiChoi.Level}");
            Console.WriteLine($"Vàng hiện có: {nguoiChoi.Gold}");
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
