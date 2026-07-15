using System;

namespace ChienDauTheoLuot
{
    class NguoiChoi
    {
        public string Ten { get; set; }
        public int MauToiDa { get; set; }
        public int MauHienTai { get; set; }
        public int SucTanCong { get; set; }
        public int PhongThu { get; set; }
        public int SoThuocHoiMau { get; set; }
        public int Vang { get; set; }

        public bool ConSong() => MauHienTai > 0;

        public void HienThiTrangThai()
        {
            Console.WriteLine($"{Ten}: HP {MauHienTai}/{MauToiDa} | ATK {SucTanCong} | DEF {PhongThu} | Thuốc: {SoThuocHoiMau} | Vàng: {Vang}");
        }
    }

    class QuaiVat
    {
        public string Ten { get; set; }
        public int Mau { get; set; }
        public int SucTanCong { get; set; }
        public int PhongThu { get; set; }
        public int VangThuong { get; set; }

        public bool ConSong() => Mau > 0;

        public void HienThiTrangThai()
        {
            Console.WriteLine($"{Ten}: HP {Mau} | ATK {SucTanCong} | DEF {PhongThu} | Vàng thưởng: {VangThuong}");
        }
    }

    class Program
    {
        static Random rand = new Random();
        static NguoiChoi nguoiChoi;
        static int soTranThang = 0;
        static int soTranThua = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("===== GAME CHIẾN ĐẤU THEO LƯỢT =====");
            TaoNguoiChoi();

            int luaChon;
            do
            {
                Console.WriteLine("\n--- MENU CHÍNH ---");
                Console.WriteLine("1. Bắt đầu trận đấu mới");
                Console.WriteLine("2. Xem thống kê");
                Console.WriteLine("0. Thoát");
                luaChon = NhapSoNguyen("Chọn: ");

                switch (luaChon)
                {
                    case 1: BatDauTran(); break;
                    case 2: XemThongKe(); break;
                    case 0: Console.WriteLine("Tạm biệt!"); break;
                    default: Console.WriteLine("Lựa chọn không hợp lệ!"); break;
                }
            } while (luaChon != 0 && nguoiChoi.ConSong());

            if (!nguoiChoi.ConSong())
                Console.WriteLine("\nNhân vật của bạn đã gục ngã. Trò chơi kết thúc!");
        }

        static void TaoNguoiChoi()
        {
            nguoiChoi = new NguoiChoi();
            nguoiChoi.Ten = NhapChuoiKhongRong("Nhập tên nhân vật: ");
            nguoiChoi.MauToiDa = 100;
            nguoiChoi.MauHienTai = 100;
            nguoiChoi.SucTanCong = 15;
            nguoiChoi.PhongThu = 5;
            nguoiChoi.SoThuocHoiMau = 3;
            nguoiChoi.Vang = 0;
            Console.WriteLine($"Chào mừng {nguoiChoi.Ten}! Bạn bắt đầu với HP 100, ATK 15, DEF 5, 3 thuốc hồi máu.");
        }

        static QuaiVat TaoQuaiNgauNhien()
        {
            string[] tenQuai = { "Goblin", "Orc", "Slime", "Zombie", "Bat quái" };
            string ten = tenQuai[rand.Next(tenQuai.Length)];
            return new QuaiVat
            {
                Ten = ten,
                Mau = rand.Next(40, 81),
                SucTanCong = rand.Next(8, 18),
                PhongThu = rand.Next(2, 8),
                VangThuong = rand.Next(10, 51)
            };
        }

        static void BatDauTran()
        {
            if (!nguoiChoi.ConSong())
            {
                Console.WriteLine("Bạn đã gục ngã, không thể chiến đấu tiếp!");
                return;
            }

            QuaiVat quai = TaoQuaiNgauNhien();
            Console.WriteLine($"\nMột {quai.Ten} xuất hiện!");
            quai.HienThiTrangThai();

            bool boChay = false;

            while (nguoiChoi.ConSong() && quai.ConSong() && !boChay)
            {
                Console.WriteLine("\n--- Lượt của bạn ---");
                Console.WriteLine("1. Tấn công");
                Console.WriteLine("2. Hồi máu");
                Console.WriteLine("3. Xem trạng thái");
                Console.WriteLine("4. Bỏ chạy");
                int hanhDong = NhapSoNguyen("Chọn hành động: ");

                switch (hanhDong)
                {
                    case 1:
                        int quaiPhongThu = quai.PhongThu;
                        TanCong(nguoiChoi.SucTanCong, nguoiChoi.Ten, quai.Ten, ref quaiPhongThu, quai);
                        break;
                    case 2:
                        HoiMau();
                        continue;
                    case 3:
                        nguoiChoi.HienThiTrangThai();
                        quai.HienThiTrangThai();
                        continue;
                    case 4:
                        Console.WriteLine("Bạn đã bỏ chạy khỏi trận đấu!");
                        boChay = true;
                        continue;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        continue;
                }

                if (!quai.ConSong())
                {
                    Console.WriteLine($"\nBạn đã tiêu diệt {quai.Ten}! Nhận được {quai.VangThuong} vàng.");
                    nguoiChoi.Vang += quai.VangThuong;
                    soTranThang++;
                    return;
                }

                // Quái tấn công lại
                Console.WriteLine($"\n--- Lượt của {quai.Ten} ---");
                int sat = TinhSatThuong(quai.SucTanCong, nguoiChoi.PhongThu);
                nguoiChoi.MauHienTai = Math.Max(0, nguoiChoi.MauHienTai - sat);
                Console.WriteLine($"{quai.Ten} gây {sat} sát thương lên bạn. HP còn lại: {nguoiChoi.MauHienTai}");

                if (!nguoiChoi.ConSong())
                {
                    Console.WriteLine("\nBạn đã bị đánh bại!");
                    soTranThua++;
                    return;
                }
            }
        }

        static void TanCong(int atkNguoiChoi, string tenNguoiChoi, string tenQuai, ref int defQuai, QuaiVat quai)
        {
            int satThuong = TinhSatThuong(atkNguoiChoi, quai.PhongThu);

            bool chiMang = rand.Next(100) < 20;
            if (chiMang)
            {
                satThuong *= 2;
                Console.WriteLine("CHÍ MẠNG!");
            }

            quai.Mau = Math.Max(0, quai.Mau - satThuong);
            Console.WriteLine($"Bạn gây {satThuong} sát thương lên {tenQuai}. HP quái còn lại: {quai.Mau}");
        }

        static int TinhSatThuong(int atk, int def)
        {
            int sat = atk - def;
            return sat < 1 ? 1 : sat;
        }

        static void HoiMau()
        {
            if (nguoiChoi.SoThuocHoiMau <= 0)
            {
                Console.WriteLine("Bạn đã hết thuốc hồi máu!");
                return;
            }

            nguoiChoi.SoThuocHoiMau--;
            nguoiChoi.MauHienTai = Math.Min(nguoiChoi.MauToiDa, nguoiChoi.MauHienTai + 30);
            Console.WriteLine($"Bạn đã hồi 30 máu. HP hiện tại: {nguoiChoi.MauHienTai}/{nguoiChoi.MauToiDa}. Còn {nguoiChoi.SoThuocHoiMau} thuốc.");
        }

        static void XemThongKe()
        {
            Console.WriteLine($"\nSố trận thắng: {soTranThang}");
            Console.WriteLine($"Số trận thua: {soTranThua}");
            nguoiChoi.HienThiTrangThai();
        }

        static string NhapChuoiKhongRong(string thongBao)
        {
            string kq;
            do
            {
                Console.Write(thongBao);
                kq = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(kq))
                    Console.WriteLine("Dữ liệu không được để trống!");
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
