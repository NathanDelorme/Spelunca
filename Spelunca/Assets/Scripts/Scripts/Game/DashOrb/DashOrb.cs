using UnityEngine;

/// <summary>
/// Cette classe définit le comportement d'une orbe de dash.
/// </summary>
public class DashOrb : MonoBehaviour
{
    /// <value>
    /// Cette propriété (<see cref="PlayerState"/>) est un ScriptableObject.
    /// Cet attributs stocke toutes les variables utile pour savoir ce que le joueur veut faire,
    /// ce qu'il peut faire, ainsi que ce qu'il est en train de faire.
    /// </value>
    public PlayerState playerState;
    /// <value>
    /// Zone de collision du joueur.
    /// </value>
    private BoxCollider2D playerCollider;
    /// <value>
    /// Zone de collision de l'orbe de dash.
    /// </value>
    private PolygonCollider2D _collider;
    /// <value>
    /// SpriteRenderer de l'orbe de dash.
    /// </value>
    private SpriteRenderer _sprite;
    /// <value>
    /// Nombre floatant qui contient le nombre de secondes nécéssaires pour réactiver l'orbe de dash si elle a été consommé.
    /// </value>
    private float _objectTotalTime = 2f;
    /// <value>
    /// Temps restant avant la réactivation de l'orbe de dash.
    /// </value>
    private float _objectTime = 0f;
    /// <value>
    /// Contient la valeur courante de l'état de l'orbe (activée ou désactivé).
    /// </value>
    private bool _isEnable = true;

    /// <summary>
    /// Fonction exécuté avant la première frame du programme, donc avant le premier appel à Update.
    /// Cette fonction agit comme un constructeur permettant d'initialiser les attributs et effectuer des actions au chargement du script.
    /// </summary>
    void Start()
    {
        _collider = GetComponent<PolygonCollider2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Permet de set l'attribut de la zone de collision du joueur.
    /// </summary>
    /// <param name="collider">Zone de collision du joueur</param>
    public void SetPlayerCollider(BoxCollider2D collider)
    {
        playerCollider = collider;
    }

    /// <summary>
    /// Fonction qui reset l'état de l'orbe à son état initial (activée).
    /// </summary>
    public void resetOrb()
    {
        _isEnable = true;
        _objectTime = 0f;
        _sprite.color = Color.white;
    }

    /// <summary>
    /// Fonction qui retourne si le joueur entre en collision avec l'orbe de dash.
    /// </summary>
    /// <returns></returns>
    private bool CheckCollision()
    {
        return _collider.IsTouching(playerCollider);
    }

    /// <summary>
    /// Permet au joueur de récupérer son dash.
    /// </summary>
    private void RecoverDash()
    {
        _isEnable = false;
        playerState.canDash = true;
        _sprite.color = Color.black;
        _objectTime = _objectTotalTime;
    }

    /// <summary>
    /// Fonction exécuté un certain nombre de fois par seconde.
    /// Permet le comportement de l'orbe de dash.
    /// </summary>
    void FixedUpdate()
    {
        if(CheckCollision() && _isEnable)
            RecoverDash();

        if(!_isEnable)
            _objectTime -= Time.deltaTime;

        if(!_isEnable && _objectTime <= 0)
            resetOrb();
    }
}