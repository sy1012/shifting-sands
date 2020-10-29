using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GraphGrammars
{
    public static class Grammar
    {
        public static bool ApplyGrammarTo(Graph graph, Rule rule)
        {
            List<Node> isomorphAssignments = FindIsomorphism(graph, rule.GetLeftHandExpression().Graph);
            if (isomorphAssignments == null)
            {
                return false;
            }
            SinglePushOut(isomorphAssignments, graph, rule.GetLeftHandExpression().Graph, rule.GetRightHandExpression().Graph);
            Debug.Log(" Rule: " + rule.transform.name);
            //Debug.Log(graph.ToString());
            return true;
        }

        private static void UpdatePossibleAssignments(Graph graph, Graph subgraph, List<List<Node>> possibleAssignments)
        {
            bool anyChanges = true;
            while (anyChanges)
            {
                anyChanges = false;
                for (int i = 0; i < subgraph.GetNodes.Count; i++)
                {
                    List<Node> nonMatches = new List<Node>();
                    foreach (Node j in possibleAssignments[i]) // Possible assignments of j from super graph to i'th node of subgraph
                    {
                        foreach (Node x in subgraph.GetAdjByIndex(i).GetAdj()) //for each neighbour of i'th node in subgraph (node x)
                        {
                            // Make sure x has at least one candidate from the neighbours of j
                            bool match = false;
                            foreach (Node k in graph.GetAdjByNode(j).GetAdj())
                            {
                                if (possibleAssignments[subgraph.Index(x)].Contains(k))
                                {
                                    match = true;
                                }
                            }
                            if (!match)
                            {
                                anyChanges = true;
                                nonMatches.Add(j);
                            }
                        }
                    }
                    //Disqualify and canidate j of i, such that j did not have at least one neighbour which is a canidate of x (an i node neighbour)
                    foreach (var j in nonMatches)
                    {
                        possibleAssignments[i].Remove(j);
                    }

                }
            }
        }
        private static bool Search(Graph graph,Graph subgraph, List<Node> assignments, List<List<Node>> possibleAssignments, List<List<Node>> allIsos)
        {
            UpdatePossibleAssignments(graph, subgraph, possibleAssignments);
            // i maps to the index of the subgraph node we will try to assign this recursion iteration.
            int i = assignments.Count;

            //Label and degree condition
            foreach (var vg in subgraph.GetNodes)
            {
                int ind_vg = subgraph.Index(vg);
                // Only assigned subgraph nodes
                if (ind_vg<i)
                {
                    //Match symbols
                    if (vg.GetSymbol != assignments[ind_vg].GetSymbol)
                    {
                        return false;
                    }
                }
            }
            
            // Consider preservation of structural connectivity
            foreach (var vg in subgraph.GetNodes)
            {
                foreach(var neighbour in subgraph.GetAdjByNode(vg).GetAdj() )
                {
                    // Only nodes with index <i have been assigned. Check this set
                    if (subgraph.Index(vg) < i && subgraph.Index(neighbour)<i)
                    {
                        if (!(graph.IsConnected
                                (assignments[subgraph.Index(vg)],
                                assignments[subgraph.Index(neighbour)])))
                        {
                            return false;
                        }
                    }
                }
            }
            // If all the vertices in the subgraph are assigned, we are done
            if (i == subgraph.GetNodes.Count)
            {
                allIsos.Add(new List<Node>(assignments));
                return true;
            }
            bool foundMatch = false;
            for (int j_ind = possibleAssignments[i].Count-1; j_ind >=0 ; j_ind--)
            {
                // j has index n in possible assignments. Must iterate backwards so we can remove j if must.
                Node j = possibleAssignments[i][j_ind];
                if (!assignments.Contains(j))
                {
                    assignments.Add(j);

                    //Make a new set of possible assignments
                    var new_pos_a = new List<List<Node>>();
                    foreach (var I in possibleAssignments)
                    {
                        List<Node> J = new List<Node>();
                        foreach (var n in I)
                        {
                            J.Add(n);
                        }
                        new_pos_a.Add(J);
                    }
                    //endcopy
                    new_pos_a[i] = new List<Node>() { j };
                    if (Search(graph, subgraph, assignments, new_pos_a, allIsos))
                    {
                        foundMatch = true;
                    }
                    assignments.Remove(j);
                }
                possibleAssignments[i].Remove(j);
                UpdatePossibleAssignments(graph, subgraph, possibleAssignments);
            }
            return foundMatch;
        }
        /// <summary>
        /// An implementation of the Ullmann Algorithm for finding if a graph G has a subgraph isomorphic to argument subgraph
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="subgraph"></param>
        /// <returns></returns>
        public static List<Node> FindIsomorphism(Graph graph, Graph subgraph)
        {
            var assignments = new List<Node>();
            var possible_assignments = new List<List<Node>>();
            for (int i = 0; i < subgraph.GetNodes.Count; i++)
            {
                possible_assignments.Add(new List<Node>(graph.GetNodes));
                //One row for every subgraph node. Each row is the current possible assignments to supergraph
                /*        s u p e r G
                 *  s N1 [A,D,C,E,F]
                 *  u N2 [A,D,C,E,F]
                 *  b N3 [A,D,C,E,F]
                 *  G
                 *  This will be 'pruned' later so we are not checking nG!/nSG! amount of possible assignments
                 */
            }
            List<List<Node>> allIsos = new List<List<Node>>();
            if (Search(graph,subgraph,assignments,possible_assignments,allIsos))
            {
                Debug.Log(assignmentToString(allIsos));
                return allIsos[UnityEngine.Random.Range(0,allIsos.Count)];
            }
            return null;
        }

        /// <summary>
        /// Apply  graph transform to a graph containing a leftGraph, and replace with rightGraph. Preserve adjacencies
        /// </summary>
        /// <param name="assignments"></param> Assigned nodes from graph to leftgraph
        /// <param name="graph"></param> Graph to be transformed
        /// <param name="leftGraph"></param> Left hand side of graph grammar.Subgraph of graph
        /// <param name="rightGraph"></param> Right hand side of graph grammar.
        /// <returns></returns>
        public static void SinglePushOut(List<Node> assignments, Graph graph, Graph leftGraph, Graph rightGraph)
        {
            //Preserve adjacencies of assigned nodes. 
            List<Adjacencies> edgePreserves = new List<Adjacencies>();
            foreach (Node assigned in assignments)
            {
                graph.RemoveConnections(assigned,assignments);
            }
            for (int i = 0; i < assignments.Count; i++)
            {
                edgePreserves.Add(graph.GetAdjByNode(assignments[i]));
            }
            graph.RemoveNodes(assignments);
            List<Node> rightNodes = rightGraph.GetNodesCopy();
            List<List<int>> rightNodesAdjIndexs = rightGraph.GetAdjIndexList();
           
            graph.AddNodes(rightNodes);

            //Restore preserved graph to leftside edges to the newly added righthand side nodes
            for (int i = 0; i < assignments.Count; i++)
            {
                foreach (var adj in edgePreserves[i].GetAdj())
                {
                    graph.AddConnection(rightNodes[i], adj);
                }
            }

            //Add edges between newly added righthand nodes
            //TODO Find out why this is so buggy here
            for (int i = 0; i < rightNodes.Count; i++)
            {
                foreach (int j in rightNodesAdjIndexs[i])
                {
                    graph.AddConnection(rightNodes[i], rightNodes[j]);
                }
            }
        }
        public static string assignmentToString(List<List<Node>> assignmentsList)
        {
            string result = "Assignments:";
            foreach (var assignment in assignmentsList)
            {
                result += "\n\tAssignment: ";
                foreach (var node in assignment)
                {
                    result += node.ToString() + ", ";
                }
            }
            return result;
        }
    }

    public static class Combination
    {
        public static void combinations2(Node[] arr, int len, int startPosition, Node[] result, List<Node[]> results)
        {
            if (len == 0)
            {
                Node[] copy = (Node[])result.Clone();
                results.Add(copy);
                return;
            }
            for (int i = startPosition; i <= arr.Length- len; i++)
            {
                result[result.Length - len] = arr[i];
                combinations2(arr, len - 1, i + 1, result,results);
            }
        }
    }
}