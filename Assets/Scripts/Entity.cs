using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Entity : MonoBehaviour, ISelectable
{
    [SerializeField] private Sprite portrait;
    public Sprite Portrait { get => portrait; set => portrait = value; }

    public bool Highlighted { get; set; }
    public bool Selected { get; set; }

    //Required for deriving classes, useless here though / Required for everything that has a healthBar
    public Action<float> OnDamageTaken;
    public Action OnDeath;

    [field: SerializeField]
    public Color OutlineColor { get; set; }

    [field: SerializeField]
    public GameObject SelectVisual { get; set; }

    protected EntityStatDisplay EntityDisplay { get; set; }

    protected void Start()
    {
        if (!EntityDisplay) SetDisplay(EntityStatDisplay.Instance);
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
        EntityDisplay.DisplayStats(DisplayableHp(), DisplayableStats());
        VisualizeSelection(true);
    }

    public virtual float DisplayableHp()
    {
        return 100.0f;
    }

    public virtual object[] DisplayableStats()
    {
        return new object[6]
        {
            0, 0, 0, 0, 0, 0
        };
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