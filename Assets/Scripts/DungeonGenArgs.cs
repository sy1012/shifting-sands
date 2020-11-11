using System;
using System.Collections;
using System.Collections.Generic;
using GraphGrammars;
using UnityEngine;

public class DungeonGenArgs:EventArgs
{
    public LevelGenerator generator;
    List<Door> doors;
    List<Transform> rooms;
    Graph graph;

    public DungeonGenArgs(LevelGenerator sender,List<Door> doors, List<Transform> rooms, Graph graph)
    {
        this.doors = doors;
        this.rooms = rooms;
        this.graph = graph;
        generator = sender;
    }

    public List<Door> Doors { get => doors; set => doors = value; }
    public List<Transform> Rooms { get => rooms; set => rooms = value; }
    public Graph Graph { get => graph; set => graph = value; }


}