

public abstract class State
{
    protected AbilitySystem system;

    public State(AbilitySystem newSystem)
    {
        system = newSystem;
    }

    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void EnterState() { }
    public virtual void ExitState() { }
}
