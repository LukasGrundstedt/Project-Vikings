using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHand : MonoBehaviour
{
    [SerializeField] private GameObject mainHandObj;

    public GameObject MainHandObj { get => mainHandObj; set => mainHandObj = value; }
}
