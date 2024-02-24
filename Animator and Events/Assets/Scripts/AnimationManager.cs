using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    public bool canRun;
    public bool SteepFloor;
    public bool Dead;
    void Start()
    {
        EventManager.JumpEvent += JumpAnimation;
        EventManager.RunEvent += RunAnimation;
        EventManager.RunEvent += SetSpeed;
        EventManager.StopRunEvent += StopRunAnimation;
    }

    void Update()
    {
        if (SteepFloor)
        { animator.SetBool("SteepFloor", true); }
        else if (!SteepFloor)
        { animator.SetBool("SteepFloor", false); }
        // liner areas speed decrease
        if (Dead)
        { animator.SetBool("Dead", true); }
        else if (!Dead)
        { animator.SetBool("Dead", false); }
    }

 

    public void JumpAnimation()
    {
        animator.SetBool("Jump", true);
    }

    public void SetSpeed()
    {
        //animator.SetFloat("RunningSpeed", 100f);
    }
    public void RunAnimation()
    {
        animator.SetBool("CanRun", true);
    }
    public void StopRunAnimation()
    {
        animator.SetBool("CanRun", false);

    }
}
