using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitStatDisplay : EntityStatDisplay
{
    [SerializeField] private GameObject unitPortraitPrefab;
    [SerializeField] private GameObject unitPortraitFrame;

    [SerializeField] private GameObject selectionHighlight;

    private void Start()
    {
        InitializeUI();
    }

    private void Update()
    {
        if (selectionHighlight == null) return;

        selectionHighlight.SetActive(displayedEntity.Selected);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hpBarValue"></param>
    /// <param name="stats">atk, dmg, def, armor, attackSpeed, attackRange</param>
    public override void DisplayStats(float hpBarValue, params object[] stats)
    {
        uiHealthBar.value = hpBarValue;
        if (wsHealthBar) wsHealthBar.value = hpBarValue;

        for (int i = 0; i < stats.Length; i++)
        {
            texts[i].text = stats[i].ToString();
        }
    }

    private void InitializeUI()
    {
        Transform portraitParent = GameObject.Find("Portrait Parent").transform;

        if (unitPortraitFrame == null) unitPortraitFrame = Instantiate(unitPortraitPrefab);

        unitPortraitFrame.transform.SetParent(portraitParent, false);
    }

    /// <summary>
    /// Used by Button 'Unit Portrait Background
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