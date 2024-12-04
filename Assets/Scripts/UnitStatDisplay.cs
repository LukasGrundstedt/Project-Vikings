using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatDisplay : EntityStatDisplay
{
    [Tooltip("Instance of the Units' UI Display")]
    [SerializeField] private GameObject unitPortraitFrame;

    [Tooltip("Portrait Selection Circle")]
    [SerializeField] private GameObject selectionHighlight;

    // required to hide base awake
    new protected void Awake()
    {
        
    }

    private void Update()
    {
        if (selectionHighlight == null) return;
        selectionHighlight.SetActive(displayedEntity.Selected);
    }

    private void Start()
    {
        InitializeUI();

        displayedEntity = GetComponent<Entity>();
        displayedEntity.OnDamageTaken += UpdateHealthbar;
        DisplayStats(displayedEntity.DisplayableHp(), displayedEntity.DisplayableStats());
    }

    private void InitializeUI()
    {
        Transform portraitParent = GameObject.Find("Portrait Parent").transform;
        unitPortraitFrame.transform.SetParent(portraitParent, false);
    }

    /// <summary>
    /// Used by Button 'Unit Portrait Background'
    /// </summary>
    public void OnClick()
    {
        GameObject unit = displayedEntity.gameObject;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            UnitManager.Instance.ShiftClickSelect(unit);
        }
        else
        {
            UnitManager.Instance.ClickSelect(unit);
        }
    }
}