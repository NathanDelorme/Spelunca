using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Cette classe définie quand le joueur doit mourrir ou gagner.
/// Par "gagner" on entend finir le niveau courant.
/// Par "mourrir" on entend lorsque le joueur entre en collision avec des piques ou le vide.
/// </summary>
public class WinDeathCondition : MonoBehaviour
{
    /// <value>
    /// GameObject du menu de fin lorsque le joueur fini le niveau.
    /// </value>
    public GameObject endMenu;
    /// <value>
    /// Cette propriété (<see cref="PlayerState"/>) est un ScriptableObject.
    /// Cet attribut stocke toutes les variables utiles pour savoir ce que le joueur veut faire,
    /// ce qu'il peut faire, ainsi que ce qu'il est en train de faire.
    /// </value>
    public PlayerState playerState;
    /// <value>
    /// Référence vers l'<see cref="AbilitySystem"/> du joueur.
    /// </value>
    private AbilitySystem abilitySystem;
    /// <value>
    /// Point de réapparition du joueur.
    /// </value>
    public GameObject spawnPoint;
    ///  <value>
    ///  RigidBody2D qui permet d'ajouter de la physique à un GameObject.
    ///  </value>
    private Rigidbody2D _rigidBody;
    /// <value>
    /// Zone de collision du joueur.
    /// </value>
    public BoxCollider2D _playerCollider;
    /// <value>
    /// Booléen qui défini si les piques et le sol sont inversé par le pouvoir Spike du joueur.
    /// </value>
    public bool reverseSpikeZone = false;
    /// <value>
    /// Liste des plateformes mouvantes présentent dans le niveau courant.
    /// </value>
    public Movement[] movementPlatforms;
    /// <value>
    /// Liste des plateformes tombantes présentent dans le niveau courant.
    /// </value>
    public FallingPlateform[] fallingPlatforms;
    /// <value>
    /// Manager des orbes de dash.
    /// </value>
    public OrbsDashManager orbsDashManager;
    /// <value>
    /// Booléen qui stocke si c'est le premier spawn du joueur dans le niveau ou non.
    /// </value>
    private bool firstLoad = true;
    /// <value>
    /// Animator qui permet de définir des variables dans l'animator pour savoir l'animation que le personnage doit executer.
    /// </value>
    private Animator animator;
    ///  <value>
    ///  SpriteRenderer qui permet de changer l'apparence du joueur.
    ///  </value>
    private SpriteRenderer _sprite;
    ///  <value>
    ///  Booléen qui contient la valeur de si le joueur meurt ou non.
    ///  </value>
    public bool isKilled = false;

    /// <summary>
    /// Fonction exécuté avant la première frame du programme, donc avant le premier appel à Update.
    /// </summary>
    void Start()
    {
        SaveSceneName();
        animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        orbsDashManager = FindObjectOfType<OrbsDashManager>();
        movementPlatforms = FindObjectsOfType<Movement>();
        fallingPlatforms = FindObjectsOfType<FallingPlateform>();
        abilitySystem = FindObjectOfType<AbilitySystem>();
        _rigidBody = GetComponentInParent<Rigidbody2D>();
        SpawnPlayer();
    }

    /// <summary>
    /// Fonction qui téléporte le joueur au spawn du niveau.
    /// </summary>
    private void SpawnPlayer()
    {
        playerState.isDashing = false;
        playerState.isJumping = false;
        playerState.isWallJumping = false;
        playerState.isWallSliding = false;
        playerState.isMoving = false;

        _rigidBody.bodyType = RigidbodyType2D.Dynamic;
        if (!firstLoad)
        {
            orbsDashManager.resetOrbs();
            foreach (Movement movementScript in movementPlatforms)
                movementScript.Respawn();
            foreach (FallingPlateform fallingPlatformScript in fallingPlatforms)
                fallingPlatformScript.InstantRespawn();
        }

        abilitySystem.SetState(new NoneState(abilitySystem));
        isKilled = false;
        _sprite.flipX = false;
        _rigidBody.transform.position = spawnPoint.transform.position;
        _rigidBody.velocity = new Vector2(0f, 0f);
        _rigidBody.gravityScale = 7f;
        firstLoad = false;
        Timer timer = FindObjectOfType<Timer>();
        timer.SaveTime(true);
    }

    /// <summary>
    /// Fonction qui tue le joueur.
    /// </summary>
    private void KillPlayer()
    {
        isKilled = true;
        _rigidBody.bodyType = RigidbodyType2D.Static;
        animator.Play("Death");
        Invoke("SpawnPlayer", 0.45f);
    }

    /// <summary>
    /// Fonction qui traite les collision entre le joueur et les autres objets qui composent la scène.
    /// </summary>
    /// <param name="collision">Collision générée par le joueur et un objet.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (reverseSpikeZone)
        {
            if (collision.collider.CompareTag("Ground"))
                KillPlayer();
        }
    }

    /// <summary>
    /// Fonction qui traite les collision entre le joueur et les autres objets qui composent la scène.
    /// </summary>
    /// <param name="collision">Objet qui est entré en collision avec le joueur.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
            NextLevel();

        else if (collision.CompareTag("KillZone"))
            KillPlayer();

        else if (!reverseSpikeZone)
        {
            if (collision.CompareTag("SpikeZone") && _playerCollider.IsTouchingLayers(7))
                KillPlayer();
        }
    }

    /// <summary>
    /// Charge le niveau suivant.
    /// </summary>
    private void NextLevel()
    {
        Timer timer = FindObjectOfType<Timer>();
        timer.SaveTime();

        int id = int.Parse(SceneManager.GetActiveScene().name.Remove(0, 5)) + 1;

        if (id <= 20)
        {
            PlayerPrefs.SetInt(Application.version + "Level" + id.ToString(), 1);
            PlayerPrefs.Save();
        }
        endMenu.SetActive(true);
    }

    /// <summary>
    /// Sauvegarde la dernière scène dans laquelle le joueur est entré.
    /// </summary>
    private void SaveSceneName()
    {
        PlayerPrefs.SetString(Application.version + "player_lastScene", SceneManager.GetActiveScene().name);
    }
}
