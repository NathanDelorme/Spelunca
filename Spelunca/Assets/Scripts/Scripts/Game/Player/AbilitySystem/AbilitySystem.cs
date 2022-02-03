using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AbilitySystem : MonoBehaviour
{
    public State currentState;
    public State previousState;
    public PlayerState playerState;
    public CompositeCollider2D spikesCollider;
    public WinDeathCondition winDeathCondition;
    public NavigationController navigationController;
    public LayerMask ground;
    public LayerMask spikes;
    public Tilemap[] tilemaps;
    public Color32 killGroundColor = new Color32(255, 0, 0, 255);

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

        if (!(currentState is SpikeState) && tilemaps[0].color != new Color32(255, 255, 255, 255))
        {
            Color color = Color.Lerp(tilemaps[0].color, new Color32(255, 255, 255, 255), 0.2f);
            foreach (Tilemap t in tilemaps)
            {
                t.color = color;
            }
        }
    }

    public void SetState(State newState)
    {
        if (currentState != null)
            currentState.ExitState();

        previousState = currentState;
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
