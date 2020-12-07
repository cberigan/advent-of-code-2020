using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{

    public class Node
    {
        public string Type { get; set; }
        public List<Node> Parents { get; set; } = new List<Node>();
        public List<Node> Children { get; set; } = new List<Node>();
        public int Count { get; set; }
    }
    class Program
    {
        static string InputFile = "input.txt";
        static void Main(string[] args)
        {
            NAryTreeSolution();
            DictionarySolution();
            Console.ReadLine();
        }

        static void DictionarySolution()
        {
            var parents = new Dictionary<string, List<string>>();
            var children = new Dictionary<string, List<string>>();
            var lines = File.ReadAllLines(InputFile);

            foreach (var line in lines)
            {
                var elems = line.Split(new string[] { " contain " }, StringSplitOptions.None);
                var contained = elems[1].Split(',');

                var parentType = elems[0];
                if (!children.ContainsKey(parentType)) children.Add(parentType, new List<string>());

                foreach (var bag in contained)
                {
                    var bagTrim = bag.Trim();
                    if (bagTrim == "no other") continue;
                    var bagCount = int.Parse(bagTrim.Substring(0, 1));
                    var bagName = bagTrim.Substring(2);
                    if (!parents.ContainsKey(bagName)) parents.Add(bagName, new List<string>());

                    parents[bagName].Add(parentType);
                    for (int i = 0; i < bagCount; i++)
                    {
                        children[parentType].Add(bagName);
                    }
                }
            }

            //count parents from gold
            var bagsSeen = new HashSet<string>();
            FindParents2(parents, "shiny gold", bagsSeen);
            Console.WriteLine(bagsSeen.Count);
            Console.WriteLine(CountChildren2(children, "shiny gold"));
        }

        static void NAryTreeSolution()
        {
            var lookup = new Dictionary<string, Node>();
            var lines = File.ReadAllLines(InputFile);

            foreach (var line in lines)
            {
                var elems = line.Split(new string[] { " contain " }, StringSplitOptions.None);
                var contained = elems[1].Split(',');
                Node parent;
                if (lookup.ContainsKey(elems[0]))
                {
                    parent = lookup[elems[0]];
                }
                else
                {
                    parent = new Node { Type = elems[0] };
                    lookup.Add(parent.Type, parent);
                }
                foreach (var bag in contained)
                {
                    Node child;
                    var bagTrim = bag.Trim();
                    if (bagTrim == "no other") continue;
                    var bagCount = int.Parse(bagTrim.Substring(0, 1));
                    var bagName = bagTrim.Substring(2);
                    if (lookup.ContainsKey(bagName))
                    {
                        child = lookup[bagName];
                    }
                    else
                    {
                        child = new Node { Type = bagName };
                        lookup.Add(child.Type, child);
                    }
                    child.Parents.Add(parent);

                    for (int i = 0; i < bagCount; i++)
                    {
                        parent.Children.Add(new Node { Type = bagName });
                    }
                }
            }

            var gold = lookup["shiny gold"];
            var bagsSeen = new HashSet<string>();
            FindParents(gold, bagsSeen);

            Console.WriteLine(bagsSeen.Count);
            Console.WriteLine(CountChildren(lookup, gold));
        }
        static void FindParents2(Dictionary<string, List<string>> parents, string bag, HashSet<string> bags)
        {
            if (!parents.ContainsKey(bag)) return;
            foreach (var parent in parents[bag])
            {
                bags.Add(parent);
                FindParents2(parents, parent, bags);
            }
        }
        static void FindParents(Node root, HashSet<string> bags)
        {
            if (root == null) return;
            foreach (var parent in root.Parents)
            {
                bags.Add(parent.Type);
                FindParents(parent, bags);
            }
        }

        static int CountChildren2(Dictionary<string, List<string>> children, string bag)
        {
            if (!children.ContainsKey(bag)) return 0;
            var count = 0;
            foreach (var child in children[bag])
            {
                count++;
                count += CountChildren2(children, child);
            }
            return count;
        }
        static int CountChildren(Dictionary<string, Node> lookup, Node root)
        {
            if (root == null) return 0;
            var count = 0;
            foreach (var child in root.Children)
            {
                count++;
                count += CountChildren(lookup, lookup[child.Type]);
            }
            return count;
        }
    }
}
