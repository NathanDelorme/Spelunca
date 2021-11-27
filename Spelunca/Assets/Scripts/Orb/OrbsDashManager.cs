using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbsDashManager : MonoBehaviour
{
    public BoxCollider2D playerCollider;
    public GameObject dashOrbPrefab;
    public List<Vector2> dashOrbPositionsList;

    void Start()
    {
        foreach (Vector2 pos in dashOrbPositionsList)
        {
            GameObject dashOrb = Instantiate(dashOrbPrefab, pos, Quaternion.identity);
            dashOrb.GetComponent<DashOrb>().playerCollider = playerCollider;
        }
    }

    void OnDrawGizmos()
    {
        foreach(Vector2 pos in dashOrbPositionsList)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(pos, 0.2f);
        }
    }
}
