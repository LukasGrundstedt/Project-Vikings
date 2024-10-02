using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    [SerializeField] private GameObject rightHandObj;

    public GameObject RightHandObj { get => rightHandObj; set => rightHandObj = value; }
}
