using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.Searcher.AnalyticsEvent;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] Animator blueAnimator;
    [SerializeField] Animator redAnimator;
    [SerializeField] Animator yellowAnimator;

    public bool Dead;

    private string _blueAgentType = "blue";
    private string _redAgentType = "red";
    private string _yellowAgentType = "yellow";

    void Start()
    {

        EventManager.JumpEvent += JumpAnimation;
        EventManager.StopJumpEvent += StopJumpAnimation;

        EventManager.RunEvent += RunAnimation;
        EventManager.StopRunEvent += StopRunAnimation;

        EventManager.SteepWalkEvent +=  SteepWalkAnimation;
        EventManager.StopSteepWalkEvent += StopSteepWalkAnimation;

        EventManager.DeathEvent += DeathAnimation; 
    }

    public void JumpAnimation(string AgentType) // done
    {
        if (AgentType == _blueAgentType)
        { blueAnimator.SetBool("Jump", true); }
        else if (AgentType == _redAgentType)
        { redAnimator.SetBool("Jump", true); }
        else if (AgentType == _yellowAgentType)
        { yellowAnimator.SetBool("Jump", true); }
    }
    public void StopJumpAnimation(string AgentType) // done
    {
        if (AgentType == _blueAgentType)
        { blueAnimator.SetBool("Jump", false); }
        else if (AgentType == _redAgentType)
        { redAnimator.SetBool("Jump", false); }
        else if (AgentType == _yellowAgentType)
        { yellowAnimator.SetBool("Jump", false); }
    }
    public void RunAnimation(string AgentType)
    {
        if (AgentType == _blueAgentType)
        { blueAnimator.SetBool("CanRun", true); blueAnimator.SetFloat("Speed", 2); }
        else if (AgentType == _redAgentType)
        { redAnimator.SetBool("CanRun", true); redAnimator.SetFloat("Speed", 2); }
        else if (AgentType == _yellowAgentType)
        { yellowAnimator.SetBool("CanRun", true); yellowAnimator.SetFloat("Speed", 2); }

    }
    public void StopRunAnimation(string AgentType)
    {
        if (AgentType == _blueAgentType)
        { blueAnimator.SetBool("CanRun", false); blueAnimator.SetFloat("Speed", 1);}
        else if (AgentType == _redAgentType)
        { redAnimator.SetBool("CanRun", false); redAnimator.SetFloat("Speed", 1); }
        else if (AgentType == _yellowAgentType)
        { yellowAnimator.SetBool("CanRun", false); yellowAnimator.SetFloat("Speed", 1); }

    }
    public void SteepWalkAnimation(string AgentType)
    {
        if (AgentType == _blueAgentType)
        { blueAnimator.SetBool("SteepFloor", true); blueAnimator.SetFloat("Speed", 0.5f); }
        else if (AgentType == _redAgentType)
        { redAnimator.SetBool("SteepFloor", true); redAnimator.SetFloat("Speed", 0.5f); }
        else if (AgentType == _yellowAgentType)
        { yellowAnimator.SetBool("SteepFloor", true); yellowAnimator.SetFloat("Speed", 0.5f); }


    }
    public void StopSteepWalkAnimation(string AgentType)
    {
        if (AgentType == _blueAgentType)
        { blueAnimator.SetBool("SteepFloor", false); blueAnimator.SetFloat("Speed", 1); }
        else if (AgentType == _redAgentType)
        { redAnimator.SetBool("SteepFloor", false); redAnimator.SetFloat("Speed", 1); }
        else if (AgentType == _yellowAgentType)
        { yellowAnimator.SetBool("SteepFloor", false); yellowAnimator.SetFloat("Speed", 1); }
    }
    public void DeathAnimation(string AgentType)
    {
        if (AgentType == _blueAgentType)
        { blueAnimator.SetBool("Dead", true); }
        else if (AgentType == _redAgentType)
        { redAnimator.SetBool("Dead", true); }
        else if (AgentType == _yellowAgentType)
        { yellowAnimator.SetBool("Dead", true); }
    }   
}
