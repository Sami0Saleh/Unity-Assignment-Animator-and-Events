using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class EventManager : MonoBehaviour
{
    public static event Action JumpEvent;
    public static event Action RunEvent;
    public static event Action DeathEvent;
    public static event Action SteepWalkEvent;

    [SerializeField] private NavMeshAgent RedNavMeshAgent;
    [SerializeField] private NavMeshAgent BlueNavMeshAgent;

    [SerializeField] Ray RedRaycast;


    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (RedNavMeshAgent.isOnOffMeshLink || BlueNavMeshAgent.isOnOffMeshLink)
        { JumpEvent?.Invoke(); }
        if (RedRaycast.direction.magnitude > 10)
        {
            Debug.Log("Can Run");
        }
    }
}
