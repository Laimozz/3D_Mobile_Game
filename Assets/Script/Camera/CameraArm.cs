using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArm : MonoBehaviour
{
    [SerializeField] private float lengthArm;
    [SerializeField] private Transform mainCamera;
    void Update()
    {
        mainCamera.position = transform.position - mainCamera.forward * lengthArm;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(mainCamera.position, transform.position);
    }
}
