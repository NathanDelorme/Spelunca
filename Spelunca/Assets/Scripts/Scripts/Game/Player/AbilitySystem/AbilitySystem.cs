using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Permet de gérer l'habilité que le joueur possède.
/// Il y a trois pouvoirs différent.
/// Le None qui signifie que le joueur n'a pas de pouvoir.
/// Le Cristal qui fait apparaître des plateformes en cristal.
/// Le Spike qui permet au joueur de marcher sur les piques mais en contre-partie, il ne peut plus marcher sur le sol.
/// 
/// On applique le Design pattern d'état pour gérer les pouvoirs du joueur.
/// </summary>
public class AbilitySystem : MonoBehaviour
{
    /// <value>
    /// Stocke son pouvoir courant.
    /// </value>
    public State currentState;
    /// <value>
    /// Stocke son ancien pouvoir.
    /// </value>
    public State previousState;
    /// <value>
    /// Cette propriété (<see cref="PlayerState"/>) est un ScriptableObject.
    /// Cet attribut stocke toutes les variables utiles pour savoir ce que le joueur veut faire,
    /// ce qu'il peut faire, ainsi que ce qu'il est en train de faire.
    /// </value>
    public PlayerState playerState;
    /// <value>
    /// Zone de collision des piques présents dans le niveau.
    /// </value>
    public CompositeCollider2D spikesCollider;
    /// <value>
    /// Script qui gère la condition de mort et de victoire du joueur.
    /// </value>
    public WinDeathCondition winDeathCondition;
    /// <value>
    /// Cet objet est utile pour savoir ce que le joueur peut faire.
    /// </value>
    public NavigationController navigationController;
    /// <value>
    /// Stocke les layers qui sont liés au sol.
    /// </value>
    public LayerMask ground;
    /// <value>
    /// Stocke les layers qui sont liés aux piques.
    /// </value>
    public LayerMask spikes;
    /// <value>
    /// Stocke les tilemaps sur lesquelles le joueur peut se déplacer.
    /// </value>
    public Tilemap[] tilemaps;
    /// <value>
    /// Stocke la tilemap de cristal pour la faire apparaître / disparaître.
    /// </value>
    public Tilemap cristalTilemap;
    /// <value>
    /// Zone de collision des plateformes de cristal présentent dans le niveau.
    /// </value>
    public CompositeCollider2D cristalTmCollider;
    /// <value>
    /// GameObject de la plateforme de cristal.
    /// </value>
    public GameObject goGround_Cristal;
    /// <value>
    /// Teinte de couleur lorsque le joueur possède le pouvoir Spike.
    /// </value>
    public Color32 killGroundColor = new Color32(255, 0, 0, 255);
    /// <value>
    /// Teinte de couleur des cristaux lorsqu'il sont activés.
    /// </value>
    public Color32 cristalUnable = new Color32(255, 0, 0, 255);
    /// <value>
    /// Teinte de couleur des cristaux lorsqu'il sont désactivés.
    /// </value>
    public Color32 cristalDisable = new Color32(255, 0, 0, 50);

    /// <summary>
    /// Fonction exécuté avant la première frame du programme, donc avant le premier appel à Update.
    /// Cette fonction agit comme un constructeur permettant d'initialiser les attributs et effectuer des actions au chargement du script.
    /// On y initialise le pouvoir par défaut du joueur.
    /// </summary>
    private void Start()
    {
        winDeathCondition = FindObjectOfType<WinDeathCondition>();
        SetState(new NoneState(this));
    }

    /// <summary>
    /// Fonction exécuté à chaque frame.
    /// On execute la fonction Update() de l'état courant du joueur.
    /// </summary>
    private void Update()
    {
        if(currentState != null) 
            currentState.Update();
    }

    /// <summary>
    /// Fonction exécuté un nombre déterminer de fois par secondes.
    /// On execute la fonction FixedUpdate() de l'état courant du joueur.
    /// On effectue aussi les transition de couleur pour les éléments affecté par les pouvoirs du joueur.
    /// </summary>
    private void FixedUpdate()
    {
        currentState.FixedUpdate();

        if (!(currentState is SpikeState) && tilemaps[0].color != new Color32(255, 255, 255, 255))
        {
            Color color = Color.Lerp(tilemaps[0].color, new Color32(255, 255, 255, 255), 0.2f);
            foreach (Tilemap t in tilemaps)
            {
                t.color = color;
            }
        }
        else if (!(currentState is CristalState) && cristalTilemap.color != cristalDisable)
        {
            Color color = Color.Lerp(cristalTilemap.color, cristalDisable, 0.2f);
            cristalTilemap.color = color;
        }
    }

    /// <summary>
    /// Change l'état du joueur.
    /// </summary>
    /// <param name="newState">Nouvel état du joueur</param>
    public void SetState(State newState)
    {
        if (currentState != null)
            currentState.ExitState();

        previousState = currentState;
        currentState = newState;

        if (currentState != null)
            currentState.EnterState();
    }

    /// <summary>
    /// Fonction appelé lorsque le joueur entre en collision avec un objet.
    /// </summary>
    /// <param name="collision">Objet qui est entré en collision avec le joueur.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ResetOrb"))
        {
            if(!(currentState is NoneState))
                SetState(new NoneState(this));
        }
        else if (collision.CompareTag("SpikeOrb"))
        {
            if (!(currentState is SpikeState))
                SetState(new SpikeState(this));
        }
        else if (collision.CompareTag("CristalOrb"))
        {
            if (!(currentState is CristalState))
                SetState(new CristalState(this));
        }
    }
}
