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
    private void Start()
    {
        EventManager.JumpEvent += StartJump;
        navMeshAgent.SetDestination(pathWaypoints[currentWaypointIndex].position);
    }
    private void Update()
    {
        if (AiRaycast.point == null)
        {
            Debug.Log("Can Run");
        }

        /*if (navMeshAgent.isOnOffMeshLink && !Jumping)
        {
            JumpEvent();
        }*/
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

    public void StarJump()
    {
    }
    public void StartJump()
    {
        if (!navMeshAgent.isOnOffMeshLink)
        { return; }
        StartCoroutine(Jump());
    }
}

