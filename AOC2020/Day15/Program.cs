using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var spoken = new Dictionary<int, int>();
            //int[] input = new int[] { 0, 3, 6 };
            int[] input = new int[] { 0, 20, 7, 16, 1, 18, 15 };
            var last = 0;
            var turn = input.Length;
            for (int i = 0; i < input.Length - 1; i++)
            {
                spoken.Add(input[i], i + 1);
            }
            last = input[input.Length - 1];


            while (turn < 30000000)
            {
                if(!spoken.ContainsKey(last))
                {
                    spoken.Add(last, turn++);
                    last = 0;
                }
                else
                {
                    var lastTurn = spoken[last];
                    spoken[last] = turn;
                    last = turn - lastTurn;
                    turn++;
                }
            }

            Console.WriteLine(last);
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            Console.ReadLine();


        }
    }
}
