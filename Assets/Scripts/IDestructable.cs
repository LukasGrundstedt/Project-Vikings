using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IDestructable
{
    public float HitPoints { get; set; }
    public float Toughness { get; set; }
    public AnimationClip DestructionAnimation { get; set; }
    public bool IsDestroyed { get; set; }

    public virtual void TakeDamage(float damage)
    {
        float netDamage = Mathf.Max(0f, damage - Toughness);
        HitPoints = Mathf.Max(0f, HitPoints - netDamage);

        if (HitPoints <= 0f) OnDestruction();
    }

    public void OnDestruction();
}