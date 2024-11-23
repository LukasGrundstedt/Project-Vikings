using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private GameObject target;

    public void SetActive()
    {
        target.SetActive(!target.activeInHierarchy);
    }
}