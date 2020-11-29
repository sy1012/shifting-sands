using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class ColorToPrefab
{
    public Color color;
    public TileBase tile;
    public Color tintColor;
}

public class ImageToTile : MonoBehaviour
{
    public Texture2D map;
    public Room room;
    public SpriteRenderer spriteRenderer;
    public ColorToPrefab[] mappings;
    public Tilemap tilemap;
    public int rotation = 0;
    private void Start()
    {
        if (map == null)
        {
            map = spriteRenderer.sprite.texture;
        }
        if (room == null)
        {
            room = GetComponentInParent<Room>();
        }
        rotation = (int)room.transform.eulerAngles.z;
        transform.Rotate(new Vector3(0, 0, -rotation));
        if (rotation == 180 || rotation == 0)
        {
            transform.localPosition = Rotate((int)transform.localPosition.x, (int)transform.localPosition.y);
        }
        else
        {
            transform.localPosition = -Rotate((int)transform.localPosition.x, (int)transform.localPosition.y);
        }
        GenerateRoom();
        //spriteRenderer.enabled = false;
    }

    private void GenerateRoom()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    public Vector3Int Rotate(int x, int y) {
        if (rotation == 0)
        {
            return new Vector3Int(x, y, 0);
        }
        else if (rotation == 90) 
        {
            return new Vector3Int(-y, x, 0);
        }
        else if (rotation == 180)
        {
            return new Vector3Int(-x, -y, 0);
        }
        else if (rotation == 270)
        {
            return new Vector3Int(y, -x, 0);
        }
        return Vector3Int.zero;
    }
    private void GenerateTile(int x, int y)
    {
        Color pixelColor =  map.GetPixel(x,y);
        if (pixelColor.a == 0)
        {
            // transparent pixel ignor it
            return;
        }
        foreach (var colorMap in mappings)
        {
            if (colorMap.color.r.Equals(pixelColor.r))
            {
                int offset = 7;
                //Rotate an offset to center pixel by the rotation
                Vector3Int pos = Rotate(x-offset, y-offset);
                //Match
                //Debug.Log("Match");

                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), colorMap.tile);
                tilemap.SetColor(new Vector3Int(pos.x, pos.y, 0), colorMap.tintColor);
            }
        }
    }
}
