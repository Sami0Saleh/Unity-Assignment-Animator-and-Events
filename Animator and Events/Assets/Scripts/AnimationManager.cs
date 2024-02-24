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
        EventManager.StopJumpEvent += StopJumpAnimation;
        EventManager.RunEvent += RunAnimation;
        EventManager.RunEvent += SetSpeed;
        EventManager.StopRunEvent += StopRunAnimation;
        EventManager.SteepWalkEvent += SteepWalkAnimation;
        EventManager.StopSteepWalkEvent += StopSteepWalkAnimation;
    }

    void Update()
    {
        if (Dead)
        { animator.SetBool("Dead", true); }
        else if (!Dead)
        { animator.SetBool("Dead", false); }
    }

    public void JumpAnimation()
    {
        animator.SetBool("Jump", true);
    }
    public void StopJumpAnimation()
    {
        animator.SetBool("Jump", false);
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
    public void SteepWalkAnimation()
    {
        animator.SetBool("SteepFloor", true);
    }
    public void StopSteepWalkAnimation()
    {
        animator.SetBool("SteepFloor", false);
    }
}
