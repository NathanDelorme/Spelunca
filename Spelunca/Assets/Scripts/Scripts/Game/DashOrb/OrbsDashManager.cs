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

    private DashOrb[] dashOrbPositionsList;

    /// <summary>
    /// Function executed at the start of the program.
    /// for each position in <c>dashOrbPositionsList</c> we instantiate a new dash orb from the prefab.
    /// </summary>
    void Start()
    {
        dashOrbPositionsList = FindObjectsOfType<DashOrb>();

        foreach (DashOrb dashOrb in dashOrbPositionsList)
        {
            dashOrb.SetPlayerCollider(playerCollider);
        }
    }
}
