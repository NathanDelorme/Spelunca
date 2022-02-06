using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpikeState : State
{
    public SpikeState(AbilitySystem newSystem) : base(newSystem) { }

    public override void Update()
    {
        
    }

    public override void FixedUpdate()
    {
        if (system.tilemaps[0].color != system.killGroundColor)
        {
            Color color = Color.Lerp(system.tilemaps[0].color, system.killGroundColor, 0.2f);

            foreach (Tilemap t in system.tilemaps)
                t.color = color;
        }
    }

    public override void EnterState()
    {
        system.playerState.AbilityType = PlayerState.Ability.SPIKE;
        system.spikesCollider.isTrigger = false;
        system.navigationController.groundLayers = system.spikes;
        system.winDeathCondition.reverseSpikeZone = true;
    }

    public override void ExitState()
    {
        system.spikesCollider.isTrigger = true;
        system.navigationController.groundLayers = system.ground;
        system.winDeathCondition.reverseSpikeZone = false;
    }
}
