using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingState : State
{
    private GameObject target;
    private Soldier targetSoldier;

    public FightingState(Entity entity) : base(entity)
    {

    }

    public override void OnStateEnter()
    {
        target = entity.GetComponent<Soldier>().Target;
        targetSoldier = target.GetComponent<Soldier>();
    }

    public override void OnStateUpdate()
    {
        FaceOpponent(target);
        Attack(targetSoldier);
    }

    public override void OnStateExit()
    {

    }

    private void FaceOpponent(GameObject opponent)
    {
        entity.EntityAgent.destination = opponent.transform.position - entity.transform.forward * 2;
    }

    private void Attack(Soldier target)
    {
        entity.SoldierStats.Attack(target);
    }
}
