﻿using System.Collections.Generic;
using UnityEngine;
using GraphGrammars;

public class LevelGenerator : MonoBehaviour
{
    public GraphGenerator graphGenerator;
    public RoomGenerator roomGenerator;
    public GraphComponent graphComponent;
    private Graph graph;
    public ForceDirectedGraph fdg;
    public List<Transform> rooms;
    public List<Door> doors;
    public bool debuggingMode;

    KeyCode newLevel = KeyCode.N;

    // Start is called before the first frame update
    private void Awake()
    {
    }
    void Start()
    {
        if (graphGenerator == null || roomGenerator == null || fdg == null || graphComponent == null)
        {
            throw new MissingComponentException("Don't forget graphGenerator,roomGenerator, force directed graph, graph component dependencies");
        }
        int maxIterations = 10;
        int curIteration = 0;
        bool done = false;
        if (!debuggingMode)
        {
            while (!done && curIteration<=maxIterations)
            {
                done = MakeNewDungeon();
                curIteration++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!debuggingMode)
        {
            return;
        }
        if (Input.GetKeyDown(newLevel))
        {
            InitializeNewDungeon();
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (graph == null)
            {
                graph = new Graph("Level Graph");
                graph.AddNode(new StartNode());
                graphGenerator.ResetForNewGeneration();
            }
            if (!graphGenerator.completedGeneration)
            {
                graphGenerator.StepThroughGenerateGraph(graph);
                graphComponent.UpdateToGraph(graph);
                fdg.SolveFDG_Recipe();
                graphComponent.SnapToGrid();
                roomGenerator.Refresh();
                roomGenerator.MakeDoors();
                roomGenerator.MakeRooms();
                roomGenerator.MoveRoomsToNodes();
                rooms = roomGenerator.rooms;
                doors = roomGenerator.doors;
            }
        }
    }

    public void InitializeNewDungeon()
    {
        graph = new Graph("Level Graph");
        graph.AddNode(new StartNode());
        graphGenerator.ResetForNewGeneration();
    }

    public bool MakeNewDungeon()
    {
        InitializeNewDungeon();
        int i = 0; //safe guard
        while (!graphGenerator.completedGeneration && i < 100)
        {
            graphGenerator.StepThroughGenerateGraph(graph);
            graphComponent.UpdateToGraph(graph);
            fdg.SolveFDG_Recipe();
            graphComponent.SnapToGrid();
            roomGenerator.Refresh();
            roomGenerator.MakeDoors();
            roomGenerator.MakeRooms();
            roomGenerator.MoveRoomsToNodes();
            rooms = roomGenerator.rooms;
            doors = roomGenerator.doors;
            i++;
        }

        // Quality check dungeon
        int originRooms = 0;
        foreach (var room in roomGenerator.rooms)
        {
            if (room.position.magnitude <= 0.1f)
            {
                originRooms += 1;
            }
        }

        if (originRooms > 1)
        {
            //fail.
            Debug.Log("Fail");
            return false;
        }

        //Let everyone else know Dungeon skeleton has been made
        EventManager.TriggerDungeonGenerated(new DungeonGenArgs(this,doors, rooms, graph));
        return true;
    }

    public Room GetRoom(NodeComponent nc)
    {
        return roomGenerator.NodeCompRoomMap[nc];
    }

    private void OnDisable()
    {
        fdg.enabled = false;
        graphGenerator.enabled = false;
        roomGenerator.enabled = false;
    }
    private void OnEnable()
    {
        fdg.enabled = true;
        graphGenerator.enabled = true;
        roomGenerator.enabled = true;
    }


}
