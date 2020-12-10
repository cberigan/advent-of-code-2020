using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    class Program
    {
        public class Node
        {
            public Node Next { get; set; }
            public int Value { get; set; }
        }

        static Dictionary<Node, long> memo = new Dictionary<Node,long>();

        static void Main(string[] args)
        {
            input.Add(0);
            input.Add(input.Max() + 3);
            input.Sort();
            var ones = 0;
            var threes = 0;
            for (int i = 1; i < input.Count; i++)
            {
                if (input[i] - input[i - 1] == 1) ones++;
                if (input[i] - input[i - 1] == 3) threes++;
            }

            Node start = new Node() { Value = input[0] };
            Node curr = start;
            for (int i = 1; i < input.Count; i++)
            {
                curr.Next = new Node() { Value = input[i] };
                curr = curr.Next;
            }

            Console.WriteLine(ones * threes);
            Console.WriteLine(FindRemovables2(start) + 1);
            Console.ReadLine();
        }

        static long FindRemovables2(Node start)
        {
            long count = 0;
            Node remove = null;
            Node curr = start;
            while (curr?.Next?.Next != null)
            {
                if(curr.Next.Next.Value - curr.Value <= 3)
                {
                    //remove start.Next
                    remove = curr;
                    count++;
                    break;
                }
                curr = curr.Next;
            }
            if (remove == null)
            {
                return 0;
            }
            Node removed = curr.Next;
            curr.Next = removed.Next;
            if(memo.ContainsKey(curr))
                count += memo[curr];
            else
            {
                var countFromNode = FindRemovables2(curr);
                memo[curr] = countFromNode;
                count += countFromNode;
            }
            //repair removal
            curr.Next = removed;
            if (memo.ContainsKey(removed))
                count += memo[removed];
            else
            {
                var countFromNode = FindRemovables2(removed);
                memo[removed] = countFromNode;
                count += countFromNode;
            }
            return count;
        }

        //static List<int> input = new List<int> { 16, 10, 15, 5, 1, 11, 7, 19, 6, 12, 4 };
        //static List<int> input = new List<int> { 28, 33, 18, 42, 31, 14, 46, 20, 48, 47, 24, 23, 49, 45, 19, 38, 39, 11, 1, 32, 25, 35, 8, 17, 7, 9, 4, 2, 34, 10, 3 };
        static List<int> input = new List<int> { 105, 78, 37, 153, 10, 175, 62, 163, 87, 22, 24, 92, 46, 5, 115, 61, 124, 128, 8, 60, 17, 93, 166, 29, 90, 148, 113, 55, 141, 134, 79, 101, 49, 133, 38, 53, 33, 30, 66, 159, 23, 132, 145, 147, 121, 94, 146, 21, 135, 56, 176, 118, 44, 138, 85, 169, 111, 9, 1, 83, 36, 59, 140, 149, 160, 43, 131, 69, 2, 25, 84, 39, 28, 171, 172, 100, 18, 15, 114, 70, 86, 97, 155, 152, 40, 122, 77, 16, 11, 170, 52, 45, 139, 76, 102, 63, 54, 142, 14, 158, 80, 154, 112, 91, 108, 73, 127, 123 };
    }
}
// 1,2,3,4,5,6