using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    [SerializeField] private GameObject leftHandObj;

    public GameObject LeftHandObj { get => leftHandObj; set => leftHandObj = value; }
}
