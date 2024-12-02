using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityStatDisplay : MonoBehaviour
{
    public static EntityStatDisplay Instance;
    public static Action<EntityStatDisplay> OnCreation;

    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private GameObject displayParent;

    private Entity displayedEntity;

    private Entity currentSelection;

    private void Awake()
    {
        Instance = this;
        OnCreation?.Invoke(Instance);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (MouseRaycast.CurrentHitType == HitType.Object || MouseRaycast.CurrentHitType == HitType.Enemy)
        //    {
        //        RaycastHit hitInfo = MouseRaycast.HitInfo;

        //        currentSelection = hitInfo.collider.GetComponent<Entity>();
        //        DisplayEntity(currentSelection);
        //    }
        //    else if (MouseRaycast.CurrentHitType == HitType.Ground || MouseRaycast.CurrentHitType == HitType.None)
        //    {
        //        currentSelection = null;
        //    }
        //}
    }

    public void DisplayEntity(Entity entity)
    {
        //Clear previous selection
        if (displayedEntity) displayedEntity.VisualizeSelection(false);

        displayedEntity = entity;
        label.text = entity.name;
        displayParent.SetActive(true);
    }

    /// <summary>
    /// Called by 'Close Button'
    /// </summary>
    public void ClearSelection()
    {
        displayParent.SetActive(false);
        displayedEntity.VisualizeSelection(false);
    }
}