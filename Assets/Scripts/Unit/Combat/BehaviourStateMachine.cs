using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Soldier))]
public class BehaviourStateMachine : MonoBehaviour
{
    public State DefaultState { get; set; }
    public State CurrentState { get; set; }
    private Dictionary<State, Dictionary<Func<bool>, State>> states;

    private Soldier soldier;

    public Vector3 Destination { get; set; } = Vector3.zero;

    [SerializeField] private ActionType actionType;

    // Start is called before the first frame update
    void Start()
    {
        soldier = GetComponent<Soldier>();

        State idleState = new IdleState(soldier);
        State fightingState = new FightingState(soldier);
        State attackState = new AttackState(soldier);
        State blockState = new BlockState(soldier);
        State walkingState = new WalkingState(soldier, Vector3.zero);

        states = new()
        {
            {
                idleState, new()
                {
                    { () => actionType == ActionType.Attack, fightingState },
                    { () => actionType == ActionType.Move, walkingState }
                }
            },
            {
                walkingState, new()
                {
                    { () => actionType == ActionType.Attack, fightingState },
                    { () => actionType == ActionType.Idle, idleState },
                }
            },
            {
                fightingState, new()
                {
                    { () => actionType == ActionType.Move, walkingState }
                }
            },
        };

        CurrentState = idleState;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState.OnStateUpdate();

        foreach (var transition in states[CurrentState])
        {
            if (transition.Key())
            {
                ChangeState(transition.Value);
            }
        }
    }

    private void ChangeState(State nextState)
    {
        CurrentState.OnStateExit();
        CurrentState = nextState;
        CurrentState.OnStateEnter();
    }


    public void SetAction(ActionType actionType)
    {
        this.actionType = actionType;
    }
    public void SetAction(ActionType actionType, Vector3 destination)
    {
        this.actionType = actionType;
        Destination = destination;
    }
    public void SetAction(ActionType actionType, GameObject target)
    {
        this.actionType = actionType;
        GetComponent<Soldier>().Target = target.GetComponent<Soldier>();
    }
}