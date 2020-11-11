using GraphGrammars;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

namespace RoomFilling
{
    public abstract class FillCommand
    {
        protected GenericFactory factory;
        public FillCommand(GenericFactory factory)
        {
            this.factory = factory;
        }
        public abstract void Execute();
    }
    public abstract class FillRoomCommand:FillCommand
    {
        Room room;

        protected FillRoomCommand(Room room, GenericFactory factory) :base(factory)
        {
            this.room = room;
        }
    }
    public abstract class FillDungeonCommand : FillCommand
    {
        List<Room> rooms;
        Graph graph;
        public FillDungeonCommand(List<Room> rooms, Graph graph, GenericFactory factory) :base(factory)
        {
            this.rooms = rooms;
            this.graph = graph;
        }
    }
    public class MakeEntranceAndExitCommand: FillDungeonCommand
    {
        public MakeEntranceAndExitCommand(List<Room> rooms, Graph graph, GenericFactory EntExitFactory) : base(rooms, graph, EntExitFactory)
        {
        }

        public override void Execute()
        {
            factory.GetNewInstance();
            //Move to correct room
        }
    }
    public class DungeonFillerController : MonoBehaviour
    {
        // Add Entrance
        // Add Exit
        // Add monsters val N to both rooms out side goal
        // Add monsters val N to room R
        Queue<FillCommand> commandQueue;
        List<Transform> rooms;
        Graph graph;
        GenericRandomFactory monsterFactory;
        GenericRandomFactory treasureFactory;
        GenericRandomFactory interactableFactory;
        public void Awake()
        {
            EventManager.onDungeonGenerated += GetDungeonInfo;
        }

        private void GetDungeonInfo(System.EventArgs e)
        {
            DungeonGenArgs de = (DungeonGenArgs) e;
            rooms = de.Rooms;
            graph = de.Graph;
        }

        public void AddCommand(FillCommand command)
        {
            commandQueue.Enqueue(command);
        }
        public void Fill()
        {
            FillCommand fc = commandQueue.Dequeue();
            fc.Execute();
        }
    }

}
