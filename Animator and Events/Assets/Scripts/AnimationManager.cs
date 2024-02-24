using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.Searcher.AnalyticsEvent;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] Animator blueAnimator;
    [SerializeField] Animator redAnimator;

    public bool Dead;

    private string _blueAgentType = "blue";
    private string _redAgentType = "red";

    void Start()
    {
        _blueAgentType = blueAnimator.gameObject.tag;

        EventManager.JumpEvent += JumpAnimation;
        EventManager.StopJumpEvent += StopJumpAnimation;

        EventManager.RunEvent += RunAnimation;
        EventManager.RunEvent += SetSpeed;
        EventManager.StopRunEvent += StopRunAnimation;

        EventManager.SteepWalkEvent +=  SteepWalkAnimation;
        EventManager.StopSteepWalkEvent += StopSteepWalkAnimation;

        EventManager.DeathEvent += DeathAnimation; 
    }

    void Update()
    {
        if (Dead)
        { blueAnimator.SetBool("Dead", true); }
        else if (!Dead)
        { blueAnimator.SetBool("Dead", false); }
    }

    public void JumpAnimation(string AgentType) // done
    {
        if (AgentType == _blueAgentType)
        { blueAnimator.SetBool("Jump", true); }
        else if (AgentType == _redAgentType)
        { redAnimator.SetBool("Jump", true); }
    }
    public void StopJumpAnimation(string AgentType) // done
    {
        if (AgentType == _blueAgentType)
        { blueAnimator.SetBool("Jump", false); }
        else if (AgentType == _redAgentType)
        { redAnimator.SetBool("Jump", false); }
    }
    public void SetSpeed(string AgentType)
    {
        //animator.SetFloat("RunningSpeed", 100f);
    }
    public void RunAnimation(string AgentType)
    {
        if (AgentType == _blueAgentType)
        { blueAnimator.SetBool("CanRun", true); }
        else if (AgentType == _redAgentType)
        { redAnimator.SetBool("CanRun", true); }
        
    }
    public void StopRunAnimation(string AgentType)
    {
        if (AgentType == _blueAgentType)
        { blueAnimator.SetBool("CanRun", false); }
        else if (AgentType == _redAgentType)
        { redAnimator.SetBool("CanRun", false); }

    }
    public void SteepWalkAnimation(string AgentType)
    {
        if (AgentType == _blueAgentType)
        { blueAnimator.SetBool("SteepFloor", true); }
        else if (AgentType == _redAgentType)
        { redAnimator.SetBool("SteepFloor", true); ; }
        
    }
    public void StopSteepWalkAnimation(string AgentType)
    {
        if (AgentType == _blueAgentType)
        { blueAnimator.SetBool("SteepFloor", false); }
        else if (AgentType == _redAgentType)
        { redAnimator.SetBool("SteepFloor", false); ; }
    }

    public void DeathAnimation(string AgentType)
    {
        if (AgentType == _blueAgentType)
        { blueAnimator.SetBool("Dead", true); }
        else if (AgentType == _redAgentType)
        { redAnimator.SetBool("Dead", true); }
    }   
    public void StopDeathAnimation(string AgentType) // do i need this??
    {
        if (AgentType == _blueAgentType)
        { blueAnimator.SetBool("Dead", false); }
        else if (AgentType == _redAgentType)
        { redAnimator.SetBool("Dead", false); ; }
    }
}
