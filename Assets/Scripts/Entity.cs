using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Entity : MonoBehaviour, ISelectable
{
    public bool Highlighted { get; set; }
    public bool Selected { get; set; }

    [field: SerializeField]
    public Color OutlineColor { get; set; }

    [field: SerializeField]
    public GameObject SelectVisual { get; set; }

    [SerializeField] protected Stats stats;

    protected EntityStatDisplay EntityDisplay { get; set; }

    protected void Start()
    {
        SetDisplay(EntityStatDisplay.Instance);
    }

    private void SetDisplay(EntityStatDisplay display)
    {
        EntityDisplay = display;
    }

    public void Highlight(bool highlighted)
    {
        Highlighted = highlighted;
        SelectVisual.SetActive(highlighted);
        SelectVisual.GetComponent<MeshRenderer>().material.color = OutlineColor;
    }

    public void VisualizeSelection(bool isSelected)
    {
        Selected = isSelected;
        SelectVisual.SetActive(isSelected);
        SelectVisual.GetComponent<MeshRenderer>().material.color = OutlineColor;
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

    private void OnEnable()
    {
        EntityStatDisplay.OnCreation += SetDisplay;
    }

    private void OnDisable()
    {
        EntityStatDisplay.OnCreation -= SetDisplay;
    }
}