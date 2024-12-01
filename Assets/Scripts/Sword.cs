using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MeleeWeapon
{ 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
