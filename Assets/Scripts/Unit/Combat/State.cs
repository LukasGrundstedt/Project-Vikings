using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Entity entity;

    public State(Entity entity)
    {
        this.entity = entity;
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
