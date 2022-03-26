using UnityEngine;

/// <summary>
/// Classe permettant au joueur de rester sur une plateforme en mouvement.
/// </summary>
public class FollowPlatform : MonoBehaviour
{
    /// <summary>
    /// Fonction qui se déclenche lorsque la plateforme a une collision avec un autre objet.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
            collision.gameObject.transform.SetParent(transform);
    }

    /// <summary>
    /// Fonction qui se déclenche lorsque la plateforme sort d'une collision avec un autre objet.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
            collision.gameObject.transform.SetParent(null);
    }
}
