using System.Collections.Generic;
using UnityEngine.Tilemaps;

namespace VariatorSystem
{
    /// <summary>
    /// Structure permettant de stocker des tuiles et leur fréquence d'apparition.
    /// </summary>
    [System.Serializable]
    public class VariationPack
    {
        /// <value>
        /// Liste des tuiles du pack.
        /// </value>
        public List<TileBase> tiles;
        /// <value>
        /// Frequence. Plus ce nombre est élevé, plus les tuiles apparaîtront.
        /// </value>
        public int frequency;
    }
}
