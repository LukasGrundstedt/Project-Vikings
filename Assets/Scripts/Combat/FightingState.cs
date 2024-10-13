using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingState : State
{
    private GameObject target;

    public FightingState(Entity entity) : base(entity)
    {

    }

    public override void OnStateEnter()
    {
        target = entity.GetComponent<Soldier>().Target;
    }

    public override void OnStateUpdate()
    {
        FaceOpponent();
    }

    public override void OnStateExit()
    {

    }

    private void FaceOpponent()
    {
        entity.EntityAgent.destination = target.transform.position - entity.transform.forward * 2;
    }
}
