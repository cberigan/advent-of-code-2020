using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
    class Program
    {
        class Cup
        {
            public Cup Next { get; set; }
            public int Value { get; set; }
        }
        static void Main(string[] args)
        {
            Part1();
            Part2();
        }

        private static void Part1()
        {
            var cups = BuildCups("459672813");
            var current = cups.Item1;
            var oneCup = cups.Item2;
            var map = cups.Item3;

            RunSimulation(current, 100, map, 9);
            Console.WriteLine("Part1:");
            PrintClockwiseOf(oneCup);
        }

        private static void Part2()
        {
            var input = "459672813";
            var cups = BuildCups(input, 1000000 - input.Length);
            var current = cups.Item1;
            var oneCup = cups.Item2;
            var map = cups.Item3;
            RunSimulation(current, 10000000, map, 1000000);
            Console.WriteLine("Part2:");
            Console.WriteLine(Convert.ToInt64(oneCup.Next.Value) * Convert.ToInt64(oneCup.Next.Next.Value));
            Console.ReadLine();
        }

        private static void RunSimulation(Cup current, int moves, Dictionary<int, Cup> map, int max)
        {
            for (int i = 0; i < moves; i++)
            {
                //pickup 3 
                var pickupStart = current.Next;
                var pickupEnd = pickupStart.Next.Next;
                var pickUpValues = new HashSet<int>
                {
                    pickupStart.Value,
                    pickupStart.Next.Value,
                    pickupStart.Next.Next.Value
                };
                current.Next = pickupEnd.Next;
                //find destination cup
                var cupNextValue = current.Value;
                do
                {
                    cupNextValue--;
                    cupNextValue = cupNextValue == 0 ? max : cupNextValue;
                }
                while (pickUpValues.Contains(cupNextValue));
                
                var destination = map[cupNextValue];
                //insert picked up
                var tempNext = destination.Next;
                destination.Next = pickupStart;
                pickupEnd.Next = tempNext;
                //select new current
                current = current.Next;
            }
        }

        private static (Cup, Cup, Dictionary<int, Cup>) BuildCups(string input, int extras = 0)
        {
            var map = new Dictionary<int, Cup>();
            var chars = input.ToCharArray();
            var start = new Cup { Value = int.Parse(chars[0].ToString()) };
            map.Add(start.Value, start);
            var current = start;
            Cup oneCup = null;
            for (int i = 1; i < chars.Length; i++)
            {
                var cup = new Cup { Value = int.Parse(chars[i].ToString()) };
                map.Add(cup.Value, cup);

                if (cup.Value == 1) oneCup = cup;
                current.Next = cup;
                current = cup;
            }

            var value = 10;
            for (int i = 0; i < extras; i++)
            {
                var cup = new Cup { Value = value };
                map.Add(cup.Value, cup);
                current.Next = cup;
                current = cup;
                value++;
            }
            //link end -> start
            current.Next = start;

            //reset current to start to begin
            current = start;
            return (current, oneCup, map);
        }

        static void PrintClockwiseOf(Cup start)
        {
            var current = start.Next;
            do
            {
                Console.Write(current.Value);
                current = current.Next;
            } while (current != start);
            Console.Write(Environment.NewLine);

        }
    }
}
