using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action JumpEvent;
    
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        { JumpEvent?.Invoke(); }
    }
}
