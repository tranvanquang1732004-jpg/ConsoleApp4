using System;
using System.Collections.Generic;
using System.Linq;

namespace GameBauCua
{
    enum LinhVat
    {
        Bau,
        Cua,
        Tom,
        Ca,
        Ga,
        Nai
    }

    class Program
    {
        static Random rand = new Random();
        static int soDu = 1000;
        static int tongSoVan = 0;
        static int soVanThang = 0;
        static int soVanThua = 0;
        static int tienThangLonNhat = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("===== GAME BẦU CUA =====");
            Console.WriteLine("Số dư ban đầu: 1000 điểm");

            bool tiepTuc = true;
            while (tiepTuc && soDu > 0)
            {
                Console.WriteLine($"\nSố dư hiện tại: {soDu}");
                Console.WriteLine("1. Chơi một ván (cược 1 linh vật)");
                Console.WriteLine("2. Chơi một ván (cược nhiều linh vật)");
                Console.WriteLine("3. Xem thống kê");
                Console.WriteLine("0. Thoát");
                int luaChon = NhapSoNguyen("Chọn: ");

                switch (luaChon)
                {
                    case 1: ChoiMotVanDonCuoc(); break;
                    case 2: ChoiMotVanNhieuCuoc(); break;
                    case 3: XemThongKe(); break;
                    case 0: tiepTuc = false; break;
                    default: Console.WriteLine("Lựa chọn không hợp lệ!"); break;
                }
            }

            Console.WriteLine("\n===== KẾT THÚC TRÒ CHƠI =====");
            XemThongKe();
            if (soDu <= 0) Console.WriteLine("Bạn đã hết tiền!");
        }

        static void HienThiLinhVat()
        {
            Console.WriteLine("Các linh vật: 1-Bầu 2-Cua 3-Tôm 4-Cá 5-Gà 6-Nai");
        }

        static LinhVat ChonLinhVat()
        {
            HienThiLinhVat();
            int chon;
            while (true)
            {
                chon = NhapSoNguyen("Chọn linh vật (1-6): ");
                if (chon >= 1 && chon <= 6) return (LinhVat)(chon - 1);
                Console.WriteLine("Lựa chọn không hợp lệ!");
            }
        }

        static int NhapTienCuoc()
        {
            int tien;
            while (true)
            {
                tien = NhapSoNguyen($"Nhập số tiền cược (số dư: {soDu}): ");
                if (tien <= 0) Console.WriteLine("Số tiền cược phải lớn hơn 0!");
                else if (tien > soDu) Console.WriteLine("Số tiền cược không được vượt quá số dư!");
                else return tien;
            }
        }

        static LinhVat[] TungXucXac()
        {
            LinhVat[] ketQua = new LinhVat[3];
            for (int i = 0; i < 3; i++)
                ketQua[i] = (LinhVat)rand.Next(6);
            return ketQua;
        }

        static void ChoiMotVanDonCuoc()
        {
            LinhVat linhVatChon = ChonLinhVat();
            int tienCuoc = NhapTienCuoc();

            LinhVat[] ketQua = TungXucXac();
            Console.WriteLine("Kết quả xúc xắc: " + string.Join(", ", ketQua));

            int soLanTrung = ketQua.Count(x => x == linhVatChon);
            tongSoVan++;

            if (soLanTrung > 0)
            {
                int tienThang = tienCuoc * soLanTrung;
                soDu += tienThang;
                soVanThang++;
                if (tienThang > tienThangLonNhat) tienThangLonNhat = tienThang;
                Console.WriteLine($"-> Chúc mừng! Bạn trúng {soLanTrung} lần, thắng {tienThang} điểm.");
            }
            else
            {
                soDu -= tienCuoc;
                soVanThua++;
                Console.WriteLine($"-> Rất tiếc, bạn thua {tienCuoc} điểm.");
            }

            Console.WriteLine($"Số dư hiện tại: {soDu}");
        }

        static void ChoiMotVanNhieuCuoc()
        {
            Dictionary<LinhVat, int> cuoc = new Dictionary<LinhVat, int>();
            Console.WriteLine("Đặt cược vào nhiều linh vật (nhập 0 để dừng đặt thêm).");

            while (true)
            {
                if (cuoc.Values.Sum() >= soDu)
                {
                    Console.WriteLine("Bạn đã cược hết số dư!");
                    break;
                }

                LinhVat lv = ChonLinhVat();
                int con = soDu - cuoc.Values.Sum();
                int tien = NhapSoNguyen($"Số tiền cược cho {lv} (còn lại {con}, nhập 0 để dừng): ");

                if (tien == 0) break;
                if (tien < 0 || tien > con)
                {
                    Console.WriteLine("Số tiền không hợp lệ!");
                    continue;
                }

                if (cuoc.ContainsKey(lv)) cuoc[lv] += tien;
                else cuoc[lv] = tien;

                Console.WriteLine("Nhập 1 để đặt thêm linh vật khác, 0 để tung xúc xắc.");
                int tiep = NhapSoNguyen("Chọn: ");
                if (tiep == 0) break;
            }

            if (cuoc.Count == 0) { Console.WriteLine("Bạn chưa đặt cược nào!"); return; }

            LinhVat[] ketQua = TungXucXac();
            Console.WriteLine("Kết quả xúc xắc: " + string.Join(", ", ketQua));

            int tongThang = 0;
            int tongCuoc = cuoc.Values.Sum();

            foreach (var kv in cuoc)
            {
                int soLanTrung = ketQua.Count(x => x == kv.Key);
                if (soLanTrung > 0)
                {
                    int thang = kv.Value * soLanTrung;
                    tongThang += thang;
                    Console.WriteLine($"- {kv.Key}: trúng {soLanTrung} lần, thắng {thang} điểm.");
                }
                else
                {
                    Console.WriteLine($"- {kv.Key}: không trúng, mất {kv.Value} điểm.");
                }
            }

            tongSoVan++;
            int laiRong = tongThang - tongCuoc;
            soDu += laiRong;

            if (laiRong > 0)
            {
                soVanThang++;
                if (tongThang > tienThangLonNhat) tienThangLonNhat = tongThang;
            }
            else if (laiRong < 0)
            {
                soVanThua++;
            }

            Console.WriteLine($"Tổng cược: {tongCuoc} | Tổng thắng: {tongThang} | Lãi/lỗ ròng: {laiRong}");
            Console.WriteLine($"Số dư hiện tại: {soDu}");
        }

        static void XemThongKe()
        {
            Console.WriteLine("\n--- THỐNG KÊ ---");
            Console.WriteLine($"Tổng số ván: {tongSoVan}");
            Console.WriteLine($"Số ván thắng: {soVanThang}");
            Console.WriteLine($"Số ván thua: {soVanThua}");
            Console.WriteLine($"Tiền thắng lớn nhất: {tienThangLonNhat}");
            Console.WriteLine($"Số dư cuối cùng: {soDu}");
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
