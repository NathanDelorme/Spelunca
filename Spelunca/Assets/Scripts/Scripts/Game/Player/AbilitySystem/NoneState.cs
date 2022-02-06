using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneState : State
{
    public NoneState(AbilitySystem newSystem) : base(newSystem) { }

    public override void Update()
    {

    }

    public override void FixedUpdate()
    {

    }

    public override void EnterState()
    {
        system.playerState.AbilityType = PlayerState.Ability.NONE;
    }

    public override void ExitState()
    {

    }
}
