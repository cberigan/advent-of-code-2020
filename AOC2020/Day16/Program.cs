using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16
{
    class Program
    {
        static List<HashSet<int>> ruleSets = new List<HashSet<int>>();
        static List<List<int>> ruleCandidates = new List<List<int>>();
        static string ticket;
        static Dictionary<long, long> mem = new Dictionary<long, long>();
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            var elements = input.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);
            var rules = elements[0].Split(new string[] { "\r\n" }, StringSplitOptions.None);
            ticket = elements[1].Split(new string[] { "\r\n" }, StringSplitOptions.None)[0];
            var nearby = elements[2].Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
            var totalValidSet = new HashSet<int>();
            var err = 0;

            foreach (var rule in rules)
            {
                var elems = rule.Split(':');
                var ranges = elems[1].Split(new string[] { " or " }, StringSplitOptions.None);
                var set = new HashSet<int>();
                foreach (var range in ranges)
                {
                    var win = range.Split('-');
                    var min = int.Parse(win[0]);
                    var max = int.Parse(win[1]);

                    for (int i = min; i <= max; i++)
                    {
                        set.Add(i);
                        totalValidSet.Add(i);
                    }
                }
                ruleSets.Add(set);
            }
            var validNearby = new List<string>();
            foreach (var nb in nearby.ToList())
            {
                var isValid = true;
                foreach (var num in nb.Split(','))
                {
                    var test = int.Parse(num);
                    if (!totalValidSet.Contains(test))
                    {
                        nearby.Remove(nb);
                        err += test;
                        isValid = false;
                    }
                }
                if(isValid)
                {
                    validNearby.Add(nb);
                }
            }
            nearby = validNearby;
            //generate pos candidates for each rule
            ruleCandidates = GetFieldCandidates(nearby, ruleSets);
            
            var rulePositions = new int[ruleCandidates.Count];
            for (int i = 0; i < ruleCandidates.Count; i++)
            {
                rulePositions[i] = -1;
            }
            FindConfig(rulePositions, 0, new HashSet<int>());
            long result = 1;
            var ticketVals = ticket.Split(',');
            for (int i = 0; i < 6; i++)
            {
                var rulePosition = rulePositions[i];
                result *= long.Parse(ticketVals[rulePosition]);
            }
            Console.WriteLine(err);
            Console.WriteLine(result);
            Console.WriteLine(string.Join(",",rulePositions));
            Console.WriteLine(nearby.All(t => IsConfigValid(rulePositions,t)));
            Console.WriteLine(IsConfigValid(rulePositions, ticket));
            Console.ReadLine();
        }

        private static bool FindConfig(int[] rulePositions, int currRule, HashSet<int> pickedPositions)
        {
            if(currRule == ruleCandidates.Count)
                return IsConfigValid(rulePositions, ticket);
            var possible = ruleCandidates[currRule];
            for (int i = 0; i < possible.Count; i++)
            {
                if (pickedPositions.Contains(possible[i])) continue;
                rulePositions[currRule] = possible[i];
                pickedPositions.Add(possible[i]);
                if (FindConfig(rulePositions, currRule + 1, pickedPositions))
                    return true;
                rulePositions[currRule] = -1;
                pickedPositions.Remove(possible[i]);
            }
            return false;
        }

        private static bool IsConfigValid(int[] rulePositions, string testTicket)
        {
            if (rulePositions.Length != new HashSet<int>(rulePositions).Count) return false;
            var ticketValues = testTicket.Split(',');
            for (int i = 0; i < ruleSets.Count; i++)
            {
                var rulePosition = rulePositions[i];
                var ticketValue = int.Parse(ticketValues[rulePosition]);
                if (!ruleSets[i].Contains(ticketValue))
                    return false;
            }
            return true;
        }

        private static List<List<int>> GetFieldCandidates(List<string> nearby, List<HashSet<int>> ruleSets)
        {
            var ruleCandidates = new List<List<int>>();
            foreach (var rule in ruleSets)
            {
                var validPos = new List<int>();
                for (int i = 0; i < nearby[0].Split(',').Length; i++)
                {
                    var valid = true;
                    foreach (var nb in nearby)
                    {
                        var fields = nb.Split(',');
                        if (!rule.Contains(int.Parse(fields[i])))
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (valid)
                    {
                        validPos.Add(i);
                    }
                }

                ruleCandidates.Add(validPos);
            }

            return ruleCandidates;
        }
    } 
}