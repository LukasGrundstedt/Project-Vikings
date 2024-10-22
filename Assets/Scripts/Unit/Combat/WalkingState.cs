using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : State
{
    public Vector3 Destination;
    private BehaviourStateMachine stateMachine;

    public WalkingState(Entity entity, Vector3 destination) : base(entity)
    {
        Destination = destination;
    }

    public override void OnStateEnter()
    {
        stateMachine = entity.GetComponent<BehaviourStateMachine>();
    }

    public override void OnStateUpdate()
    {
        Destination = stateMachine.Destination;

        if (Destination == null) return;
        entity.EntityAgent.destination = Destination;

        //if (entity.transform.position.magnitude - destination.magnitude < 0.1f) entity.GetComponent<BehaviourStateMachine>().SetAction(ActionType.Idle, Vector3.zero);
    }

    public override void OnStateExit()
    {

    }
}
