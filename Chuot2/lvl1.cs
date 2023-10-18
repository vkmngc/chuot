using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Chuot2
{
    internal class lvl
    {
        public void lv (int NumOfLvl, int row, int col, int Time, int speed, int NumOfLives, int NumOfYCheese, int NumOfTraps, string Name, ref bool pass)
        {

            end:
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            int CheeseEaten = 0;
            string Mouse = "🐭", Cat = "😾", Trap = "🔴", Cheese = "🧀", Star = "*";
            var P = new Print();
            P.PrintEachLv(NumOfLvl, NumOfYCheese, NumOfLives, NumOfTraps, Time, Name);
            P.PrintOutline(row, col, NumOfYCheese, Name, Time);
            //Xac dinh vi tri con chuot o vi tri xuat phat la goc trai tren cung va in no ra
            Position PosOfMouse = new Position(1, 1);
            //Random Generator
            Random randomNum = new Random();
            //Tao mieng do an dau tien roi in ra
            Position food;
            food = new Position(
                randomNum.Next(2, row - 2),
                randomNum.Next(2, col - 2));
            MoveCursor(food.row, food.col);
            Console.Write(Cheese);


            //Tao list cac bay va in tui no ra
            List<Position> PosOfTraps = new List<Position>();
            for (int i = 0; i < NumOfTraps; i++)
            {
                do
                {
                    PosOfTraps.Add(new Position(
                        randomNum.Next(2, row - 2),
                        randomNum.Next(2, col - 2)));
                }
                while (food.row == PosOfTraps[i].row && food.col == PosOfTraps[i].col);
            }
            foreach (Position trap in PosOfTraps)
            {
                MoveCursor(trap.row, trap.col);
                Console.Write(Trap);
            }

            //Vi kich thuoc cua chuot va bay hoi chenh nhau, co truong hop nhin thay chuot dung bay
            //nhung theo toa do thi khong co, dieu chinh nhu nay de khong bo sot truong hop chuot dung bay visually not technically

            List<Position> IlluPosOfTraps = new List<Position>();

            for (int i = 0; i < NumOfTraps; i++)
            {
                IlluPosOfTraps.Add(new Position(
                        PosOfTraps[i].row,
                        PosOfTraps[i].col - 1));
                IlluPosOfTraps.Add(new Position(
                        PosOfTraps[i].row,
                        PosOfTraps[i].col + 1));
            }

            //Khoi tao con meo
            Position PosOfCat = new Position();
            
                do
                {
                    PosOfCat.row = randomNum.Next(2, row - 2);
                    PosOfCat.col = randomNum.Next(2, col - 2);
                }
                while (PosOfTraps.Contains(PosOfCat) || IlluPosOfTraps.Contains(PosOfCat) || (food.row == PosOfCat.row && food.col == PosOfCat.col));
            
            MoveCursor(PosOfCat.row, PosOfCat.col);
            Console.Write(Cat);

            //Khoi tao ngoi sao o ngoai duong bien man hinh
            Position PosOfStar = new Position(row + 2, col + 2);
            bool StarAppeared = false;


            //Set cac so tuong trung cho 4 phuong huong
            byte right = 0;
            byte left = 1;
            byte down = 2;
            byte up = 3;

            //4 huong chuot co the di chuyen
            Position[] directions =
            {
                new Position(0,1), //Right
                new Position(0,-1), //Left
                new Position(1,0), //Down
                new Position(-1,0)//Up
            };

            //Set huong di ban dau cua chuot la di sang ben phai
            byte currentDirection = right;

            //In chuot dau tien
            MoveCursor(PosOfMouse.row, PosOfMouse.col);
            Console.WriteLine(Mouse);

            //Luu thoi gian bat dau
            int startTime = Environment.TickCount;
            
            //Bat dau tro choi
            while (Environment.TickCount - startTime <= Time)
            {
                if (Console.KeyAvailable)
                {
                    //Doc phim duoc nhan vao
                    ConsoleKeyInfo userKeyInput = Console.ReadKey();

                    //Kiem tra phim nhap vao la phim nao, neu hop le thi thay doi huong di cua chuot
                    if (userKeyInput.Key == ConsoleKey.RightArrow)
                    {
                        if (currentDirection != left)
                            currentDirection = right;
                    }
                    else if (userKeyInput.Key == ConsoleKey.LeftArrow)
                    {
                        if (currentDirection != right)
                            currentDirection = left;
                    }
                    else if (userKeyInput.Key == ConsoleKey.DownArrow)
                    {
                        if (currentDirection != up)
                            currentDirection = down;
                    }
                    else if (userKeyInput.Key == ConsoleKey.UpArrow)
                    {
                        if (currentDirection != down)
                            currentDirection = up;
                    }
                }
                //Cap nhat huong di moi
                Position nextDirection = directions[currentDirection];

                //Cap nhat vi tri moi cua chuot
                Position NewPosOfMouse = new Position(
                    PosOfMouse.row + nextDirection.row,
                    PosOfMouse.col + nextDirection.col);

                //Kiem tra neu chuot di ra ngoai map thi cho no quay lai o huong doi dien
                if (NewPosOfMouse.row < 1)
                    NewPosOfMouse.row = row - 2;
                else if (NewPosOfMouse.row >= row - 1)
                    NewPosOfMouse.row = 1;
                else if (NewPosOfMouse.col < 1)
                    NewPosOfMouse.col = col - 3;
                else if (NewPosOfMouse.col >= col - 2)
                    NewPosOfMouse.col = 1;

                int check = 0;
                //Neu chuot dung bay/meo thi sao?
                //Check xem chuot dung bay theo truong hop nao
                //Dung bay tai dung toa do
                if (PosOfTraps.Contains(NewPosOfMouse))
                {
                    check = 1;
                }
                //Dung bay tai ao giac cua vat
                else if (IlluPosOfTraps.Contains(NewPosOfMouse))
                {
                    check = 2;
                }
                //Dung meo
                else if (NewPosOfMouse.row == PosOfCat.row && (NewPosOfMouse.col == PosOfCat.col || NewPosOfMouse.col == PosOfCat.col - 1 || NewPosOfMouse.col == PosOfCat.col + 1))
                {
                    check = 3;
                }
                //Neu chuot dung bay/dung meo
                if (check != 0)
                {
                    if (check == 1)
                    {
                        //xoa bay
                        PosOfTraps.Remove(NewPosOfMouse);
                        Position Tem = new Position(
                            NewPosOfMouse.row,
                            NewPosOfMouse.col + 1);
                        IlluPosOfTraps.Remove(Tem);

                        Tem.col = NewPosOfMouse.col - 1;
                        IlluPosOfTraps.Remove(Tem);
                    }
                    else if (check == 2)
                    {
                        int eureka = IlluPosOfTraps.LastIndexOf(NewPosOfMouse);
                        Position FI = NewPosOfMouse;
                        if (eureka % 2 == 1)
                        {
                            IlluPosOfTraps.Remove(FI);
                            FI.col -= 2;
                            IlluPosOfTraps.Remove(FI);
                            FI.col++;
                            PosOfTraps.Remove(FI);
                        }
                        else
                        {
                            IlluPosOfTraps.Remove(FI);
                            FI.col += 2;
                            IlluPosOfTraps.Remove(FI);
                            FI.col--;
                            PosOfTraps.Remove(FI);
                        }
                    }

                    //Cap nhat so mang, in len man hinh
                    NumOfLives--;
                    MoveCursor(row, 5);
                    Console.WriteLine(NumOfLives);

                    //Neu het mang
                    if (NumOfLives == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        MoveCursor(row / 2, 0);
                        Console.WriteLine("Thua mất òi, muốn chơi lại hăm? \n (Có thì nhấn phím cách, hong thì nhấn Esc nha)");
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
                            goto end;
                        }
                        else
                        {
                            Console.ResetColor();
                            Console.Clear();
                            MoveCursor(0, 0);
                            pass = false;
                            return;
                        }
                    }
                    //Neu con mang
                    else
                    {
                        P.ChuotNhapNhay(Mouse, PosOfMouse.row, PosOfMouse.col);
                    }
                }

                //Cac bien de luu su di chuyen cua meo
                Position nextDirecOfCat = new Position();
                Position NewPosOfCat = new Position();
                //Meo di chuyen
                int RandDirecOfCat;
                //Kiem tra meo va huong di cua no
                
                    do
                    {
                        RandDirecOfCat = randomNum.Next(0, 4);
                        nextDirecOfCat = directions[RandDirecOfCat];
                        NewPosOfCat = new Position(
                            PosOfCat.row + nextDirecOfCat.row + 1,
                            PosOfCat.col + nextDirecOfCat.col + 1);
                    }
                    while (PosOfTraps.Contains(NewPosOfCat) || IlluPosOfTraps.Contains(NewPosOfCat)
                                        || (food.row == NewPosOfCat.row && (food.col == NewPosOfCat.col || food.col - 1 == NewPosOfCat.col || food.col + 1 == NewPosOfCat.col))
                                        || NewPosOfMouse.row == NewPosOfCat.row && (NewPosOfMouse.col == NewPosOfCat.col || NewPosOfMouse.col - 1 == NewPosOfCat.col || NewPosOfMouse.col - 1 == NewPosOfCat.col)
                                        || PosOfStar.row == NewPosOfCat.row && (PosOfStar.col == NewPosOfCat.col || PosOfStar.col - 1 == NewPosOfCat.col || PosOfStar.col - 1 == NewPosOfCat.col));

                    //Kiem tra meo co di ra ngoai map khong
                    if (NewPosOfCat.row < 1)
                        NewPosOfCat.row = row - 2;
                    else if (NewPosOfCat.row >= row - 1)
                        NewPosOfCat.row = 1;
                    else if (NewPosOfCat.col < 1)
                        NewPosOfCat.col = col - 3;
                    else if (NewPosOfCat.col >= col - 2)
                        NewPosOfCat.col = 2;
                
                //Neu chuot an duoc pho mai vang
                if (food.row == NewPosOfMouse.row && ((food.col == NewPosOfMouse.col) || (food.col == NewPosOfMouse.col - 1) || (food.col == NewPosOfMouse.col + 1)))
                {
                    Console.Beep(900, 50);
                    CheeseEaten++;
                    MoveCursor(row, 13);
                    Console.Write(CheeseEaten);
                    MoveCursor(food.row, food.col);
                    Console.Write(" ");
                    if (CheeseEaten == NumOfYCheese)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        MoveCursor(row / 2, col / 3);
                        Console.WriteLine("Thắng ròi giỏi wa!!!");
                        Console.ResetColor();
                        Thread.Sleep(3000);
                        //Tinh diem
                        P.inBXH(Time, startTime, Name, NumOfLvl);
                        if (NumOfLvl != 3)
                        {
                            Console.WriteLine("Nhấn cách để qua màn tiếp theo, nhấn Esc để thoát nha");
                        }
                        else
                        {
                            Console.WriteLine("Nhấn cách để tới màn hình nhận thưởng hoặc Esc để thoát nha");
                        }
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
                            pass = true;
                            return;
                        }
                        else
                        {
                            Console.ResetColor();
                            Console.Clear();
                            MoveCursor(0, 0);
                            pass = false;
                            return;
                        }
                    }
                    else
                    {
                        do
                        {
                            food = new Position(
                            randomNum.Next(2, row - 2),
                            randomNum.Next(2, col - 2));
                        }
                        while (PosOfTraps.Contains(food) || IlluPosOfTraps.Contains(food)
                        || (food.row == NewPosOfMouse.row && ((food.col == NewPosOfMouse.col) || (food.col == NewPosOfMouse.col - 1) || (food.col == NewPosOfMouse.col + 1)))
                        || (food.row == NewPosOfCat.row) && ((food.col == NewPosOfCat.col) || (food.col == NewPosOfCat.col - 1) || (food.col == NewPosOfCat.col + 1))
                        || (food.row == PosOfStar.row) && ((food.col == PosOfStar.col) || (food.col == PosOfStar.col - 1) || (food.col == PosOfStar.col + 1)));
                        MoveCursor(food.row, food.col);
                        Console.Write(Cheese);
                    }
                }
                //Cap nhat thoi gian
                MoveCursor(row, 21);
                Console.Write("  ");
                MoveCursor(row, 21);
                int tem = ((Environment.TickCount - startTime) / 1000);
                tem = Time / 1000 - tem;
                Console.Write("{0}", tem);

                //Neu ngoi sao xuat hien
                if (StarAppeared)
                {
                    //Ngoi sao bi chuot an
                    if (NewPosOfMouse.row == PosOfStar.row && ((NewPosOfMouse.col == PosOfStar.col) || (NewPosOfMouse.col == PosOfStar.col - 1) || (NewPosOfMouse.col == PosOfStar.col + 1)))
                    {
                        //MoveCursor(PosOfStar.row, PosOfStar.col);
                        //Console.Write(" ");
                        speed = speed - 75;
                        StarAppeared = false;
                    }
                    //Het 5s roi ma chuot chua an ngoi sao thi ngoi sao bien mat
                    else if (tem % 10 == 5)
                    {
                        MoveCursor(PosOfStar.row, PosOfStar.col);
                        Console.Write(" ");
                        StarAppeared = false;
                    }
                }

                //Sau moi 10s se co 20% co hoi xuat hien ngoi sao, an ngoi sao thi tang toc
                if (tem % 10 == 0 && StarAppeared == false)
                {
                    int rnd = randomNum.Next(0, 5);
                    if (rnd == 4)
                    {
                        do
                        {
                            PosOfStar = new Position(
                            randomNum.Next(2, row - 2),
                            randomNum.Next(2, col - 2));
                        }
                        while (PosOfTraps.Contains(PosOfStar) || IlluPosOfTraps.Contains(PosOfStar)
                        || (PosOfStar.row == NewPosOfMouse.row && ((PosOfStar.col == NewPosOfMouse.col) || (PosOfStar.col == NewPosOfMouse.col - 1) || (PosOfStar.col == NewPosOfMouse.col + 1)))
                        || (PosOfStar.row == food.row && ((PosOfStar.col == food.col) || (PosOfStar.col == food.col - 1) || (PosOfStar.col == food.col + 1)))
                        || (PosOfStar.row == NewPosOfCat.row) && ((PosOfStar.col == NewPosOfCat.col) || (PosOfStar.col == NewPosOfCat.col - 1) || (PosOfStar.col == NewPosOfCat.col + 1)));
                        //Di chuyen con chuot, in ngoi sao
                        MoveCursor(PosOfStar.row, PosOfStar.col);
                        Console.Write(Star);
                        StarAppeared = true;
                    }
                }
                //Xoa chuot cu
                Console.SetCursorPosition(PosOfMouse.col, PosOfMouse.row);
                Console.Write(" ");
                //In chuot moi
                Console.SetCursorPosition(NewPosOfMouse.col, NewPosOfMouse.row);
                Console.Write(Mouse);
                //Cap nhat vi tri chuot cho buoc di tiep theo
                PosOfMouse.row = NewPosOfMouse.row;
                PosOfMouse.col = NewPosOfMouse.col;
                
                //Xoa meo cu
                MoveCursor(PosOfCat.row, PosOfCat.col);
                Console.Write(" ");
                //In meo moi
                MoveCursor(NewPosOfCat.row, NewPosOfCat.col);
                Console.Write(Cat);
                //Cap nhat vi tri meo
                PosOfCat.row = NewPosOfCat.row;
                PosOfCat.col = NewPosOfCat.col;

                Thread.Sleep(speed);
            }
            //Het gio
            MoveCursor(row, 21);
            Console.Write("  ");
            MoveCursor(row, 21);
            Console.Write("0");
            MoveCursor(row / 2, 4);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("Hết giờ ròi thua nha, bạn muốn chơi lại hong?\n Có thì nhấn cách, hong thì nhấn Esc để thoát nha");
            ConsoleKeyInfo u;
            do
            {
                u = Console.ReadKey();
            }
            while (u.Key != ConsoleKey.Spacebar && u.Key != ConsoleKey.Escape);

            if (u.Key == ConsoleKey.Spacebar)
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
                pass = false;
                return;
            }
        }
        struct Position
        {
            public int row;
            public int col;

            public Position(int row, int col)
            {
                this.row = row;
                this.col = col;
            }
        }
        static void MoveCursor(int row, int col)
        {
            Console.SetCursorPosition(col, row);
        }
    }
}
