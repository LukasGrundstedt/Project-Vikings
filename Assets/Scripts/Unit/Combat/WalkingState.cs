using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        if (!soldier.EntityAgent.SetDestination(Destination))
        {
            Debug.Log($"Destination was not set successfully: Destination: {Destination}, Agent Destination: {soldier.EntityAgent.destination}");
        }

        if (soldier.transform.position.CompareDistance(Destination) < 0.1f)
        {
            stateMachine.SetAction(ActionType.Idle, Vector3.zero);
        } 
    }

    public override void OnStateExit()
    {

    }

    
}
