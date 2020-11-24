using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="RoomPicker",menuName ="ScriptableObjects/RoomPicker",order =1)]
public class RoomPicker : ScriptableObject
{
    public List<GameObject> OneDoorRooms;
    public List<GameObject> TwoDoorRooms;
    public List<GameObject> ThreeDoorRooms;
    public List<GameObject> FourDoorRooms;


    public GameObject GetRoomMatch(Stack<Door> doors, NodeComponent node)
    {
        List<Door> ordered = new List<Door>();
        string headings = "";
        while (doors.Count != 0)
        {
            var top = doors.Pop();
            ordered.Add(top);
            char heading = top.GetDoorHeadingByNode(node);
            headings += heading;
        }
        //Debug.Log(headings + "  " + node.name);

        int n = ordered.Count;
        List<GameObject> objectPool = new List<GameObject>();
        List<string> roomPool = new List<string>();
        if (n==1)
        {
            objectPool = OneDoorRooms;

        }
        else if (n==2)
        {
            objectPool = TwoDoorRooms;
        }
        else if (n == 3)
        {
            objectPool = ThreeDoorRooms;
        }
        else if (n == 4)
        {
            objectPool = FourDoorRooms;
        }

        roomPool = new List<string>();
        foreach (var obj in objectPool)
        {
            roomPool.Add(obj.GetComponent<Room>().doorHeadings);
        }
        if (objectPool.Count != roomPool.Count)
        {
            Debug.Log("Error. Picker headings list do not match room prefab list");
            return null;
        }
        float rotation = 0;
        List<GameObject> canidates = new List<GameObject>() ;
        List<float> canidatesRots = new List<float>() ;
        //Rotation for loop
        for (int i = 0; i < 4; i++)
        {

            //Iterate over relevant room pool
            int j = 0;
            foreach (var roomCode in roomPool)
            {
                bool match = StringMatch(headings, roomCode);
                if (match)
                {
                    canidates.Add(objectPool[j]);
                    canidatesRots.Add(rotation);
                }
                j++;
            }

            headings = RotateString(headings);
            rotation += 90;
        }
        int randomCanidateIndex = UnityEngine.Random.Range(0, canidates.Count);

        if (canidates.Count == 0)
        {
            Debug.Log("No rooms were found for node" + node.transform.name + ", edges: " + n + ", headings: "+ headings);
        }
        GameObject chosenRoom = canidates[randomCanidateIndex];
        float chosenRoomRot = canidatesRots[randomCanidateIndex];
        chosenRoom = Instantiate(chosenRoom,Vector3.zero,Quaternion.identity) as GameObject;
        chosenRoom.transform.Rotate(Vector3.forward, chosenRoomRot);

        //* Logging
        //Debug.Log(node.name + " has : " + canidates.Count + " canidates," + headings + " chose canidate index: "+randomCanidateIndex);
        //* End Logging

        return chosenRoom;
    }

    private string RotateString(string headings)
    {
        Dictionary<char, char> rotHeadings = new Dictionary<char, char>() { { 'N', 'E' }, { 'E', 'S' }, { 'S', 'W' }, { 'W', 'N' },{ '*','*'} };
        int n = headings.Length;
        char[] newHeadings = new char[n];
        for (int i = 0; i < n; i++)
        {
            char c = headings[i];
            newHeadings[i] = rotHeadings[c];
            //headings.Insert(i, rotHeadings[c].ToString());
            //headings.Remove(i+1,1);
        }
        return new string(newHeadings);
    }

    private bool StringMatch(string headings, string roomCode)
    {
        int n = headings.Length;
        List<char> rcList = new List<char>();
        foreach (var c in roomCode)
        {
            if (c == '*')
            {
                //Dont need to check wild cards
                continue;
            }
            rcList.Add(c);
        }
        foreach (var c in headings)
        {
            if (rcList.Contains(c))
            {
                rcList.Remove(c);
            }
        }
        if (rcList.Count==0)
        {
            return true;
        }

        return false;
    }
}
