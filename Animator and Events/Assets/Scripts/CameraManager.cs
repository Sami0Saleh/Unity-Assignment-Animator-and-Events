using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamiCameraManager : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] GameObject camBody;

    private Vector3 _right = new Vector3(0.1f,0,0);
    private Vector3 _left = new Vector3(-0.1f,0,0);
    [SerializeField] Camera PodiumCamera;
    private void Start()
    {
        EventManager.WhoWon += MoveToPodium;
        camera.enabled = true;
        PodiumCamera.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            camBody.transform.Translate(_right);
        }
        if (Input.GetKey(KeyCode.A))
        {
            camBody.transform.Translate(_left);
        }
    }

    public void MoveToPodium(string[] WhoWon)
    {
        camera.enabled = false;
        PodiumCamera.enabled = true;
    }

}
