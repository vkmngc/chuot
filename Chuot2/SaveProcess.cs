using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chuot2
{
    public class Score
    {
        public string UserName { get; set; }
        public int S { get; set; }
    }
    public class Scores
    {
        public List<Score> scores = new List<Score>();
        public void AddScore(Score score)
        {
            scores.Add(score);
        }
        public void Save(Score S, int NumOfLvl)
        {
            string filename;
            // Ghi danh sách điểm số vào file
            if (NumOfLvl==1)
            {
                filename = "lvl1.txt";
            }
            else if (NumOfLvl==2)
            {
                filename = "lvl2.txt";
            }
            else
            {
                filename = "lvl3.txt";
            }
            File.AppendAllText(filename, "\n" + S.UserName + "," + S.S);
        }
        public void Load(Score S, int NumOfLvl)
        {
            string filename;
            // Ghi danh sách điểm số vào file
            if (NumOfLvl == 1)
            {
                filename = "lvl1.txt";
            }
            else if (NumOfLvl == 2)
            {
                filename = "lvl2.txt";
            }
            else
            {
                filename = "lvl3.txt";
            }
            StreamReader reader = File.OpenText(filename);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();

                string[] tokens = line.Split(',');

                if (tokens.Length == 2)
                {
                    Score score = new Score();
                    score.UserName = tokens[0];
                    score.S = int.Parse(tokens[1]);
                    scores.Add(score);
                }
            }
            List<Score> SortedList = scores.OrderByDescending(o => o.S).ToList();
            int i = 0;
            foreach (Score item in SortedList)
            {
                i++;
                if (item == S)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.SetCursorPosition(0, i + 2);
                    Console.Write("{1}. {0}  ", item.UserName, i);
                    Console.SetCursorPosition(50, i + 2);
                    Console.WriteLine((string.Format("{0:00}", item.S)));
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.SetCursorPosition(0, i + 2);
                    Console.Write("{1}. {0}  ", item.UserName, i);
                    Console.SetCursorPosition(50, i + 2);
                    Console.WriteLine((string.Format("{0:00}", item.S)));
                }
            }
            i++;
            Console.SetCursorPosition(0, i + 2);
            reader.Close();
        }
    }
}
