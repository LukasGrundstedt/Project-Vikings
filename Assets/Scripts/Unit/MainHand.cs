using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHand : MonoBehaviour
{
    [SerializeField] private GameObject mainHandObj;

    [field: SerializeField]
    public Weapon HeldWeapon { get; set; }

    public bool MainHandFull {  get; set; }
    public GameObject MainHandObj { get => mainHandObj; set => mainHandObj = value; }
}
