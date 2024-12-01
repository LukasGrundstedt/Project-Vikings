using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffHand : MonoBehaviour
{
    [SerializeField] private GameObject offHandObj;

    [field: SerializeField]
    public Weapon HeldWeapon { get; set; }

    public bool OffHandFull {  get; set; }

    public GameObject OffHandObj { get => offHandObj; set => offHandObj = value; }
}