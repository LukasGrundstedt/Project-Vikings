using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeWeapon : Weapon
{
    [SerializeField] private int weaponDmg;
    [SerializeField] private float weaponAtkSpeed;
    [SerializeField] private float weaponRange;

    public void IncreaseStats(ref int dmg, ref float atkSpeed, ref float atkRange)
    {
        dmg += weaponDmg;
        atkSpeed += weaponAtkSpeed;
        atkRange += weaponRange;
    }
}