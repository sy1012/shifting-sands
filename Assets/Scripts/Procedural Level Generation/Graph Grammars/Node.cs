using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphGrammars
{
    [System.Serializable]
    public class Node
    {
        [SerializeField]
        protected Symbol symbol;
        public Symbol GetSymbol{ get { return symbol; } }
        [SerializeField]
        protected string name;
        protected Vector2 position;
        public Node()
        {
            symbol = Symbol.t;
            name = "N";
        }
        public Node(string _name)
        {
            symbol = Symbol.t;
            name = _name;
        }

        public Node(string _name, Vector2 position) : this(_name)
        {
            this.position = position;
        }

        public override string ToString()
        {
            return name +": "+ GetHashCode().ToString().Substring(6);
        }

        public virtual Node Copy()
        {
            return new Node(name,position);
        }
        public Vector2 Position { get { return position; } }
        public void SetPosition(Vector2 _position) { position = _position; }
    }

    [System.Serializable]
    public class StartNode : Node
    {
        public StartNode():base("Start")
        {
            symbol = Symbol.Start;
        }
        public StartNode(Vector2 pos):base("Start",pos)
        {
            symbol = Symbol.Start;
        }
        public override Node Copy()
        {
            return new StartNode(position);
        }

    }
    [System.Serializable]
    public class EntranceNode : Node
    {
        public EntranceNode():base("Ent")
        {
            symbol = Symbol.Entrance;
        }
        public EntranceNode(Vector2 pos) : base("Ent",pos)
        {
            symbol = Symbol.Entrance;
        }
        public override Node Copy()
        {
            return new EntranceNode(position);
        }
    }
    [System.Serializable]
    public class GoalNode : Node
    {
        public GoalNode() : base("Goal")
        {
            base.symbol = Symbol.Goal;
        }
        public GoalNode(Vector2 pos) : base("Goal",pos)
        {
            base.symbol = Symbol.Goal;
        }
        public override Node Copy()
        {
            return new GoalNode(position);
        }
    }
    [System.Serializable]
    public class NTNode : Node
    {
        public NTNode(string _name)
        {
            base.symbol = Symbol.NT;
            name = _name;
        }

        public NTNode(string _name, Vector2 position) : base(_name, position)
        {
            symbol = Symbol.NT;
        }

        public override Node Copy()
        {
            return new NTNode(name,position);
        }
    }
    public class LockNode : Node
    {
        public LockNode(string _name)
        {
            base.symbol = Symbol.NT;
            name = _name;
        }

        public LockNode(string _name, Vector2 position) : base(_name, position)
        {
            symbol = Symbol.NT;
        }

        public override Node Copy()
        {
            return new LockNode(name, position);
        }
    }
    public class KeyNode: Node
    {
        public KeyNode(string _name)
        {
            base.symbol = Symbol.NT;
            name = _name;
        }

        public KeyNode(string _name, Vector2 position) : base(_name, position)
        {
            symbol = Symbol.NT;
        }

        public override Node Copy()
        {
            return new KeyNode(name, position);
        }
    }
    public class CurseNode: Node
    {
        public CurseNode(string _name)
        {
            base.symbol = Symbol.Curse;
            name = _name+":Curse";
        }

        public CurseNode(string _name, Vector2 position) : base(_name, position)
        {
            symbol = Symbol.Curse;
        }

        public override Node Copy()
        {
            return new CurseNode(name, position);
        }
    }
    public class RelicNode : Node
    {
        public RelicNode(string _name)
        {
            base.symbol = Symbol.Relic;
            name = _name + ":Relic";
        }

        public RelicNode(string _name, Vector2 position) : base(_name, position)
        {
            symbol = Symbol.Relic;
        }

        public override Node Copy()
        {
            return new RelicNode(name, position);
        }
    }

    public class OasisNode : Node
    {
        Oasis oasis;
        public OasisNode(string _name, Oasis myOasis)
        {
            name = _name;
            oasis = myOasis;
            
        }

        public Oasis getOasis()
		{
            return oasis;
		}

        public override Node Copy()
        {
            return new OasisNode(name, oasis);
        }
    }
    // t:terminal , NT: non terminal
    public enum Symbol { t ,Entrance,Goal,NT,Start,Key,Lock,Curse,Relic}
}