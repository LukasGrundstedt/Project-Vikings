using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingState : State
{
    private Soldier targetSoldier;

    public FightingState(Soldier soldier) : base(soldier)
    {

    }

    public override void OnStateEnter()
    {
        targetSoldier = soldier.Target.gameObject.GetComponent<Soldier>();
    }

    public override void OnStateUpdate()
    {
        FaceOpponent(targetSoldier.gameObject);

        if (soldier.transform.position.CompareDistance(targetSoldier.gameObject.transform.position) > soldier.AttackRange) return;
        if (soldier.AttackCooldown > 0f) return;
        soldier.Attack();
    }

    public override void OnStateExit()
    {

    }

    private void FaceOpponent(GameObject opponent)
    {
        soldier.EntityAgent.destination = opponent.transform.position - soldier.transform.forward * 2;
    }
}