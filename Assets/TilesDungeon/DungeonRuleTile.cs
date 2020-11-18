using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class DungeonRuleTile : RuleTile<DungeonRuleTile.Neighbor> {

    public bool isWall;
    public bool isFloor;
    public bool isDoor;


    public class Neighbor : RuleTile.TilingRule.Neighbor {
        public const int IsFloor = 3;
        public const int IsNotFloor = 4;
        public const int IsWall = 5;
        public const int IsNotWall = 6;
        public const int IsDoor = 7;
        public const int IsNotDoor = 8;
    }

    public override bool RuleMatch(int neighbor, TileBase tile) {
        var other = tile as DungeonRuleTile;
        switch (neighbor) {
            case Neighbor.IsFloor: return other && other.isFloor;
            case Neighbor.IsNotFloor: return other && !other.isFloor;
            case Neighbor.IsWall: return other && other.isWall;
            case Neighbor.IsNotWall: return other && !other.isWall;
            case Neighbor.IsDoor: return other && !other.isDoor;
            case Neighbor.IsNotDoor: return other && !other.isDoor;
        }
        return base.RuleMatch(neighbor, tile);
    }
}
