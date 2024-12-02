using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : State
{
    public Vector3 Destination;
    private BehaviourStateMachine stateMachine;

    public WalkingState(Soldier soldier, Vector3 destination) : base(soldier)
    {
        Destination = destination;
    }

    public override void OnStateEnter()
    {
        stateMachine = soldier.GetComponent<BehaviourStateMachine>();
    }

    public override void OnStateUpdate()
    {
        Destination = stateMachine.Destination;

        if (Destination == null) return;
        soldier.EntityAgent.destination = Destination;

        //if (entity.transform.position.magnitude - destination.magnitude < 0.1f) entity.GetComponent<BehaviourStateMachine>().SetAction(ActionType.Idle, Vector3.zero);
    }

    public override void OnStateExit()
    {

    }
}
