using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Entity : MonoBehaviour, ISelectable
{
    [field: SerializeField]
    public bool Selected { get; set; }
    public Action OnSelection;

    [field: SerializeField]
    public GameObject SelectVisual { get; set; }

    [field: SerializeField]
    protected EntityStatDisplay Display { get; set; }


    [field: SerializeField]
    public NavMeshAgent EntityAgent { get; set; }

    [field: SerializeField]
    public Soldier SoldierStats { get; set; }

    protected void Start()
    {
        SetDisplay(EntityStatDisplay.Instance);
    }

    private void SetDisplay(EntityStatDisplay display)
    {
        this.Display = display;
        Debug.Log("Set Display");
    }

    private void OnEnable()
    {
        EntityStatDisplay.OnCreation += SetDisplay;
        Debug.Log(gameObject.name);
    }

    private void OnDisable()
    {
        EntityStatDisplay.OnCreation -= SetDisplay;
    }
}