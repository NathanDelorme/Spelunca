using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalState : State
{
    public CristalState(AbilitySystem newSystem) : base(newSystem) { }

    public override void Update()
    {

    }

    public override void FixedUpdate()
    {
        if (system.cristalTilemap.color != system.cristalUnable)
        {
            Color color = Color.Lerp(system.cristalTilemap.color, system.cristalUnable, 0.2f);
            system.cristalTilemap.color = color;
        }
    }

    public override void EnterState()
    {
        system.playerState.AbilityType = PlayerState.Ability.CRYSTAL;
        system.cristalTmCollider.isTrigger = false;
        system.goGround_Cristal.layer = 6;
    }

    public override void ExitState()
    {
        system.cristalTmCollider.isTrigger = true;
        system.goGround_Cristal.layer = 0;
    }
}