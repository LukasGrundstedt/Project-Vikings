using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingState : State
{
    private Soldier targetSoldier;
    private Shield soldierShield;

    private Vector3 offHandPos;
    private Vector3 offHandRot;

    public FightingState(Soldier soldier) : base(soldier)
    {

    }

    public override void OnStateEnter()
    {
        targetSoldier = soldier.Target;
        soldierShield = soldier.OffHand.HeldWeapon.GetComponent<Shield>();

        offHandPos = soldier.OffHand.transform.localPosition;
        offHandRot = soldier.OffHand.transform.localRotation.eulerAngles;
    }

    public override void OnStateUpdate()
    {
        FaceOpponent(targetSoldier.gameObject);

        soldierShield.ShieldOpponent(targetSoldier.gameObject);

        if (soldier.transform.position.CompareDistance(targetSoldier.gameObject.transform.position) > soldier.AttackRange) return;
        if (soldier.AttackCooldown > 0f) return;
        soldier.Attack();
    }

    public override void OnStateExit()
    {
        soldier.OffHand.transform.localPosition = offHandPos;
        soldier.OffHand.transform.localRotation = Quaternion.Euler(offHandRot);
    }

    private void FaceOpponent(GameObject opponent)
    {
        soldier.EntityAgent.destination = opponent.transform.position - soldier.transform.forward * 2;
    }

    
}