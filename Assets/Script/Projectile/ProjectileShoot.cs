using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileShoot : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] private float heightDistance;
    [SerializeField] private float dame;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ShootToPlayer(Vector3 player)
    {
        //Up
        float gravity = Physics.gravity.magnitude;
        float time = Mathf.Sqrt(2 * heightDistance / gravity);

        // Forward
        Vector3 distanceBw = player - transform.position;
        distanceBw.y = 0;
        float s = distanceBw.magnitude;

        //speed
        float speedForward =  s / (2 * time); // v = s/2t
        float speedUp = gravity * time; // v= gt

        Vector3 force = speedUp * Vector3.up + speedForward * distanceBw.normalized;

        rb.AddForce(force , ForceMode.VelocityChange);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerHeath>().TakeDamage(dame);
        }
        Destroy(gameObject);
    }
}
