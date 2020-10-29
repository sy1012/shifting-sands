using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphGrammars;

[CreateAssetMenu]
[System.Serializable]
public class Recipe : ScriptableObject
{
    [SerializeField]
    public List<string> instructions;
    public List<Rule> rules;
    public List<Rule> GetInstruction(int stepIndex)
    {
        var instruction = instructions[stepIndex];
        if (instruction == "???")
        {
            return new List<Rule>(rules);
        }
        var splitInstruction = instruction.Split('|');
        var result = new List<Rule>();
        foreach (var item in splitInstruction)
        {
            result.Add(rules[int.Parse(item)]);
        }
        return result;
    }
}

/*
private void ApplyRule(KeyValuePair<string, RuleType> instruction)
{
    Graph ExecuteRule(bool commented, RuleType ruleType, Graph g)
    {
        var grammar = new Grammar(g, ruleType);
        List<Graph> potentialMatches = MatchFinder.FindMatch(grammar);

        return grammar.outputG;

    }
    Graph IterateRule(bool commented, RuleType ruleType, int iterations, Graph g)
    {
        var graph = new Graph();
        for (int i = 0; i < iterations; i++)
        {
            ExecuteRule(commented, ruleType, g);
        }
    }

    public void SetOutputGraph(Graph output)
{
    outputG = output;
}*/
