using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DungeonDataKeeper : MonoBehaviour
{

    public static DungeonDataKeeper instance = null;


    public bool beatLastDungeon = false;
    public float dungeonDistance = 0;
    public DungeonVariant dungeonVariant = DungeonVariant.tiny;

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
