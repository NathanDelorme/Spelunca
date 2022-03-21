using UnityEngine;

/// <summary>
/// Cette classe permet de créer un fantome du joueur et de l'effacer au bout d'un certain temps.
/// Cela est utile pour faire l'effet de ghost du joueur quand il dash.
/// </summary>
public class GhostEffect : MonoBehaviour
{
    /// <value>
    /// Temsp restant avant que l'objet soit détruit
    /// </value>
    private float timer = 0.4f;
    /// <value>
    /// SpriteRenderer du joueur.
    /// </value>
    private SpriteRenderer sprite;

    /// <summary>
    /// Fonction exécuté avant la première frame du programme, donc avant le premier appel à Update.
    /// Cette fonction agit comme un constructeur permettant d'initialiser les attributs et effectuer des actions au chargement du script.
    /// </summary>
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        sprite.sprite = playerObject.GetComponent<SpriteRenderer>().sprite;
        sprite.color = new Vector4(50, 50, 50, 0.2f);
        transform.localPosition = playerObject.transform.localPosition;
    }

    /// <summary>
    /// Fonction exécuté un certain nombre de fois par seconde.
    /// </summary>
    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
        sprite.color = Color.Lerp(sprite.color, new Vector4(50, 50, 50, 0f), 0.4f);

        if (timer <= 0f)
            Destroy(gameObject);
    }
}
