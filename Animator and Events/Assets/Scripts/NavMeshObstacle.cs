using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshObstacle : MonoBehaviour
{
    public Vector3 StartingPos;
    public Vector3 EndingPos;
    Vector3 MoveLeft = Vector3.left;
    Vector3 MoveRight = Vector3.right;
    public bool Direction = false;
    private float _speed;
   
    public float minSpeed = 7;
    public float maxSpeed = 13;

    // Start is called before the first frame update
    void Start()
    {
        StartingPos = transform.position;
        _speed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > EndingPos.z && Direction == false)
        { transform.Translate(MoveLeft * Time.deltaTime * _speed); }
        else if (transform.position.z <= EndingPos.z)
        { Direction = true; }
        if (transform.position.z < StartingPos.z && Direction == true)
        { transform.Translate(MoveRight * Time.deltaTime * _speed); }
        else if (transform.position.z >= StartingPos.z)
        { Direction = false; }
    }
}
