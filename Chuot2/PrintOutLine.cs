using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Chuot2
{
    public class Print
    {
        public void MoveCursor(int row, int col)
        {
            Console.SetCursorPosition(col, row);
        }
        public void PrintOutline(int row, int col, int NOC, int L, string N, int T)
        {
            MoveCursor(0, 0);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (i == 0 || i == row - 1)
                    {
                        Console.Write("-");
                    }
                    else if (j == 0 || j == col - 1)
                    {
                        Console.Write("|");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.Write("|");

            Console.WriteLine("❤️: {3}/{3} 🧀: 0/{0} ⏰: {2} Tên: {1}", NOC, N, T / 1000, L);
            MoveCursor(row, col - 1);
            Console.WriteLine("|");

            for (int i = 0; i < col; i++)
            {
                Console.Write("-");
            }

        }

        public void ChuotNhapNhay (string Mouse, int r, int c)
        {
            MoveCursor(r, c);
            Console.Write(" ");
            Thread.Sleep(50);
            MoveCursor(r, c);
            Console.Write(Mouse);
            Thread.Sleep(50);
            MoveCursor(r, c);
            Console.Write(" ");
            Thread.Sleep(50);
            MoveCursor(r, c);
            Console.Write(Mouse);
            MoveCursor(r, c);
            Console.Write(" ");
            Thread.Sleep(50);
            MoveCursor(r, c);
            Console.Write(Mouse);
            Thread.Sleep(200);
        }

        public void inBXH (int res, string Name, int NumOfLvl)
        {
            //In ra bang xep hang
            Console.Clear();
            MoveCursor(1, 15);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("BẢNG XẾP HẠNG LVL {0}", NumOfLvl);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("----------------------------------------------------");
            var BangDiem = new Scores();
            Score Result = new Score();
            Result.UserName = Name;
            Result.S = res;
            BangDiem.AddScore(Result);
            BangDiem.Load(Result, NumOfLvl);
            BangDiem.Save(Result, NumOfLvl);
            Console.WriteLine("----------------------------------------------------");
            Thread.Sleep(1000);
        }

        public void HDSD(ref string N, ref int e)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("                        Chào ");
            N.Trim();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(N);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" nhaaaaa");
            Console.WriteLine("            Chào mừng bạn đến với game của tụi mìnhhhh\n                        Bạn đọc cách chơi nha!\n");
            Thread.Sleep(500);
            //Console.Clear();
            Console.WriteLine(@"                          HƯỚNG DẪN SỬ DỤNG:

       Điều khiển Chuột 🐭 bằng các phím mũi tên ⬆️⬇⬅️➡️ 

       Ăn hết số Phô mai 🧀 ở mỗi màn trong thời gian quy định sẽ qua màn
       
       Từ màn 2 trở đi sẽ xuất hiện Mèo Ma chạy lung tung trên màn hình đó!

       Tránh các Bẫy 🔴 và Mèo Ma 😾 nha! Mỗi lần chạm Bẫy 🔴 hoặc Mèo Ma 😾 
       bạn sẽ mất 1 mạng, hết số mạng bạn sẽ thua

       Thỉnh thoảng sẽ xuất hiện một Ngôi Sao * đó! Nếu Chuột ăn được 
       Ngôi Sao * thì sẽ được tăng tốc độ nha, lưu ý là Mèo Ma 😾 cũng sẽ tăng
       tốc theo bạn đó!

       Nhưng Ngôi Sao * chỉ xuất hiện trong 5s thôi, sau 5s Chuột hong ăn được 
       thì sẽ biến mất á!

       Nhiêu đó hoy ák, sẵn sàng chơi thì nhấn phím cách nha! Đổi ý thì nhấn Esc
       để thoát á!
                    ");

            ConsoleKeyInfo userKeyInput;
            do
            {
                userKeyInput = Console.ReadKey();
            }
            while (userKeyInput.Key != ConsoleKey.Spacebar && userKeyInput.Key != ConsoleKey.Escape);

            if (userKeyInput.Key == ConsoleKey.Spacebar)
            {
                Console.ResetColor();
                Console.Clear();
            }
            else
            {
                Console.ResetColor();
                Console.Clear();
                MoveCursor(0, 0);
                e = 1;
            }
        }
        
        public void PrintEachLv (int NumOfLvl, int NumOfYCheese, int NumOfLives, int NumOfTraps, int Time, string Name)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            MoveCursor(5, 30);
            Console.WriteLine("MÀN {0}\n", NumOfLvl);
            MoveCursor(6, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Để qua màn này, bạn cần ăn hết {1} cục phô mai vàng trong {3} giây nhaa\n        Lưu ý: bạn có {0} mạng và sẽ gặp {2} bẫy trên map đóo", NumOfLives, NumOfYCheese, NumOfTraps, Time/1000);
            Thread.Sleep(1000);
            MoveCursor(8, 10);
            Console.WriteLine("     Okeeeeee nhấn cách để chơi thôyy!");
            ConsoleKeyInfo userKeyInput;
            do
            {
                userKeyInput = Console.ReadKey();
            }
            while (userKeyInput.Key != ConsoleKey.Spacebar);
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine(@"
                     Chúc bạn chơi vui hehe
                        Game bắt đầu sau");
            Console.WriteLine("                               3");
            Thread.Sleep(1000);
            MoveCursor(3, 0);
            Console.WriteLine("                               2");
            Thread.Sleep(1000);
            MoveCursor(3, 0);
            Console.WriteLine("                               1");
            Thread.Sleep(1000);
            MoveCursor(3, 0);
            Console.WriteLine("                               0");
            Thread.Sleep(1000);
            MoveCursor(3, 0);
            Console.WriteLine("                             GO GO!");
            Thread.Sleep(1000);
            Console.Clear();
        }
    }
}
