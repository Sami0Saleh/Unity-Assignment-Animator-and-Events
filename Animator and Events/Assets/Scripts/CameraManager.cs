using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamiCameraManager : MonoBehaviour
{
    [SerializeField] Camera camera;

    private Vector3 _right = new Vector3(0.1f,0,0);
    private Vector3 _left = new Vector3(-0.1f,0,0);

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            camera.transform.Translate(_right);
        }
        if (Input.GetKey(KeyCode.A))
        {
            camera.transform.Translate(_left);
        }
    }
}
