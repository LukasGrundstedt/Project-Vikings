using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MeleeWeapon
{
    /// <summary>
    /// Method called by AnimationEvent
    /// </summary>
    public void Impact()
    {
        OnImpact?.Invoke();
        PlayImpactSound();
    }

    private void PlayImpactSound()
    {

    }
}
