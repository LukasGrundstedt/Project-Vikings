using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class BehaviourStateMachine : MonoBehaviour
{
    [SerializeField] private bool goFight = false;

    public State DefaultState { get; set; }
    public State CurrentState { get; set; }
    private Dictionary<State, Dictionary<Func<bool>, State>> states;

    private Entity entity;

    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponent<Entity>();

        State idleState = new IdleState(entity);
        State fightingState = new FightingState(entity);
        State attackState = new AttackState(entity);
        State blockState = new BlockState(entity);

        states = new()
        {
            {
                idleState, new()
                {
                    { () => goFight, fightingState }
                }
            },
            {
                fightingState, new()
                {
                    { () => !goFight, idleState }
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
}
