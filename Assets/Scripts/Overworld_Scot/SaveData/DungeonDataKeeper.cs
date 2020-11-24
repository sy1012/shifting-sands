using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DungeonDataKeeper : MonoBehaviour
{
    public bool beatLastDungeon = false;
    public float dungeonDistance = 0;
    public DungeonVariant dungeonVariant = DungeonVariant.tiny;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
