using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            var groups = input.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);
            var total = 0;
            foreach (var g in groups)
            {
                var chars = g.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                var sec = new HashSet<char>();
                sec = new HashSet<char>(chars[0]);
                for (int i = 1; i < chars.Length; i++)
                {
                    sec.IntersectWith(chars[i]);
                }
                total += sec.Count;

            }


            Console.WriteLine(total);
            Console.ReadLine();

        }
    }
}