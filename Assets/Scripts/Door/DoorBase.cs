using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBase : MonoBehaviour
{

    protected virtual bool preOpen()
    {
        return true;
    }
    protected virtual void Open()
    {

    }

    public virtual void Interect() 
    {
    
    }
}
