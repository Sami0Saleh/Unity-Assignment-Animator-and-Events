using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AiControll : MonoBehaviour
{
    // SerializeFields
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform Target;

    [SerializeField] private Transform FirstPlaceTransform;
    [SerializeField] private Transform SecondPlaceTransform;
    [SerializeField] private Transform ThirdPlaceTransform;

    // Private Variables
    private float jumpHeight = 2.0f;
    private float jumpDuration = 1f;
    private bool podiumSet = false;

    private string AgentType;
    // private Vector3 walkPoint;

    private void Start()
    {
        // setting agent type
        if (this.tag == "red")
        { AgentType = "red"; }
        if (this.tag == "blue")
        { AgentType = "blue"; }
        if (this.tag == "yellow")
        { AgentType = "yellow"; }

        // subscribing to events
        EventManager.JumpEvent += StartJump;
        EventManager.RunEvent += StartRunning;
        EventManager.StopRunEvent += StopRunning;
        EventManager.SteepWalkEvent += StartSteep;
        EventManager.StopSteepWalkEvent += StopSteep;
        EventManager.DeathEvent += Death;
        EventManager.WhoWon += SetPodium;


        // Setting NavMeshAgent Variables
        navMeshAgent.speed = 1;
        navMeshAgent.SetDestination(Target.position);    
    }
    public void StartJump(string agentType)
    {
        if (agentType == AgentType)
        {
            StartCoroutine(Jump());
        }
    }
    public void StartRunning(string agentType)
    {
      //  if (agentType == AgentType)
         //   navMeshAgent.speed = 2f;
    }
    public void StopRunning(string agentType)
    {
       // if (agentType == AgentType)
            //navMeshAgent.speed = 1f;
    }
    public void StartSteep(string agentType)
    {

        if (agentType == AgentType)
        {
            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.acceleration = 0;
        }
    }
    public void StopSteep(string agentType)
    {
        if (agentType == AgentType)
        {
            navMeshAgent.acceleration = 8;
        }
    }

    public void Death(string agentType)
    {
        if (agentType == AgentType)
        {
            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.acceleration = 0;
        }
    }

    public void SetPodium(string[] agentDetails)
    {
        if (agentDetails[0] == AgentType && !podiumSet)
        {
            switch (agentDetails[1])
            {
                case "first": transform.position = FirstPlaceTransform.position; podiumSet = true; break;
                case "second": transform.position = SecondPlaceTransform.position; podiumSet = true; break;
                case "third": transform.position = ThirdPlaceTransform.position; podiumSet = true; break;
            }
            
        }
    }

    IEnumerator Jump()
    {
        OffMeshLinkData data = navMeshAgent.currentOffMeshLinkData;
        Vector3 startPos = navMeshAgent.transform.position;
        Vector3 endPos = data.endPos;

        float timeElapsed = 0f;

        while (timeElapsed < jumpDuration)
        {
            float t = timeElapsed / jumpDuration;
            float yOffset = jumpHeight * 4.0f * (t - t * t);
            navMeshAgent.transform.position = Vector3.Lerp(startPos, endPos, t) + yOffset * Vector3.up;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        navMeshAgent.transform.position = endPos;
        navMeshAgent.CompleteOffMeshLink();
    }

}

