using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GraphGrammars
{
    public class TestsUtils : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

            if (!TestFindIsomorphism1())
            {
                Debug.Log("Find isomorphism 1 has failed");
            }
            if (TestFindIsomorphism2())
            {
                Debug.Log("Find isomorphism 2 has failed");
            }
            for (int i = 0; i < 200; i++)
            {
                if (!TestFindIsomorphism3())
                {
                    Debug.Log("Find isomorphism 3 has failed");
                }
            }
            if (!TestFindIsomorphism4())
            {
                Debug.Log("Find isomorphism 4 has failed");
            }
            TestSinglePushout();
            TestCombine();
        }

        // Update is called once per frame
        void Update()
        {

        }
        bool TestFindIsomorphism1()
        {
            //No cycles in input
            var subgraph = new Graph();
            //super graph
            var graph = new Graph();
            var A = new Node("A");
            var B = new Node("B");
            var C = new Node("C");
            var D = new Node("D");
            var E = new Node("E");
            var F = new Node("F");
            var _1 = new Node("_1");
            var _2 = new Node("_2");
            var _3 = new Node("_3");
            graph.AddNodes(new List<Node>() { A,B,C,D,E,F});
            subgraph.AddNodes(new List<Node>() { _1, _2, _3 });
            graph.AddConnection(A, E);
            graph.AddConnection(C, D);
            graph.AddConnection(B, D);
            graph.AddConnection(C, A);
            graph.AddConnection(C, F);
            subgraph.AddConnection(_1, _2);
            subgraph.AddConnection(_3, _2);
            List<Node> mappingAssignments = Grammar.FindIsomorphism(graph, subgraph);
            if (mappingAssignments == null)
            {
                return false;
            }
            if (mappingAssignments.Count == subgraph.GetNodes.Count)
            {
                return true;
            }
            throw new System.Exception("Should not get here. Mapping should b null if no isoM found. IsoM is not null " +
                "but not equal in count to subgraph.");
        }
        bool TestFindIsomorphism2()
        {
            //No cycles in input
            var subgraph = new Graph();
            //super graph
            var graph = new Graph();
            var A = new Node("A");
            var B = new Node("B");
            var C = new Node("C");
            var D = new Node("D");
            var E = new Node("E");
            var _1 = new Node("_1");
            var _2 = new Node("_2");
            var _3 = new Node("_3");
            graph.AddNodes(new List<Node>() { A, B, C, D, E });
            subgraph.AddNodes(new List<Node>() { _1, _2, _3 });
            graph.AddConnection(A, E);
            graph.AddConnection(C, D);
            subgraph.AddConnection(_1, _2);
            subgraph.AddConnection(_3, _2);
            List<Node> mappingAssignments = Grammar.FindIsomorphism(graph, subgraph);
            if (mappingAssignments == null)
            {
                return false;
            }
            if (mappingAssignments.Count == subgraph.GetNodes.Count)
            {
                return true;
            }
            throw new System.Exception("Should not get here. Mapping should b null if no isoM found. IsoM is not null " +
                "but not equal in count to subgraph.");
        }
        bool TestFindIsomorphism3()
        {
            //No cycles in input
            var subgraph = new Graph();
            //super graph
            var graph = new Graph();
            var A = new NTNode("A");
            var B = new NTNode("B");
            var C = new Node("C");
            var D = new Node("D");
            var E = new GoalNode();
            var F = new Node("F");
            var G = new Node("G");
            var H = new Node("H");
            var I = new NTNode("I");
            var J = new Node("J");
            var K = new Node("K");
            var _1 = new Node("_1");
            var _2 = new NTNode("_2");
            var _3 = new Node("_3");
            graph.AddNodes(new List<Node>() { A, B, C, D, E, F, G, H, I, J, K});
            subgraph.AddNodes(new List<Node>() { _1, _2, _3 });
            graph.AddConnection(A, E);
            graph.AddConnection(C, D);
            graph.AddConnection(B, D);
            graph.AddConnection(C, A);
            graph.AddConnection(C, F);
            graph.AddConnection(F, G);
            graph.AddConnection(F, H);
            graph.AddConnection(D, I);
            graph.AddConnection(I, J);
            graph.AddConnection(I, K);
            subgraph.AddConnection(_1, _2);
            subgraph.AddConnection(_3, _2);
            List<Node> mappingAssignments = Grammar.FindIsomorphism(graph, subgraph);
            if (mappingAssignments == null)
            {
                return false;
            }
            if (mappingAssignments.Count == subgraph.GetNodes.Count)
            {
                return true;
            }
            throw new System.Exception("Should not get here. Mapping should b null if no isoM found. IsoM is not null " +
                "but not equal in count to subgraph.");
        }
        /// <summary>
        /// Tests symbol matching of isoM finder
        /// </summary>
        /// <returns></returns>
        bool TestFindIsomorphism4()
        {
            //No cycles in input
            var subgraph = new Graph();
            //super graph
            var graph = new Graph();
            var A = new Node("A");
            var B = new Node("B");
            var D = new GoalNode();
            var E = new NTNode("E");
            var F = new EntranceNode();
            var _1 = new Node("_1");
            var _2 = new GoalNode();
            var _3 = new Node("_3");
            graph.AddNodes(new List<Node>() { A, B, D, E, F });
            subgraph.AddNodes(new List<Node>() { _1, _2, _3 });
            graph.AddConnection(A, D);
            graph.AddConnection(E, D);
            graph.AddConnection(B, D);
            graph.AddConnection(B, F);
            subgraph.AddConnection(_1, _2);
            subgraph.AddConnection(_3, _2);
            List<Node> mappingAssignments = Grammar.FindIsomorphism(graph, subgraph);
            if (mappingAssignments == null)
            {
                return false;
            }
            if (mappingAssignments[0] != B)
            {
                Debug.Log(mappingAssignments[0].ToString() + mappingAssignments[1].ToString() + mappingAssignments[2].ToString());
                return false;
            }
            if (mappingAssignments[1] != D)
            {
                Debug.Log(mappingAssignments[0].ToString() + mappingAssignments[1].ToString() + mappingAssignments[2].ToString());
                return false;
            }
            if (mappingAssignments[2] != A)
            {
                Debug.Log(mappingAssignments[0].ToString() + mappingAssignments[1].ToString() + mappingAssignments[2].ToString());
                return false;
            }
            return true;
        }
        bool TestSinglePushout()
        {
            //No cycles in input
            var left = new Graph();
            var right = new Graph();
            //super graph
            var graph = new Graph();
            var A = new Node("A");
            var B = new Node("B");
            var D = new GoalNode();
            var E = new NTNode("E");
            var F = new EntranceNode();
            var _1 = new Node("_1");
            var _2 = new GoalNode();
            var _3 = new Node("_3");
            var R1 = new Node("R1");
            var R2 = new Node("R2");
            var R3 = new Node("R3");
            graph.AddNodes(new List<Node> { A, B, D, E, F });
            left.AddNodes(new List<Node> { _1, _2 });
            right.AddNodes(new List<Node> { R1, R3, R2});
            List<Node> assignments = new List<Node>{ D, B};
            graph.AddConnection(A, D);
            graph.AddConnection(E, D);
            graph.AddConnection(B, D);
            graph.AddConnection(B, F);
            left.AddConnection(_1, _2);
            right.AddConnection(R1, R2);
            right.AddConnection(R3, R2);
            Grammar.SinglePushOut(assignments, graph, left, right);
            string expected = "A\n\tAdjacent To:\tR1\n" +
                "E\n\tAdjacent To:\tR1\n" +
                "Ent\n\tAdjacent To:\tR3\n" +
                "R1\n\tAdjacent To:\tA\tE\tR2\n" +
                "R3\n\tAdjacent To:\tEnt\tR2\n" +
                "R2\n\tAdjacent To:\tR1\tR3\n";

            if (graph.ToString().CompareTo(expected)!=0)
            {
                Debug.Log(graph.ToString());
                return false;
            }
            return true;
        }
        void TestCombine()
        {
            Node A;
            Node B;
            Node C;
            Node D;
            Node E;
            Node F;
            Graph graph = new Graph("Main");
            graph.AddNode(new StartNode());
            //Rule 1
            Graph left = new Graph("LeftStart");
            left.AddNode(new StartNode());
            Graph right = new Graph("RightStart");
            A = new EntranceNode();
            B = new NTNode("B");
            C = new NTNode("C");
            D = new NTNode("D");
            E = new NTNode("E");
            F = new GoalNode();
            right.AddNodes(new List<Node> { A, B, C, D, E, F });
            right.AddConnection(A, B);
            right.AddConnection(A, D);
            right.AddConnection(B, C);
            right.AddConnection(E, C);
            right.AddConnection(E, D);
            right.AddConnection(E, F);
            RuleGrammar ruleS1 = new RuleGrammar(left, right);

            //Start Rule 2
            left = new Graph("Left Start");
            left.AddNode(new StartNode());
            right = new Graph("Right Start");
            A = new EntranceNode();
            B = new NTNode("B");
            C = new NTNode("C");
            D = new GoalNode();
            right.AddNodes(new List<Node> { A, B, C, D });
            right.AddConnection(A, B);
            right.AddConnection(A, C);
            right.AddConnection(C, D);
            right.AddConnection(B, D);
            RuleGrammar ruleS2 = new RuleGrammar(left, right);


            //Rule 2
            Node LA; Node LB; Node LC; 
            Node R1; Node R2; Node R3; Node R4; Node R5;

            left = new Graph("LeftGrow");
            LB = new NTNode("LB");
            LC = new NTNode("LC");
            left.AddNodes(new List<Node> { LB, LC });
            left.AddConnection(LB, LC);
            right = new Graph("Right Grow");
            R1 = new Node("R1a");
            R2 = new NTNode("R2a");
            R3 = new Node("R3a");
            R4 = new NTNode("R4a");
            R5 = new Node("R5a");
            right.AddNodes(new List<Node> { R1, R2, R3, R4, R5 });
            right.AddConnection(R1, R3);
            right.AddConnection(R3, R4);
            right.AddConnection(R4, R5);
            right.AddConnection(R5, R2);
            RuleGrammar ruleG1 = new RuleGrammar(left, right);

            //Rule 3
            left = new Graph("LeftGrow");
            LA = new NTNode("LA");
            left.AddNodes(new List<Node> { LA});
            right = new Graph("Right Grow");
            R1 = new Node("R1");
            R2 = new NTNode("R2");
            right.AddNodes(new List<Node> { R1, R2});
            right.AddConnection(R1, R2);
            RuleGrammar ruleG2 = new RuleGrammar(left, right);

            //Rule 4
            left = new Graph("LeftGrow");
            LA = new NTNode("LA");
            left.AddNodes(new List<Node> { LA });
            right = new Graph("Right Grow");
            R1 = new Node("R1b");
            R2 = new Node("R2b");
            right.AddNodes(new List<Node> { R1, R2 });
            right.AddConnection(R1, R2);
            RuleGrammar ruleG3 = new RuleGrammar(left, right);

            //Rule 5
            left = new Graph("LeftGrow");
            LA = new NTNode("LA");
            LB = new NTNode("LB");
            left.AddNodes(new List<Node> { LA , LB});
            right = new Graph("Right Grow");
            R1 = new Node("R1b");
            R2 = new Node("R2b");
            R3 = new Node("R3b");
            R4 = new NTNode("R4b");
            right.AddNodes(new List<Node> { R1, R2, R3 ,R4 });
            right.AddConnection(R1, R3);
            right.AddConnection(R3, R2);
            right.AddConnection(R4, R2);
            RuleGrammar ruleG4 = new RuleGrammar(left, right);

            //Rule 5
            left = new Graph("LeftGrow");
            LA = new NTNode("LA");
            LB = new Node("LB");
            left.AddNodes(new List<Node> { LA, LB });
            right = new Graph("Right Grow");
            R1 = new Node("R1b");
            R2 = new Node("R2b");
            R3 = new Node("R3b");
            R4 = new NTNode("R4b");
            R5 = new Node("R5b");
            right.AddNodes(new List<Node> { R1, R2, R3, R4,R5 });
            right.AddConnection(R1, R3);
            right.AddConnection(R3, R5);
            right.AddConnection(R5, R4);
            right.AddConnection(R5, R2);
            RuleGrammar ruleG5 = new RuleGrammar(left, right);

            /*
            Grammar.ApplyGrammarTo(graph, ruleS2, 1);
            Grammar.ApplyGrammarTo(graph, ruleG5, 1);
            Grammar.ApplyGrammarTo(graph, ruleG2, 1);
            Grammar.ApplyGrammarTo(graph, ruleG1, 1);
            Grammar.ApplyGrammarTo(graph, ruleG4, 1);
            Grammar.ApplyGrammarTo(graph, ruleG5, 1);

            /*
            var assignments = Grammar.FindIsomorphism(graph, left);
            if (assignments != null)
            {
                Grammar.SinglePushOut(assignments, graph, left, right);
                Debug.Log(graph.ToString());
            }
            assignments = Grammar.FindIsomorphism(graph, left);
            if (assignments != null)
            {
                Grammar.SinglePushOut(assignments, graph, left, right);
                Debug.Log(graph.ToString());
            }
            */
        }

    }
    public class RuleGrammar
    {
        public Graph left;
        public Graph right;
        public RuleGrammar(Graph _left, Graph _right)
        {
            left = _left;
            right = _right;
        }
    }
}