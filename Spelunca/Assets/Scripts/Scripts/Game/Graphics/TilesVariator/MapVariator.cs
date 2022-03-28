using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace VariatorSystem
{
    /// <summary>
    /// Permet de faire varier l'apparition des tuiles afin d'éviter la répétition des même tuiles.
    /// Chaque tuile à un poids permettantde définir sa probabilité d'apparition.
    /// Ce code est en partie récupérer d'un script provenant d'un tutoriel.
    /// </summary>
    public class MapVariator : MonoBehaviour
    {
        /// <value>
        /// Contient la tile map sur laquelle le script va agir.
        /// </value>
        [SerializeField]
        private Tilemap map;
        /// <value>
        /// Contient les différents pack à randomiser
        /// </value>
        [SerializeField]
        private List<VariationPack> packs;

        /// <summary>
        /// Créer la variation des tuiles dans la map
        /// </summary>
        [ContextMenu("Make Map Variations")]
        private void GoThroughTheMap()
        {
            map.CompressBounds();

            Vector3Int bottomLeft = map.origin;
            Vector3Int topRight = map.origin + map.size;

            for (int x = bottomLeft.x; x < topRight.x; x++)
            {
                for (int y = bottomLeft.y; y < topRight.y; y++)
                {
                    Vector3Int tilePos = new Vector3Int(x, y, map.origin.z);
                    TileBase tile = map.GetTile(tilePos);

                    int tileID = GetPackID(tile);

                    if (tileID > -1)
                    {
                        TileBase newTile = GetTileWithFrequency(tileID);
                        Debug.Log("TileID : " + tileID + " | Current : " + tile.name + " | New : " + newTile.name);
                        map.SetTile(tilePos, newTile);
                    }
                }
            }
        }

        /// <summary>
        /// Récupère l'identifiant du pack à partir du nom d'une tuile.
        /// </summary>
        /// <param name="tile">Nom de la tuile</param>
        /// <returns>ID du pack</returns>
        private int GetPackID(TileBase tile)
        {
            foreach (VariationPack pack in packs)
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

        /// <summary>
        /// Récupère une tuile aléatoirement en fonction du "poid" alloué à chaque tuile.
        /// </summary>
        /// <param name="tileID">ID</param>
        /// <returns>Tuile randomisé</returns>
        private TileBase GetTileWithFrequency(int tileID)
        {
            List<TileBase> tileList = new List<TileBase>();

            foreach (VariationPack pack in packs)
                for (int i = 0; i < pack.frequency; i++)
                    tileList.Add(pack.tiles[tileID]);

            int randomTileNumber = Random.Range(0, tileList.Count);
            return tileList[randomTileNumber];
        }
    }
}
