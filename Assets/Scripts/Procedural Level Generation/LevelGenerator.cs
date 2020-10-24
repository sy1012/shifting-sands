using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphGrammars;
using System;

public class LevelGenerator : MonoBehaviour
{
    public GraphGenerator graphGenerator;
    public RoomGenerator roomGenerator;
    public GraphComponent graphComponent;
    private Graph graph;
    public ForceDirectedGraph fdg;
    public List<Transform> rooms;
    public List<Door> doors;
    

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
        //TODO why no work?
        //roomGenerator.SetGraphComp(graphComponent);
        //fdg.SetGraphComponent(graphComponent);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(newLevel))
        {
            graph = new Graph("Level Graph");
            graph.AddNode(new StartNode());
            graphGenerator.ResetForNewGeneration();
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
                SolveFDG_Recipe();
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


    private void SolveFDG_Recipe()
    {
        for (int i = 0; i < 15; i++)
        {
            fdg.SolveForceDrivenGraph(1f, 0.2f, 0.4f);
        }
        for (int i = 0; i < 15; i++)
        {
            fdg.SolveForceDrivenGraph(0.9f, 0.3f, 0.5f);
        }
        for (int i = 0; i < 10; i++)
        {
            fdg.SolveForceDrivenGraph(6f, 0.2f, 1f);
        }
        for (int i = 0; i < 10; i++)
        {
            fdg.SolveForceDrivenGraph(14f, 0.2f, 50f);
        }
        for (int i = 0; i < 10; i++)
        {
            fdg.SolveForceDrivenGraph(18f, 0.2f, 500f);
        }
        for (int i = 0; i < 10; i++)
        {
            fdg.SolveForceDrivenGraph(24f, 0.2f, 1300f);
        }
    }
}
