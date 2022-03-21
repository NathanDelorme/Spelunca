using System;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Classe qui définie le comportement du layer des jump-pads
/// </summary>
public class Jump_Pad : MonoBehaviour
{
    /// <value>
    /// Layer qui représent la zone de collisions des jump-pads dans le niveau.
    /// </value>
    public LayerMask jumpPadZone;
    /// <value>
    /// Tilemap qui comporte les jump-pad.
    /// Cela permet de passer à travers ces tuiles afin de connaître leurs noms et récupérer le sens de poussé du jump-pad.
    /// </value>
    public Tilemap tilemap;

    ///  <value>
    ///  RigidBody qui permet de donner une physique à un GameObject
    ///  </value>
    private Rigidbody2D _rigidBody;
    /// <value>
    /// Zone de collision du joueur.
    /// </value>
    private BoxCollider2D _playerCollider;
    /// <value>
    /// Force appliqué sur le joueur lorsqu'il touche un jump-pad.
    /// </value>
    private float _bounceForce = 30f;

    /// <summary>
    /// Function executed at the start of the program.
    /// Used to get components (<c>_rigidBody</c>, <c>_playerCollider</c>) from the parent of the current <c>GameObject</c>.
    /// </summary>
    void Start()
    {
        _rigidBody = GetComponentInParent<Rigidbody2D>();
        _playerCollider = GetComponentInParent<BoxCollider2D>();
    }

    /// <summary>
    /// Fonction exécuté avant la première frame du programme, donc avant le premier appel à Update.
    /// Cette fonction agit comme un constructeur permettant d'initialiser les attributs et effectuer des actions au chargement du script.
    /// Si le joueur touche un jump-pad, on lui applique une force dans une direction.
    /// </summary>
    private void FixedUpdate()
    {
        if (CheckIsInJumpPad())
            JumpPad(GetTileDirection());
    }

    /// <summary>
    /// Vérifie si le joueur touche un jump-pad ou non.
    /// </summary>
    /// <returns>
    /// Vrai si le joueur touche un jump-pad, sinon faux.
    /// </returns>
    private bool CheckIsInJumpPad()
    {
        return _playerCollider.IsTouchingLayers(jumpPadZone.value);
    }

    /// <summary>
    /// Récupère la direction du jump-pad.
    /// </summary>
    /// <return>
    /// Renvoi un entier :
    ///     0 : Rebondi vers le haut.
    ///     1 : Rebondi vers la gauche.
    ///     2 : Rebondi vers le le bas.
    ///     3 : Rebondi vers la droite.
    /// </return>
    private int GetTileDirection()
    {
        for (int x = (int)_rigidBody.position.x - 1; x <= (int)_rigidBody.position.x + 1; x++)
        {
            for (int y = (int)_rigidBody.position.y - 1; y <= (int)_rigidBody.position.y + 1; y++)
            {
                Vector3Int tilePos = tilemap.WorldToCell(new Vector3(x, y, 0f));
                Tile tile = (Tile)tilemap.GetTile(tilePos);

                if (tile != null)
                {
                    string tileName = tile.name;
                    return Int32.Parse(tileName.Substring(tileName.Length - 1));
                }
            }
        }
        return -1;
    }

    /// <summary>
    /// Fonction qui applique le rebond du joueur dans une direction
    ///     0 : Rebondi vers le haut.
    ///     1 : Rebondi vers la gauche.
    ///     2 : Rebondi vers le le bas.
    ///     3 : Rebondi vers la droite.
    /// </summary>
    /// <param name="padDirection">
    /// Direction vers laquelle le joueur sera propulsé.
    /// </param>
    /// <argument>
    private void JumpPad(int padDirection)
    {
        switch (padDirection)
        {
            case 1:
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0f);
                _rigidBody.AddForce(Vector2.up * _bounceForce, ForceMode2D.Impulse);
                break;
            case 2:
                _rigidBody.velocity = new Vector2(0f, _rigidBody.velocity.y);
                _rigidBody.AddForce(Vector2.left * _bounceForce, ForceMode2D.Impulse);
                break;
            case 3:
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0f);
                _rigidBody.AddForce(Vector2.down * _bounceForce, ForceMode2D.Impulse);
                break;
            case 4:
                _rigidBody.velocity = new Vector2(0f, _rigidBody.velocity.y);
                _rigidBody.AddForce(Vector2.right * _bounceForce, ForceMode2D.Impulse);
                break;
            default:
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0f);
                _rigidBody.AddForce(Vector2.up * _bounceForce, ForceMode2D.Impulse);
                break;
        }

    }
}