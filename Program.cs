using System;
using System.Collections.Generic;
using System.Linq;

namespace coupons
{
  class Program
  {
    class Node
    {
      public int Value;
      public Node Parent;
      public List<Node> Nodes = new List<Node>();

      public override string ToString()
      {
        return Value.ToString();
      }
    }

    static List<List<int>> combinations = 
      new List<List<int>>();

    static List<int> coupons;
    static int target;
    private static List<List<int>> Options(params int[] p)
    {
      var n = p[0];
      target = p[p.Length - 1];
      coupons = p.Skip(1).Take(p.Length - 2).ToList();

      combinations = new List<List<int>>();

      for (int i = 0; i < coupons.Count; i++)
      {
        var node = new Node() {Value = coupons[i]};
        if (node.Value == target)
        {
          combinations.Add(new List<int>() {node.Value});
        }
        else
        {
          AddNode(node, i, 0);
        }
      }

      return combinations;
    }

    private static void AddNode(Node node, int index, int reminder)
    {
      for (int i = index + 1; i < coupons.Count; i++)
      {
          if (node.Value + coupons[i] + reminder == target)
          {
            var comb = new List<int> { coupons[i], node.Value };
            var temp = node;
            while (node.Parent != null)
            {
              comb.Add(node.Parent.Value);
              node = node.Parent;
            }
            node = temp;
            combinations.Add(comb);
            continue;
          }
          var newNode = new Node {Value = coupons[i], Parent = node};
          AddNode(newNode, i, node.Value + reminder);
          node.Nodes.Add(newNode);
      }
    }

    static void Main(string[] args)
    {
      var options = Options(7, 1, 2, 3, 4, 5, 6, 7, 7);
      foreach (var o in options)
      {
        o.ForEach(i => Console.Write($"{i} "));
        
        Console.WriteLine();
      }
      Console.WriteLine();
    }
  }
}
