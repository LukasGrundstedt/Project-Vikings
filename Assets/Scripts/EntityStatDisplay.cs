using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityStatDisplay : MonoBehaviour
{
    [SerializeField] protected Slider uiHealthBar;
    [SerializeField] protected Slider wsHealthBar;

    [SerializeField] protected TextMeshProUGUI[] texts;


    public static EntityStatDisplay Instance;
    public static Action<EntityStatDisplay> OnCreation;

    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private GameObject displayParent;

    protected Entity displayedEntity;

    private void Awake()
    {
        Instance = this;
        OnCreation?.Invoke(Instance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void DisplayEntity(Entity entity)
    {
        //Clear previous selection
        if (displayedEntity) displayedEntity.VisualizeSelection(false);

        displayedEntity = entity;
        label.text = entity.name;
        displayParent.SetActive(true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hpBarValue"></param>
    /// <param name="stats">atk, dmg, def, armor, attackSpeed, attackRange</param>
    public virtual void DisplayStats(float hpBarValue, params object[] stats)
    {
        uiHealthBar.value = hpBarValue;
        if (wsHealthBar) wsHealthBar.value = hpBarValue;

        for (int i = 0; i < stats.Length; i++)
        {
            texts[i].text = stats[i].ToString();
        }
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