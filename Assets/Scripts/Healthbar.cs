using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    public void UpdateHealthBar(int value, int maxValue)
    {
        healthBar.value = (float)value / maxValue;
    }
}