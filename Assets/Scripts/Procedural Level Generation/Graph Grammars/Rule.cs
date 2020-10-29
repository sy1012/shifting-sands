using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphGrammars {
    public class Rule:MonoBehaviour
    {
        public string ruleName;
        public Expression leftHandExpression;
        public List<Expression> rightHandsExpressions;
        //When using prefabs. Its important to refresh them.
        public Expression GetLeftHandExpression()
        {
            leftHandExpression.Refresh();
            return leftHandExpression;
        }
        public Expression GetRightHandExpression()
        {
            //TODO implement probability for other list expressoins
            Expression e;
            if (rightHandsExpressions.Count == 1)
            {
                e = rightHandsExpressions[0];
            }
            else
            {
                e = rightHandsExpressions[UnityEngine.Random.Range(0, rightHandsExpressions.Count)];
            }
            e.Refresh();
            return e;
        }
        public override string ToString()
        {
            return ruleName;
        }
    }
}