using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityStatDisplay : MonoBehaviour
{
    public static EntityStatDisplay Instance;
    public static Action<EntityStatDisplay> OnCreation;

    [SerializeField] private GameObject displayParent;

    [SerializeField] protected Slider uiHealthBar;

    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] protected Image portrait;
    [SerializeField] protected TextMeshProUGUI[] texts;

    protected Entity displayedEntity;

    protected void Awake()
    {
        Instance = this;
        OnCreation?.Invoke(Instance);
    }

    public virtual void DisplayEntity(Entity entity)
    {
        //Clear previous selection
        if (displayedEntity) 
        { 
            displayedEntity.VisualizeSelection(false);
            displayedEntity.OnDamageTaken -= UpdateHealthbar;
        }

        displayedEntity = entity;
        displayedEntity.OnDamageTaken += UpdateHealthbar;
        label.text = entity.name;
        portrait.sprite = entity.Portrait;
        displayParent.SetActive(true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hpBarValue"></param>
    /// <param name="stats">atk, dmg, def, armor, attackSpeed, attackRange</param>
    public virtual void DisplayStats(float hpBarValue, params object[] stats)
    {
        UpdateHealthbar(hpBarValue);

        for (int i = 0; i < stats.Length; i++)
        {
            texts[i].text = stats[i].ToString();
        }
    }

    protected void UpdateHealthbar(float hpBarValue)
    {
        uiHealthBar.value = hpBarValue;
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