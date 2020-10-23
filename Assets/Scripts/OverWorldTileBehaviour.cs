using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEngine.SceneManagement;

public class OverWorldTileBehaviour : MonoBehaviour
{
    // maps to be used
    public Tilemap pyramidMap;
    public Tilemap oasisMap;
    public Tilemap desertMap;
    public Tilemap ruinsMap;

    // tiles to be used
    public Tile pyramidTile;
    public Tile oasisTile;
    public Tile desertTile;
    public Tile ruins1Tile;
    public Tile ruins2Tile;
    public Tile ruins3Tile;
    public Tile ruins4Tile;
    public Tile ruins5Tile;

    // Update is called once per frame
    void Update()
    {
        // if left mouse clicked
        if (Input.GetMouseButtonDown(0))
        {
            // get position of mouse on the grid
            Vector3Int position = desertMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Debug.Log(position);

            // check if tile is a pyramid
            Tile tile1 = pyramidMap.GetTile<Tile>(position);
            Debug.Log(tile1);
            // if this tile was a pyramid tile
            if (tile1 != null && tile1 == pyramidTile)
            {
                // load the new scene
                SceneManager.LoadScene(sceneName: "EnemyAI");
                return;
            }

            // check if tile is a ruins
            Tile tile2 = ruinsMap.GetTile<Tile>(position);
            Debug.Log(tile2);
            // if this tile was a ruins tile
            if (tile2 != null && (tile2 == ruins1Tile || tile2 == ruins2Tile || tile2 == ruins3Tile || tile2 == ruins4Tile || tile2 == ruins5Tile))
            {
                // send a message to debug console
                Debug.Log("Hey this is a ruins tile.");
                return;
            }

            // check if tile is an oasis
            Tile tile3 = oasisMap.GetTile<Tile>(position);
            Debug.Log(tile3);
            // if this tile was an oasis tile
            if (tile3 != null && tile3 == oasisTile)
            {
                // send a message to debug console
                Debug.Log("Hey this is an oasis tile.");
                return;
            }

            // check if tile is desert
            Tile tile4 = desertMap.GetTile<Tile>(position);
            Debug.Log(tile4);
            // if this tile was a desert tile
            if (tile4 != null && tile4 == desertTile)
            {
                // send a message to debug console
                Debug.Log("Hey this is a desert tile.");
                return;
            }
        }
    }
}
