using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapVariator : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;
    [SerializeField]
    private List<VariationPack> packs;

    [ContextMenu("Make Map Variations")]
    private void GoThroughTheMap()
    {
        map.CompressBounds();

        Vector3Int bottomLeft = map.origin;
        Vector3Int topRight = map.origin + map.size;

        for(int x = bottomLeft.x; x <topRight.x; x++)
        {
            for (int y = bottomLeft.y; y < topRight.y; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, map.origin.z);
                TileBase tile = map.GetTile(tilePos);

                int tileID = GetPackID(tile);

                if(tileID > -1)
                {
                    TileBase newTile = GetTileWithFrequency(tileID);
                    Debug.Log("TileID : " + tileID + " | Current : " + tile.name + " | New : " + newTile.name);
                    map.SetTile(tilePos, newTile);
                }
            }
        }
    }

    private int GetPackID(TileBase tile)
    {
        foreach(VariationPack pack in packs)
        {
            int i = 0;
            foreach (TileBase packedTile in pack.tiles)
            {
                if (tile == packedTile)
                    return i;
                i++;
            }
        }

        return -1;
    }

    private TileBase GetTileWithFrequency(int tileID)
    {
        List<TileBase> tileList = new List<TileBase>();
        foreach (VariationPack pack in packs)
        {
            for(int i = 0; i < pack.frequency; i++)
            {
                tileList.Add(pack.tiles[tileID]);
            }
        }
        int randomTileNumber = Random.Range(0, tileList.Count);
        return tileList[randomTileNumber];
    }
}
