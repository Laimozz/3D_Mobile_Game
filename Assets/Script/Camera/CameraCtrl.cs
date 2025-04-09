using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField] private Transform followPos;
    [SerializeField] private float turnSpeed;

    private void LateUpdate()
    {
        transform.position = followPos.position;
    }

    public void RotateCamera(float amt)
    {
        transform.Rotate(Vector3.up, amt * turnSpeed * Time.deltaTime);
    }
}
