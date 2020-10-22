using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class OverworldGeneration : MonoBehaviour
{
    // chance a tile will be a landmark (pyramid or ruins) (2%)
    private int landmarkChance = 2;
    // chance a landmark will be a pyramid (30%)
    private int pyramidChance = 30;
    // grid array
    private int[,] grid;

    // tile for desert
    public Tilemap desertMap;
    public Tile desertTile;
    // tile for oasis
    public Tilemap oasisMap;
    public Tile oasisTile;
    // tile for pyramid
    public Tilemap pyramidMap;
    public Tile pyramidTile;
    // tiles for ruins
    public Tilemap ruinsMap;
    public Tile ruins1Tile;
    public Tile ruins2Tile;
    public Tile ruins3Tile;
    public Tile ruins4Tile;
    public Tile ruins5Tile;

    // size of grid, and whole overworld
    // for now 100x100
    private int width = 100;
    private int height = 100;

    public void generate()
    {
        // initialize the tile maps and the grid
        desertMap.ClearAllTiles();
        oasisMap.ClearAllTiles();
        pyramidMap.ClearAllTiles();
        ruinsMap.ClearAllTiles();
        grid = new int[width, height];

        // fill and generate the grid
        // for every row
        for (int row = 0; row < height; row++)
        {
            // for every column
            for (int col = 0; col < width; col++)
            {
                // center of the grid is the starting oasis
                if (row == height / 2 && col == width / 2)
                {
                    // create oasis tile at this position
                    oasisMap.SetTile(new Vector3Int(-col + width / 2,
                    -row + height / 2, 0), oasisTile);
                }
                // determine if this tile will have a landmark
                else
                {
                    int landmark = Random.Range(1,101) < landmarkChance ? 1 : 0;
                    // if this is a landmark
                    if (landmark == 1)
                    {
                        // determine if this is pyramid
                        int pyramid = Random.Range(1,101) < pyramidChance ? 1 : 0;
                        // if this is a pyramid
                        if (pyramid == 1)
                        {
                            // create pyramid tile at this position
                            pyramidMap.SetTile(new Vector3Int(-col + width / 2,
                             -row + height / 2, 0), pyramidTile);
                        }
                        // ruins
                        else
                        {
                            // decide which ruins tile to use
                            int ruinsChoice = Random.Range(0,5);

                            if (ruinsChoice == 0)
                            {
                                // generate ruins tile 1
                                ruinsMap.SetTile(new Vector3Int(-col + width / 2,
                                -row + height / 2, 0), ruins1Tile);
                            }
                            else if (ruinsChoice == 1){
                                // generate ruins tile 2
                                ruinsMap.SetTile(new Vector3Int(-col + width / 2,
                                -row + height / 2, 0), ruins2Tile);
                            }
                            else if (ruinsChoice == 2)
                            {
                                // generate ruins tile 3
                                ruinsMap.SetTile(new Vector3Int(-col + width / 2,
                                -row + height / 2, 0), ruins3Tile);
                            }
                            else if (ruinsChoice == 3)
                            {
                                // generate ruins tile 4
                                ruinsMap.SetTile(new Vector3Int(-col + width / 2,
                                -row + height / 2, 0), ruins4Tile);
                            }
                            else
                            {
                                // generate ruins tile 5
                                ruinsMap.SetTile(new Vector3Int(-col + width / 2,
                                -row + height / 2, 0), ruins5Tile);
                            }
                        }
                    }
                }
                // create desrt tile for every position
                desertMap.SetTile(new Vector3Int(-col + width / 2,
                -row + height / 2, 0), desertTile);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // generate overworld
        generate();
    }
}
