using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DungeonVariant
{
    tiny,
    rhoss,
    anubis,
    none
}

public class DungeonGeneratorSelector : MonoBehaviour
{
    [SerializeField]
    LevelGenerator tinyDungeon;
    [SerializeField]
    LevelGenerator rhossDungeon;
    [SerializeField]
    LevelGenerator anubisDungeon;
    [SerializeField]
    bool newLevelTrigger = false;


    public DungeonVariant currentVariant;
    // Start is called before the first frame update
    void Start()
    {
        //  Get dungeon information from overworld
        var ddkeeper = FindObjectOfType<DungeonDataKeeper>();
        if (ddkeeper!= null)
        {
            currentVariant = ddkeeper.dungeonVariant;
        }

        //  Select which generator to use
        switch (currentVariant)
        {
            case DungeonVariant.tiny:
                tinyDungeon.gameObject.SetActive(true);
                break;
            case DungeonVariant.rhoss:
                rhossDungeon.gameObject.SetActive(true);
                break;
            case DungeonVariant.anubis:
                anubisDungeon.gameObject.SetActive(true);
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
            rhossDungeon.gameObject.SetActive(false);
            anubisDungeon.gameObject.SetActive(false);

            switch (currentVariant)
            {
                case DungeonVariant.tiny:
                    tinyDungeon.gameObject.SetActive(true);
                    tinyDungeon.MakeNewDungeon();
                    break;
                case DungeonVariant.rhoss:
                    rhossDungeon.gameObject.SetActive(true);
                    rhossDungeon.MakeNewDungeon();
                    break;
                case DungeonVariant.anubis:
                    anubisDungeon.gameObject.SetActive(true);
                    anubisDungeon.MakeNewDungeon();
                    break;
                case DungeonVariant.none:
                    break;
                default:
                    break;
            }
        }
    }
}
