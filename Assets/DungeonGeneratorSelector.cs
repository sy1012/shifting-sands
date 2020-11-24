using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DungeonVariant
{
    tiny,
    medium,
    none
}

public class DungeonGeneratorSelector : MonoBehaviour
{
    [SerializeField]
    LevelGenerator tinyDungeon;
    [SerializeField]
    LevelGenerator mediumDungeon;
    [SerializeField]
    bool newLevelTrigger = false;


    public DungeonVariant currentVariant;
    // Start is called before the first frame update
    void Start()
    {
        switch (currentVariant)
        {
            case DungeonVariant.tiny:
                tinyDungeon.gameObject.SetActive(true);
                break;
            case DungeonVariant.medium:
                mediumDungeon.gameObject.SetActive(true);
                break;
            case DungeonVariant.none:
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        if (newLevelTrigger)
        {
            newLevelTrigger = false;

            tinyDungeon.gameObject.SetActive(false);
            mediumDungeon.gameObject.SetActive(false);

            switch (currentVariant)
            {
                case DungeonVariant.tiny:
                    tinyDungeon.gameObject.SetActive(true);
                    tinyDungeon.MakeNewDungeon();
                    break;
                case DungeonVariant.medium:
                    mediumDungeon.gameObject.SetActive(true);
                    mediumDungeon.MakeNewDungeon();
                    break;
                case DungeonVariant.none:
                    break;
                default:
                    break;
            }
        }
    }
}
