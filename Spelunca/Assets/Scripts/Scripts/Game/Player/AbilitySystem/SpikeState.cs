using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeState : State
{
    public SpikeState(AbilitySystem newSystem) : base(newSystem) { }

    public override void Update()
    {

    }

    public override void FixedUpdate()
    {

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
