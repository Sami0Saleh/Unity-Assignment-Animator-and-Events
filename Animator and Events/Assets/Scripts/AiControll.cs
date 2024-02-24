using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class AiControll : MonoBehaviour
{
    // SerializeFields
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform[] pathWaypoints;

    // Private Variables
    private int currentWaypointIndex = 0;
    private float jumpHeight = 2.0f;
    private float jumpDuration = 1f;

    private string AgentType;
    // private Vector3 walkPoint;

    private void Start()
    {
        // setting agent type
        if (this.tag != "blue")
        { AgentType = "red"; }
        else { AgentType = "blue"; }

        // subscribing to events
        EventManager.JumpEvent += StartJump;
        EventManager.RunEvent += StartRunning;
        EventManager.StopRunEvent += StopRunning;
        EventManager.SteepWalkEvent += StartSteep;


        // Setting NavMeshAgent Variables
        navMeshAgent.speed = 1;
        navMeshAgent.SetDestination(pathWaypoints[currentWaypointIndex].position);
        
    }
    private void Update()
    {
       
    }
    public void StartJump(string agentType)
    {
        if (agentType == AgentType)
        { StartCoroutine(Jump()); }
    }
    public void StartRunning(string agentType)
    {
        if (agentType == AgentType)
            navMeshAgent.speed = 2f;
    }
    public void StopRunning(string agentType)
    {
        if (agentType == AgentType)
            navMeshAgent.speed = 1f;
    }
    public void StartSteep(string agentType)
    {

        if (agentType == AgentType)
        {
            Debug.Log($"{agentType} should be slowing");
            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.acceleration = 0;
            navMeshAgent.speed = 0.2f;
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

