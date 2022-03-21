using UnityEngine;

/// <summary>
/// Cette classe est le manager de toutes les orbes de dash présentent dans le niveau courant.
/// </summary>
public class OrbsDashManager : MonoBehaviour
{
    /// <value>
    /// Zone de collision du joueur.
    /// </value>
    public BoxCollider2D playerCollider;
    /// <value>
    /// Liste de toutes les orbes de dash présentent dans le niveau courant.
    /// </value>
    private DashOrb[] dashOrbPositionsList = { };

    /// <summary>
    /// Fonction exécuté avant la première frame du programme, donc avant le premier appel à Update.
    /// Cette fonction agit comme un constructeur permettant d'initialiser les attributs et effectuer des actions au chargement du script.
    /// </summary>
    void Start()
    {
        dashOrbPositionsList = FindObjectsOfType<DashOrb>();

        foreach (DashOrb dashOrb in dashOrbPositionsList)
            dashOrb.SetPlayerCollider(playerCollider);
    }

    /// <summary>
    /// Réinitialise toutes les orbes de dash. (utile quand le joueur meurt et qu'il faut réinitialiser le niveau).
    /// </summary>
    public void resetOrbs()
    {
        foreach (DashOrb dashOrb in dashOrbPositionsList)
            dashOrb.resetOrb();
    }
}
