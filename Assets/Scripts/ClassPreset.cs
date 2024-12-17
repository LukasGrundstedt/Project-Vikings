using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewClass", menuName = "Class Preset")]
public class ClassPreset : ScriptableObject
{
    public Sprite Portrait;

    public int MaxHp;
    public int Hp;
    public int Atk;
    public int Dmg;
    public int Def;
    public int Armor;
    public float AttackSpeed;
    public float AttackRange;
    public float AggroRange;

    public GameObject MainHand;
    public GameObject OffHand;
}