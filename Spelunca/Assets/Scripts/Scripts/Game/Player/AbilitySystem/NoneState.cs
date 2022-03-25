/// <summary>
/// Etat sans pouvoir.
/// Hérite de la classe <see cref="State"/>.
/// </summary>
public class NoneState : State
{
    /// <summary>
    /// Constructeur de la classe <see cref="NoneState"/>.
    /// </summary>
    /// <param name="newSystem">Référence à l'<see cref="AbilitySystem"/> du joueur.</param>
    public NoneState(AbilitySystem newSystem) : base(newSystem) { }

    /// <summary>
    /// Fonction exécuté à chaque frame.
    /// </summary>
    public override void Update() { }

    /// <summary>
    /// Fonction exécuté un nombre déterminer de fois par secondes.
    /// </summary>
    public override void FixedUpdate() { }

    /// <summary>
    /// Fonction executé lorsque l'on change d'un état à celui-ci.
    /// </summary>
    public override void EnterState()
    {
        system.playerState.AbilityType = PlayerState.Ability.NONE;
    }

    /// <summary>
    /// Fonction executé lorsque l'on remplace cet état par un autre.
    /// </summary>
    public override void ExitState() { }
}
