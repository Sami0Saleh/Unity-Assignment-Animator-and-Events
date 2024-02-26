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
        
        EventManager.SteepWalkEvent += StartSteep;
        EventManager.StopSteepWalkEvent += StopSteep;
        EventManager.DeathEvent += Death;
        EventManager.WhoWon += SetPodium;


        // Setting NavMeshAgent Variables
        navMeshAgent.speed = 1;
        navMeshAgent.SetDestination(Target.position);    
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
            Target = FirstPlaceTransform;
            navMeshAgent.enabled = false;
            switch (agentDetails[1])
            {
                case "first": transform.position = FirstPlaceTransform.position; Target = FirstPlaceTransform; break;
                case "second": transform.position = SecondPlaceTransform.position; Target = SecondPlaceTransform; break;
                case "third": transform.position = ThirdPlaceTransform.position; Target = ThirdPlaceTransform; break;
                default: Debug.Log("Bug 1"); break;
            }
            
        }
    }
}

