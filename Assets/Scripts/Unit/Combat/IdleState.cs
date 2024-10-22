using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(Entity entity) : base(entity)
    {

    }

    public override void OnStateEnter()
    {

    }

    public override void OnStateUpdate()
    {
        entity.EntityAgent.destination = entity.transform.position;
    }

    public override void OnStateExit()
    {

    }
}
