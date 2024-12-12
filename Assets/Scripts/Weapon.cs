using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    public Action OnImpact;

    [field: SerializeField]
    public Animator WeaponAnimator { get; set; }
}