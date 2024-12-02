using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(Soldier soldier) : base(soldier)
    {

    }

    public override void OnStateEnter()
    {

    }

    public override void OnStateUpdate()
    {
        soldier.EntityAgent.destination = soldier.transform.position;
    }

    public override void OnStateExit()
    {

    }
}
