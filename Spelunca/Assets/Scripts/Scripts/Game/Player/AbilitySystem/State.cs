/// <summary>
/// Classe permettant de définir les méthodes et attribut d'un état du joueur pour l'<see cref="AbilitySystem"/>.
/// </summary>
public abstract class State
{
    /// <value>
    /// Référence vers l'<see cref="AbilitySystem"/> du joueur.
    /// </value>
    protected AbilitySystem system;

    /// <summary>
    /// Constructeur de la classe <see cref="State"/>.
    /// </summary>
    /// <param name="newSystem">Référence à l'<see cref="AbilitySystem"/> du joueur.</param>
    public State(AbilitySystem newSystem)
    {
        system = newSystem;
    }

    /// <summary>
    /// Fonction exécuté à chaque frame.
    /// </summary>
    public virtual void Update() { }

    /// <summary>
    /// Fonction exécuté un nombre déterminer de fois par secondes.
    /// </summary>
    public virtual void FixedUpdate() { }

    /// <summary>
    /// Fonction executé lorsque l'on change d'un état à celui-ci.
    /// </summary>
    public virtual void EnterState() { }

    /// <summary>
    /// Fonction executé lorsque l'on remplace cet état par un autre.
    /// </summary>
    public virtual void ExitState() { }
}
