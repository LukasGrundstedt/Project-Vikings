using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingState : State
{
    private Soldier targetSoldier;

    public FightingState(Entity entity) : base(entity)
    {

    }

    public override void OnStateEnter()
    {
        targetSoldier = entity.SoldierStats.Target.gameObject.GetComponent<Soldier>();
    }

    public override void OnStateUpdate()
    {
        FaceOpponent(targetSoldier.gameObject);

        if (entity.transform.position.CompareDistance(targetSoldier.gameObject.transform.position) > entity.SoldierStats.AttackRange) return;
        if (entity.SoldierStats.AttackCooldown > 0f) return;
        entity.SoldierStats.Attack();
    }

    public override void OnStateExit()
    {

    }

    private void FaceOpponent(GameObject opponent)
    {
        entity.EntityAgent.destination = opponent.transform.position - entity.transform.forward * 2;
    }
}