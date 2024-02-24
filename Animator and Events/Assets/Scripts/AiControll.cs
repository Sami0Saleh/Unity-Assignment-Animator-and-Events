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
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform[] pathWaypoints;

    public RaycastHit AiRaycast;
    private int currentWaypointIndex = 0;
    private bool Jumping = false;
    private float jumpHeight = 2.0f;
    private float jumpDuration = 1f;

    private Vector3 walkPoint;

    private void Start()
    {
        EventManager.JumpEvent += StartJump;
        EventManager.RunEvent += StartRunning;
        EventManager.StopRunEvent += StopRunning;

        navMeshAgent.speed = 1;
        navMeshAgent.SetDestination(pathWaypoints[currentWaypointIndex].position);
        
    }
    private void Update()
    {
        SearchWalkPoint();
    }
    public void SearchWalkPoint()
    {

        walkPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(walkPoint, out hit, 0.1f, 1 << NavMesh.GetAreaFromName("Stairs")))
        { Debug.Log("Stairs"); }
        
    }
    public void StartJump()
    {
        if (!navMeshAgent.isOnOffMeshLink)
        { return; }
        StartCoroutine(Jump());
    }
    
    public void StartRunning()
    {
        navMeshAgent.speed = 2f;
    }

    public void StopRunning()
    {
        navMeshAgent.speed = 1f;
    }

    IEnumerator Jump()
    {
        Jumping = true;

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
        Jumping = false;

        navMeshAgent.CompleteOffMeshLink();
    }
}

