using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class EventManager : MonoBehaviour
{
    // Events
    public static event Action JumpEvent;
    public static event Action RunEvent;
    public static event Action DeathEvent;
    public static event Action SteepWalkEvent;

    public static event Action StopRunEvent;
    
    // NavMeshAgents
    [SerializeField] private NavMeshAgent RedNavMeshAgent;
    [SerializeField] private NavMeshAgent BlueNavMeshAgent;

    private float timer = 0f;
    public float delayAmount = 1f;

    public bool Run = true;
    private float _staminaBar = 5;

    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        Jumping();
        Running();
    }

    public void Jumping()
    {
        if (RedNavMeshAgent.isOnOffMeshLink || BlueNavMeshAgent.isOnOffMeshLink)
        {
            //StopRunEvent?.Invoke();
            //JumpEvent?.Invoke();
        }
    }
    public void Running()
    {
        timer += Time.deltaTime;

        if (Run)
        {
            RunEvent?.Invoke();
            if (timer >= delayAmount)
            {
                timer = 0f;
                _staminaBar--;
                if (_staminaBar <= 0)
                {
                    StopRunEvent?.Invoke(); Run = false;
                }
            }
        }
        else if (!Run)
        {
            if (timer >= delayAmount)
            {
                timer = 0f;
                _staminaBar += 0.5f;
                if (_staminaBar >= 5)
                {
                    RunEvent?.Invoke(); Run = true;
                }
            }
        }
    }
    }
