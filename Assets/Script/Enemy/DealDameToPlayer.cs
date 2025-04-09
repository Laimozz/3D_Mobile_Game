using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDameToPlayer : MonoBehaviour
{
    [SerializeField] private float dame;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.GetComponent<PlayerHeath>().TakeDamage(dame);
        }
    }
}
