using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffHand : MonoBehaviour
{
    [SerializeField] private GameObject offHandObj;

    [field: SerializeField]
    public Weapon HeldWeapon { get; set; }

    public bool OffHandFull {  get; set; }

    /// <summary>
    /// The actual GameObject acting as the Hand, not what is held by the Hand
    /// </summary>
    [Tooltip("The actual GameObject acting as the Hand, not what is held by the Hand")]
    public GameObject OffHandObj { get => offHandObj; set => offHandObj = value; }

    public void Awake()
    {
        HeldWeapon = GetComponentInChildren<Weapon>();
    }
}