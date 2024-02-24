using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using Unity.VisualScripting;

public class EventManager : MonoBehaviour
{
    // Events
    public static event Action<string> JumpEvent;
    public static event Action<string> StopJumpEvent;

    public static event Action<string> RunEvent;
    public static event Action<string> StopRunEvent;

    public static event Action<string> SteepWalkEvent;
    public static event Action<string> StopSteepWalkEvent;

    public static event Action<string> DeathEvent;

    // NavMeshAgents
    [SerializeField] private NavMeshAgent RedNavMeshAgent;
    [SerializeField] private NavMeshAgent BlueNavMeshAgent;
    
    // private Variables
    private float timer = 0f;
    private float delayAmount = 1f;
    private bool Run = true;
    private float _staminaBar = 5;
    private Vector3 _redWalkPoint;
    private Vector3 _blueWalkPoint;

    public bool DeathBool =  false;
    void Update()
    {
        Jumping();
        Running();
        CheckNavMeshSurface();
        Death();
    }
    public void CheckNavMeshSurface()
    {
        // checking the NavMeshSurface
        NavMeshHit hit;
        Transform tR = RedNavMeshAgent.gameObject.transform;
        Transform tB = BlueNavMeshAgent.gameObject.transform;
        _redWalkPoint = new Vector3(tR.position.x, tR.position.y, tR.position.z);
        _blueWalkPoint = new Vector3(tB.position.x, tB.position.y, tB.position.z);

        if (NavMesh.SamplePosition(_redWalkPoint, out hit, 0.1f, 1 << NavMesh.GetAreaFromName("Stairs")))
        { StopRunEvent?.Invoke("red"); SteepWalkEvent?.Invoke("red"); }
        else { StopSteepWalkEvent?.Invoke("red"); }

        if (NavMesh.SamplePosition(_blueWalkPoint, out hit, 0.1f, 1 << NavMesh.GetAreaFromName("Stairs")))
        { Debug.Log("Samiii"); StopRunEvent?.Invoke("blue"); SteepWalkEvent?.Invoke("blue"); }
        else { StopSteepWalkEvent?.Invoke("blue"); }
    }
    public void Jumping()
    {
        if (RedNavMeshAgent.isOnOffMeshLink)
        {  JumpEvent?.Invoke("red"); }
        else { StopJumpEvent?.Invoke("red"); }
        if (BlueNavMeshAgent.isOnOffMeshLink)
        { JumpEvent?.Invoke("blue"); }
        else  { StopJumpEvent?.Invoke("blue");}
    }
    public void Running()
    {
        timer += Time.deltaTime;
        if (Run)
        {   RunEvent?.Invoke("red"); RunEvent?.Invoke("blue");
            if (timer >= delayAmount)
            {
                timer = 0f;
                _staminaBar--;
                if (_staminaBar <= 0)
                { StopRunEvent?.Invoke("red"); StopRunEvent("blue"); Run = false;}
            }
        }
        else if (!Run)
        {
            if (timer >= delayAmount)
            {
                timer = 0f;
                _staminaBar += 0.5f;
                if (_staminaBar >= 5)
                { RunEvent?.Invoke("red"); RunEvent?.Invoke("blue"); Run = true; }
            }
        }
    }
    public void Death()
    {
        if (BlueNavMeshAgent.transform.position == BlueNavMeshAgent.destination)
        {
            DeathEvent?.Invoke("red"); Debug.Log(" Red Death");
        }
       
        if(RedNavMeshAgent.transform.position == RedNavMeshAgent.destination)
        {
            DeathEvent?.Invoke("blue"); Debug.Log("blue Death");
        }
       
    }
}
