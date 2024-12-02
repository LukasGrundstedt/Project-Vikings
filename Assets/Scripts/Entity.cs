using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Entity : MonoBehaviour, ISelectable
{
    [field: SerializeField]
    public bool Highlighted { get; set; }
    [field: SerializeField]
    public bool Selected { get; set; }
    public Action OnSelection;

    [field: SerializeField]
    public GameObject SelectVisual { get; set; }

    [field: SerializeField]
    protected EntityStatDisplay EntityDisplay { get; set; }


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
        this.EntityDisplay = display;
        Debug.Log("Set Display");
    }

    public void Highlight(bool highlighted)
    {
        Highlighted = highlighted;
        SelectVisual.SetActive(highlighted);
    }

    public void VisualizeSelection(bool isSelected)
    {
        Selected = isSelected;
        SelectVisual.SetActive(isSelected);
    }

    public void OnMouseEnter()
    {
        Highlight(true);
    }

    public void OnMouseExit()
    {
        Highlighted = false;

        if (!Selected)
        {
            Highlight(false);
        }
    }

    public virtual void OnMouseDown()
    {
        EntityDisplay.DisplayEntity(this);
        VisualizeSelection(true);
    }

    //public void Highlight(bool highlighted)
    //{
    //    SelectVisual.SetActive(highlighted);
    //}

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