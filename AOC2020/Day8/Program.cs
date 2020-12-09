using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            Part1(lines);
            Part2(lines);
            Console.ReadLine();

        }

        static void Part2(string[] lines)
        {
            var ops = new HashSet<int>();

            for (int j = 0; j < lines.Length; j++)
            {
                var op = lines[j].Split(' ')[0];
                if (op == "nop" || op == "jmp") ops.Add(j);
            }
            
            foreach (var opIndex in ops)
            {
                var set = new HashSet<int>();
                var acc = 0;
                var i = 0;
                var infLoopFound = false;
                while (i < lines.Length)
                {
                    var elem = lines[i].Split(' ');

                    string op = elem[0];
                    if(i == opIndex)
                    {
                        op = elem[0] == "nop" ? "jmp" : "nop";
                    }
                    var num = int.Parse(elem[1]);

                    if (set.Contains(i))
                    {
                        infLoopFound = true;
                        break;
                    }
                    set.Add(i);

                    if (op == "acc") acc += num;

                    var jump = op == "jmp" ? num : 1;
                    i += jump;

                }
                if (infLoopFound) continue;
                else
                {
                    Console.WriteLine(acc);
                    return;
                }


            }

            
        }


        static void Part1(string[] lines)
        {
            var set = new HashSet<int>();
            var acc = 0;
            var i = 0;
            while (true)
            {
                var elem = lines[i].Split(' ');
                var op = elem[0];
                var num = int.Parse(elem[1]);
                if (set.Contains(i)) break;
                set.Add(i);

                if (op == "acc") acc += num;

                var jump = op == "jmp" ? num : 1;
                i += jump;

            }

            Console.WriteLine(acc);
        }

        static string input = @"";
    }
}
