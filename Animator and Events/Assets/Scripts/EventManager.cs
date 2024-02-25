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

    public static event Action WhosLeading;

    public static event Action<string> WhoWon;
    // NavMeshAgents
    [SerializeField] private NavMeshAgent RedNavMeshAgent;
    [SerializeField] private NavMeshAgent BlueNavMeshAgent;
    [SerializeField] private NavMeshAgent YellowNavMeshAgent;
    
    // private Variables
    private float timer = 0f;
    private float delayAmount = 1f;
    private bool Run = true;
    private float _staminaBar = 5;
    private Vector3 _redWalkPoint;
    private Vector3 _blueWalkPoint;
    private Vector3 _yellowWalkPoint;
    private bool RedSteepStep = false;
    private bool BlueSteepStep = false;
    private bool yellowSteepStep = false;

    public bool DeathBool =  false;
    void Update()
    {
        Jumping();
        Running();
        CheckNavMeshSurface();
        Death();
        WhosLeading?.Invoke();
    }
    public void CheckNavMeshSurface()
    {
        // checking the NavMeshSurface
        NavMeshHit hit;
        Transform tR = RedNavMeshAgent.gameObject.transform;
        Transform tB = BlueNavMeshAgent.gameObject.transform;
        Transform tY = YellowNavMeshAgent.gameObject.transform;
        _redWalkPoint = new Vector3(tR.position.x, tR.position.y, tR.position.z);
        _blueWalkPoint = new Vector3(tB.position.x, tB.position.y, tB.position.z);
        _yellowWalkPoint = new Vector3(tY.position.x, tY.position.y, tY.position.z);

        if (NavMesh.SamplePosition(_redWalkPoint, out hit, 0.1f, 1 << NavMesh.GetAreaFromName("Stairs")))
        { StopRunEvent?.Invoke("red"); SteepWalkEvent?.Invoke("red"); RedSteepStep = true; }
        else { if (RedSteepStep) StopSteepWalkEvent?.Invoke("red"); RedSteepStep = false; }

        if (NavMesh.SamplePosition(_blueWalkPoint, out hit, 0.1f, 1 << NavMesh.GetAreaFromName("Stairs")))
        { StopRunEvent?.Invoke("blue"); SteepWalkEvent?.Invoke("blue"); BlueSteepStep = true; }
        else { if (BlueSteepStep) StopSteepWalkEvent?.Invoke("blue"); BlueSteepStep = false; }

        if (NavMesh.SamplePosition(_yellowWalkPoint, out hit, 0.1f, 1 << NavMesh.GetAreaFromName("Stairs")))
        { StopRunEvent?.Invoke("yellow"); SteepWalkEvent?.Invoke("yellow"); yellowSteepStep = true; }
        else { if (yellowSteepStep) StopSteepWalkEvent?.Invoke("yellow"); yellowSteepStep = false; }
    }
    public void Jumping()
    {
        if (RedNavMeshAgent.isOnOffMeshLink)
        {  JumpEvent?.Invoke("red"); }
        else { StopJumpEvent?.Invoke("red"); }
        if (BlueNavMeshAgent.isOnOffMeshLink)
        { JumpEvent?.Invoke("blue"); }
        else  { StopJumpEvent?.Invoke("blue");}
        if (YellowNavMeshAgent.isOnOffMeshLink)
        { JumpEvent?.Invoke("yellow"); }
        else { StopJumpEvent?.Invoke("yellow"); }
    }
    public void Running()
    {
        timer += Time.deltaTime;
        if (Run)
        {   RunEvent?.Invoke("red"); RunEvent?.Invoke("blue"); RunEvent?.Invoke("yellow");
            if (timer >= delayAmount)
            {
                timer = 0f;
                _staminaBar--;
                if (_staminaBar <= 0)
                { StopRunEvent?.Invoke("red"); StopRunEvent?.Invoke("blue"); StopRunEvent?.Invoke("yellow"); Run = false;}
            }
        }
        else if (!Run)
        {
            if (timer >= delayAmount)
            {
                timer = 0f;
                _staminaBar += 0.5f;
                if (_staminaBar >= 5)
                { RunEvent?.Invoke("red"); RunEvent?.Invoke("blue"); RunEvent?.Invoke("yellow"); Run = true; }
            }
        }
    }
    public void Death()
    {
        if (BlueNavMeshAgent.transform.position == BlueNavMeshAgent.destination)
        {
            DeathEvent?.Invoke("red"); DeathEvent?.Invoke("yellow"); WhoWon?.Invoke("blue");
        }
       
        if(RedNavMeshAgent.transform.position == RedNavMeshAgent.destination)
        {
            DeathEvent?.Invoke("blue"); DeathEvent?.Invoke("yellow"); WhoWon?.Invoke("red");
        }

        if (RedNavMeshAgent.transform.position == RedNavMeshAgent.destination)
        {
            DeathEvent?.Invoke("blue"); DeathEvent?.Invoke("red"); WhoWon?.Invoke("yellow");
        }
    }
}
