using Chuot2;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Chuot
    {
        static void Main(string[] args)
        {
        end:
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            //Cac bien can co
            string Name;
            int NumOfYCheese, NumOfLives, NumOfTraps, Time, row, col;
            int speed = 200;
            //Goi class PrintOutLine
            var P = new Print();

            //Nhap ten, huong dan su dung, chuc choi gem vui
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            MoveCursor(0, 10);
            Console.WriteLine("CHUỘT ĂN PHÔ MAI");
            Console.ResetColor();
            MoveCursor(1, 5);
            Console.Write("Nhập tên của bạn: ");
            Name = Console.ReadLine();
            Console.ResetColor();
            int e = 0;
            P.HDSD(ref Name, ref e);
            if (e == 1)
            {
                return;
            }

            //Bat dau 1 man
            //Set thong so man hinh va cac chi so khac trong lvl
            NumOfYCheese = 3;
            NumOfLives = 3;
            NumOfTraps = 4;
            Time = 30000;
            row = 15;
            col = 50;
            speed = 200;
            bool pass = false;
            var L = new lvl();
            L.lv(1, row, col, Time, speed, NumOfLives, NumOfYCheese, NumOfTraps, Name, ref pass);
            if (pass)
            {
                //Set thông số màn 2
                NumOfYCheese = 5;
                NumOfLives = 4;
                NumOfTraps = 6;
                Time = 45000;
                row = 20;
                col = 60;
                speed = 160;
                pass = false;
                L.lv(2, row, col, Time, speed, NumOfLives, NumOfYCheese, NumOfTraps, Name, ref pass);
                if (pass)
                {
                    //Set thông số màn 3
                    NumOfYCheese = 7;
                    NumOfLives = 5;
                    NumOfTraps = 8;
                    Time = 60000;
                    row = 25;
                    col = 70;
                    speed = 135;
                    pass = false;
                    L.lv(3, row, col, Time, speed, NumOfLives, NumOfYCheese, NumOfTraps, Name, ref pass);
                    if (pass)
                    {
                        Console.WriteLine();
                        Console.WriteLine("                      Chúc mừng bạn đã vượt qua 3 mànnnn");
                        Console.WriteLine("Chuột cảm ơn bạn rất nhiều vì đã giúp Chuột ăn hết Phô mai nha! 🐭 ( ∩´͈ ᐜ `͈∩)");
                        Console.WriteLine("Tặng bạn một cục kẹooooo 🍬 ৻(  •̀ ᗜ •́  ৻)");
                        Console.WriteLine("Nhấn cách để chơi lại từ đầu hoặc một phím bất kỳ để thoát gem nha");
                        ConsoleKeyInfo userKeyInput = Console.ReadKey();
                        if (userKeyInput.Key == ConsoleKey.Spacebar)
                        {
                            Console.ResetColor();
                            Console.Clear();
                            goto end;
                        }
                        else
                        {
                            Console.ResetColor();
                            Console.Clear();
                            MoveCursor(0, 0);
                            return;
                        }
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.Clear();
                        MoveCursor(0, 0);
                        Console.WriteLine("Byeeeee");
                        return;
                    }
                }
                else
                {
                    Console.ResetColor();
                    Console.Clear();
                    MoveCursor(0, 0);
                    Console.WriteLine("Byeeeee");
                    return;
                }
            }
            else
            {
                Console.ResetColor();
                Console.Clear();
                MoveCursor(0, 0);
                Console.WriteLine("Byeeeee");
                return;
            }
        }
        static void MoveCursor(int row, int col)
        {
            Console.SetCursorPosition(col, row);
        }
    }
}


