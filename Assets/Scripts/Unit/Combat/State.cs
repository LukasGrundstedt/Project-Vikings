using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Soldier soldier;

    public State(Soldier soldier)
    {
        this.soldier = soldier;
    }

    public virtual void OnStateEnter()
    {

    }

    public virtual void OnStateUpdate()
    {

    }

    public virtual void OnStateExit() 
    { 
    
    }
}