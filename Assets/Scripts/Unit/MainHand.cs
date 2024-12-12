using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHand : MonoBehaviour
{
    [SerializeField] private GameObject mainHandObj;

    public Weapon HeldWeapon { get; set; }

    public bool MainHandFull {  get; set; }

    /// <summary>
    /// The actual GameObject acting as the Hand, not what is held by the Hand
    /// </summary>
    [Tooltip("The actual GameObject acting as the Hand, not what is held by the Hand")]
    public GameObject MainHandObj { get => mainHandObj; set => mainHandObj = value; }

    private void Awake()
    {
        HeldWeapon = GetComponentInChildren<Weapon>();
    }
}
