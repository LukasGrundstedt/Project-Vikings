using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AggroTrigger : MonoBehaviour
{
    [SerializeField] private Collider aggroRangeTrigger;

    public SoldierFaction SoldierFaction {  get; set; }

    public Action<GameObject> OnTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Soldier>().FactionID != SoldierFaction)
        {
            OnTriggered?.Invoke(other.gameObject);
        }
    }
}