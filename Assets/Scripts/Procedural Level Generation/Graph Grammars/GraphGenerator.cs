using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GraphGrammars
{
    public class GraphGenerator : MonoBehaviour
    {
        public List<Recipe> recipes;
        public bool completedGeneration;
        private Recipe currentRecipe;
        private int currentStep = 0;

        // Start is called before the first frame update
        void Start()
        {
        }
        public Graph GenerateGraph()
        {
            var g = new Graph();
            g.AddNode(new StartNode());
            //Pipe Line
            Stack<Graph> undo = new Stack<Graph>();
            //undo.Push(new Graph(g));
            foreach (var recipe in recipes)
            {
                for (int step = 0; step < recipe.instructions.Count; step++)
                {
                    var instruction = recipe.GetInstruction(step);
                    //Grammar.ApplyGrammarTo(g, instruction, 1);
                }
            }
            return g;
        }
        public void StepThroughGenerateGraph(Graph g)
        {
            bool done = false;
            var instructionRules = currentRecipe.GetInstruction(currentStep);
            while (!done && instructionRules.Count>0)
            {
                var rngRule = instructionRules[UnityEngine.Random.Range(0, instructionRules.Count)];
                instructionRules.Remove(rngRule);
                done = Grammar.ApplyGrammarTo(g, rngRule);

            }
            if (!done && instructionRules.Count == 0)
            {
                //Debug.Log("Exhausted options for rules to apply at step: " + currentStep + ", moving on to next step if any" );
            }
            //if this is the last recipe
            if (currentStep == currentRecipe.instructions.Count-1)
            {
                //if this is the last step
                if (currentRecipe == recipes[recipes.Count-1])
                {
                    completedGeneration = true;
                }
                else
                {
                    var nextRecipe = recipes[recipes.IndexOf(currentRecipe) + 1];
                    currentRecipe = nextRecipe;
                    currentStep = 0;
                    return;
                }
            }
            else
            {
                currentStep++;
            }
        }
        public void ResetForNewGeneration()
        {
            currentRecipe = recipes[0];
            currentStep = 0;
            completedGeneration = false;
        }
    }
}