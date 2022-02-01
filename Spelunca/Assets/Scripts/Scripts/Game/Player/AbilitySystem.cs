using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    public State currentState;
    public PlayerState playerState;
    public CompositeCollider2D spikesCollider;
    public WinDeathCondition winDeathCondition;
    public NavigationController navigationController;
    public LayerMask ground;
    public LayerMask spikes;

    private void Start()
    {
        winDeathCondition = FindObjectOfType<WinDeathCondition>();
        SetState(new NoneState(this));
    }

    private void Update()
    {
        currentState.Update();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    public void SetState(State newState)
    {
        if (currentState != null)
            currentState.ExitState();

        currentState = newState;

        if (currentState != null)
            currentState.EnterState();
    }

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
    }
}
