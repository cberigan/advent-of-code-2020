using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14
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
        static void Part2(string [] lines)
        {
            var mem = new Dictionary<long, long>();
            string mask = "";
            foreach (var line in lines)
            {
                var els = line.Split(',');
                var op = els[0];
                if (op == "mask")
                {
                    mask = els[1];
                }
                if (op == "mem")
                {
                    var address = Convert.ToString(long.Parse(els[1]), 2);
                    var value = long.Parse(els[2]);
                    address = address.PadLeft(36, '0');

                    var addresses = new List<long>();
                    GetAddresses(addresses, new StringBuilder(), address, mask, 0);

                    foreach (var variation in addresses)
                    {
                        if (mem.ContainsKey(variation))
                            mem[variation] = value;
                        else
                            mem.Add(variation, value);
                    }

                }
            }
            Console.WriteLine(mem.Values.Sum());

        }

        static void GetAddresses(List<long> addresses, StringBuilder sb, string address, string mask, int start)
        {
            for (int i = start; i < mask.Length; i++)
            {
                if (mask[i] == '0') sb.Append(address[i]);
                else if (mask[i] == '1') sb.Append("1");
                else
                {
                    var one = new StringBuilder(sb.ToString());
                    one.Append("1");
                    GetAddresses(addresses, one, address, mask, i + 1);
                    var zero = new StringBuilder(sb.ToString());
                    zero.Append("0");
                    GetAddresses(addresses, zero, address, mask, i + 1);
                    return;
                }
                   

            }
            addresses.Add(Convert.ToInt64(sb.ToString(), 2));
        }

        static void Part1(string[] lines)
        {
            var mem = new Dictionary<string, long>();
            string mask = "";
            foreach (var line in lines)
            {
                var els = line.Split(',');
                var op = els[0];
                if (op == "mask")
                {
                    mask = els[1];
                }
                if (op == "mem")
                {
                    var address = els[1];
                    var value = Convert.ToString(long.Parse(els[2]), 2);
                    value = value.PadLeft(36, '0');
                    var sb = new StringBuilder();
                    for (int i = 0; i < mask.Length; i++)
                    {
                        if (mask[i] == 'X') sb.Append(value[i]);
                        else sb.Append(mask[i]);

                    }
                    var converted = Convert.ToInt64(sb.ToString(), 2);
                    if (mem.ContainsKey(address))
                        mem[address] = converted;
                    else
                        mem.Add(address, converted);

                }
            }


            Console.WriteLine(mem.Values.Sum());
        }

    }
}
