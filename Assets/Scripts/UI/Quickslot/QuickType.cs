using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickType : MonoBehaviour
{
    public Equipment equipment;

    public Equipment Equipment => equipment;

    public bool IsEmpty => transform.childCount == 0;

    public void RemoveItem()
    {
        Transform child = transform.GetChild(0);
        if(child != null)
        {
            Destroy(child.gameObject);
        }
    }
}


