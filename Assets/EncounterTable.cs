using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoomFilling;

public class EncounterTable : MonoBehaviour
{
    [SerializeField]
    LevelEncounters Level1;
    [SerializeField]
    LevelEncounters Level2;
    [SerializeField]
    LevelEncounters Level3;
    public EncounterFactory GetEncounterFactory(int level, int difficulty)
    {
        if (level == 1)
        {
            if (difficulty == 1)
            {
                return Level1.easy;
            }
            else if (difficulty == 2)
            {
                return Level1.dangerous;
            }
            else if (difficulty == 3)
            {
                return Level1.lethal;
            }
        }
        else if (level == 2)
        {
            if (difficulty == 1)
            {
                return Level2.easy;
            }
            else if (difficulty == 2)
            {
                return Level2.dangerous;
            }
            else if (difficulty == 3)
            {
                return Level2.lethal;
            }
        }
        else if (level == 3)
        {
            if (difficulty == 1)
            {
                return Level3.easy;
            }
            else if (difficulty == 2)
            {
                return Level3.dangerous;
            }
            else if (difficulty == 3)
            {
                return Level3.lethal;
            }
        }
        throw new System.Exception("Invalid level or difficulty");
    }
}
[System.Serializable]
public class LevelEncounters
{
    public EncounterFactory easy;
    public EncounterFactory dangerous;
    public EncounterFactory lethal;
}
