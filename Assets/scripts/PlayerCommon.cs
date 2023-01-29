using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommon : MonoBehaviour
{
    private Animator anim;
    [HideInInspector]
    public bool IsPunching = false;
    [HideInInspector]
    public bool IsKicking = false;
    [HideInInspector]
    public bool IsJumping = false;
    [HideInInspector]
    public bool IsIdle = false;
    [HideInInspector]
    public bool IsMoving = false;
    [HideInInspector]
    public bool CanChangeHeight = true;
    [HideInInspector]
    public bool CanChangeAttackDefend = true;
    public bool GotHit = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Set()
    {
        IsJumping = false;
        IsKicking = false;
        IsPunching = false;

        IsMoving = false;
        CanChangeHeight = true;
        CanChangeAttackDefend = true;
    }

    void SetJump()
    {
        IsKicking = false;
        IsPunching = false;
    }
    void SetPunch()
    {
        IsKicking = false;
        IsJumping = false;
    }
    void SetKick()
    {
        IsPunching = false;
        IsJumping = false;
    }
    void UnSetJump()
    {
        IsJumping = false;
        anim.SetTrigger("IdleTrig");
        CanChangeHeight = true;
        CanChangeAttackDefend = true;
    }
    void UnSetPunch()
    {
        IsPunching = false;
        anim.SetTrigger("IdleTrig");
        CanChangeHeight = true;
        CanChangeAttackDefend = true;
    }
    void UnSetKick()
    {
        IsKicking = false;
        anim.SetTrigger("IdleTrig");
        CanChangeHeight = true;
        CanChangeAttackDefend = true;
    }
    void UnSetMoving()
    {
        IsMoving = false;
    }

    void AfterGettingUp()
    {
        anim.SetTrigger("IdleTrig");
        GotHit = false;
    }
}
