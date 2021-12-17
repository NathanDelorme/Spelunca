using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This class is used as a manager for all dash orb of a level.
/// </summary>
public class OrbsDashManager : MonoBehaviour
{
    /// <value>
    /// The <c>playerCollider</c> property is a BoxCollider2D which represent the player hitbox.
    /// </value>
    public BoxCollider2D playerCollider;
    /// <value>
    /// The <c>dashOrbPrefab</c> property is a GameObject which is the dash orb prefab.
    /// </value>
    public GameObject dashOrbPrefab;
    /// <value>
    /// The <c>dashOrbPositionsList</c> property is a List of vector which are the positions of all dash orbs of the level.
    /// </value>
    public List<Vector2> dashOrbPositionsList;

    /// <summary>
    /// Function executed at the start of the program.
    /// for each position in <c>dashOrbPositionsList</c> we instantiate a new dash orb from the prefab.
    /// </summary>
    void Start()
    {
        foreach (Vector2 pos in dashOrbPositionsList)
        {
            GameObject dashOrb = Instantiate(dashOrbPrefab, pos, Quaternion.identity);
            dashOrb.GetComponent<DashOrb>().playerCollider = playerCollider;
        }
    }

    /// <summary>
    /// Function executed only in the unity editor.
    /// Draw on the editor the position of the dash orbs.
    /// </summary>
    void OnDrawGizmos()
    {
        foreach(Vector2 pos in dashOrbPositionsList)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(pos, 0.2f);
        }
    }
}
