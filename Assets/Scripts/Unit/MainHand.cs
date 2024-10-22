using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHand : MonoBehaviour
{
    [SerializeField] private GameObject mainHandObj;

    public bool MainHandFull {  get; set; }
    public GameObject MainHandObj { get => mainHandObj; set => mainHandObj = value; }
}
