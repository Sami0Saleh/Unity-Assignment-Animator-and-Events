using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    public bool canRun;
    public bool SteepFloor;
    public bool Dead;
    public bool Jump;
    void Start()
    {
        EventManager.JumpEvent += JumpAnimation;
    }

    void Update()
    {
        if (canRun)
        { animator.SetBool("CanRun", true); }
        else if (!canRun)
        { animator.SetBool("CanRun", false); }

        if (SteepFloor)
        { animator.SetBool("SteepFloor", true); }
        else if (!SteepFloor)
        { animator.SetBool("SteepFloor", false); }
        // liner areas speed decrease
        if (Dead)
        { animator.SetBool("Dead", true); }
        else if (!Dead)
        { animator.SetBool("Dead", false); }

        if (Jump)
        { animator.SetBool("Jump", true); }
        else if (!Jump)
        { animator.SetBool("Jump", false); }
    }

    public void SetSpeed()
    {
        animator.SetFloat("RunningSpeed", 100f);
    }

    public void JumpAnimation()
    {
        animator.SetBool("Jump", true);
    }
}
