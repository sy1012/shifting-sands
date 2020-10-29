using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphGrammars
{
    [System.Serializable]
    public class Graph
    {
        public string name;
        [SerializeField]
        List<Node> nodes;
        // Adjecency list
        [SerializeField]
        List<Adjacencies> adjList;
        [SerializeField]
        List<Symbol> symbols;

        public Graph()
        {
            nodes = new List<Node>();
            adjList = new List<Adjacencies>();
            symbols = new List<Symbol>();
        }
        public Graph(string _name)
        {
            nodes = new List<Node>();
            adjList = new List<Adjacencies>();
            symbols = new List<Symbol>();
            name = _name;
        }

        /// <summary>
        /// Initializes a shallow copy to Node depth of a graph.
        /// </summary>
        /// <param name="original"></param>Graph instance
        public Graph(Graph original)
        {
            nodes = new List<Node>();
            adjList = new List<Adjacencies>();
            symbols = new List<Symbol>();
            AddNodes(original.GetNodes);
            var ogAdjList = original.GetAdjacencyList;
            for (int i = 0; i < nodes.Count; i++)
            {
                adjList[i].AddNodes(ogAdjList[i].GetAdj());
            }
        }

        public Node AddNode(Node node)
        {
            nodes.Add(node);
            adjList.Add(new Adjacencies());
            symbols.Add(node.GetSymbol);
            return node;
        }
        public void AddNodes(List<Node> nodes)
        {
            foreach (var n in nodes)
            {
                AddNode(n);
            }
        }
        public void AddConnection(Node from,Node to)
        {
            int i = Index(from);
            int j = Index(to);
            if (!adjList[i].Contains(to))
            {
                adjList[i].AddNode(to);
            }
            if (!adjList[j].Contains(from))
            {
                adjList[j].AddNode(from);
            }
        }
        public void AddConnections(Node from, List<Node> toNodes)
        {
            foreach (Node to in toNodes)
            {
                AddConnection(from, to);
            }
        }
        public void RemoveNode(Node node)
        {
            int i = Index(node);
            Adjacencies nodeAdjacencies = adjList[i];
            RemoveConnections(nodeAdjacencies.GetAdj(), node);
            adjList.RemoveAt(i);
            symbols.RemoveAt(i);
            nodes.Remove(node);
        }
        public void RemoveNodes(List<Node> nodes)
        {
            foreach (var node in nodes)
            {
                RemoveNode(node);
            }
        }
        public void RemoveConnection(Node from, Node to)
        {
            if (!nodes.Contains(to))
            {
                throw new Exception("To is not in graph: " + name +".");
            }
            adjList[Index(from)].RemoveNode(to);
        }
        public void RemoveConnections(Node from, List<Node> toNodes)
        {
            foreach (var to in toNodes)
            {
                RemoveConnection(from, to);
            }
        }
        public void RemoveConnections(List<Node> fromNodes, Node to)
        {
            foreach (var from in fromNodes)
            {
                RemoveConnection(from, to);
            }
        }
        public Node RandomNode()
        {
            return nodes[UnityEngine.Random.Range(0,nodes.Count)];
        }
        public bool                     IsConnected(Node a, Node b)
        {
            if (!(nodes.Contains(a) && nodes.Contains(b)))
            {
                throw new ArgumentException("One or both nodes are not in this graph");
            }
            var adjacencies = GetAdjByNode(a);
            if (adjacencies.Contains(b))
            {
                return true;
            }
            return false;
        }
        public Adjacencies              GetAdjByNode(Node node)
        {
            return adjList[Index(node)];
        }
        public Adjacencies GetAdjByIndex(int i)
        {
            return adjList[i];
        }
        public List<List<int>> GetAdjIndexList()
        {
            var result = new List<List<int>>();
            for (int i = 0; i < nodes.Count; i++)
            {
                var n = nodes[i];
                result.Add(new List<int>());
                var adj = GetAdjByNode(n).GetAdj();
                foreach (var a in adj)
                {
                    result[i].Add(Index(a));
                }
            }
            return result;
        }
        public List<Node>               GetNodes { get { return nodes; } }
        public List<Node>               GetNodesCopy() {
            var copy = new List<Node>();
            foreach (var n in nodes)
            {
                copy.Add(n.Copy());
            }
            return copy;
        }
        public List<Adjacencies>        GetAdjacencyList { get { return adjList; } }
        public List<Symbol>             GetSymbolsList { get { return symbols; } }
        public int                      GetDegreeOf(Node node)
        {
            return adjList[Index(node)].Count;
        }
        public int                      Index(Node node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("Node is null");
            }
            int i = nodes.IndexOf(node);
            if (i<0)
            {
                string hash = name + "";
                foreach (var n in nodes)
                {
                    hash += n.GetHashCode().ToString()+"\n";
                }
                throw new Exception("Expected on of:" + hash +"\n but got:" + node.GetHashCode().ToString() + " graph: " + name);
            }
            return i;
        }
        public override string          ToString()
        {
            string graph = name;
            foreach (var n in nodes)
            {
                graph += n.ToString() + "\n";
                graph += "\tAdjacent To:";
                foreach (var a in adjList[Index(n)].GetAdj())
                {
                    graph += "\t" + a.ToString();
                }
                graph += "\n";
            }
            
            return graph;
        }

    }
    [System.Serializable]
    public class Adjacencies
    {
        [SerializeField]
        public List<Node> adj;
        public Adjacencies()
        {
            adj = new List<Node>();
        }
        public void AddNode(Node node)
        {
            if (!adj.Contains(node))
            {
                adj.Add(node);
            }
        }
        public void AddNodes(List<Node> nodes)
        {
            foreach (var n in nodes)
            {
                AddNode(n);
            }
        }
        public void RemoveNode(Node node)
        {
            if (adj.Contains(node))
            {
                adj.Remove(node);
            }
        }
        public bool Contains(Node node)
        {
            if (adj.Contains(node))
            {
                return true;
            }
            return false;
        }
        public List<Node> GetAdj()
        {
            return adj;
        }
        public int Count { get { return adj.Count; } }
    } 

}