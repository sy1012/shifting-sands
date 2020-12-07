using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DungeonDataKeeper : MonoBehaviour
{

    public static DungeonDataKeeper instance = null;


    public bool beatLastDungeon = false;
    public int levelsBeat = 0;
    public bool beatRhoss = false;
    public bool beatAnubis = false;
    public float dungeonDistance = 0;
    public DungeonVariant dungeonVariant = DungeonVariant.tiny;
    public float curseValue = 1;
    public float blessingValue = 1;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    public static DungeonDataKeeper getInstance()
    {
        if (instance == null)
        {
            var newDDK = new GameObject("DungeonDataKeeper");
            newDDK.AddComponent<DungeonDataKeeper>();
        }
        return instance;
    }
}
