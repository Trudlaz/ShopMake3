using System.Collections;
using TMPro;
using UnityEngine;

public class DoorManual : DoorBase
{   
    public float coolTime = 0.5f;

    bool isOpen = false;
    float currentCoolTime = 0;
    bool CanUse => currentCoolTime < 0.0f;

    Animator animator;

    readonly int IsOpenHash = Animator.StringToHash("IsOpen");

    private void Awake()
    {
        animator = GetComponent<Animator>(); ;
    }

    void Update()
    {
        currentCoolTime -= Time.deltaTime;
    }

    public override void Interect()
    {
        if(CanUse) 
        {
            if (isOpen)
            {
                Close();
                isOpen = false;
            }
            else
            {
                Open();
                isOpen = true;
            }
            currentCoolTime = coolTime;
        }        
    }

    protected virtual void Open()
    {
        if(preOpen()) animator.SetBool(IsOpenHash, true);
    }

    private void Close()
    {
        animator.SetBool(IsOpenHash, false);
    }
}
