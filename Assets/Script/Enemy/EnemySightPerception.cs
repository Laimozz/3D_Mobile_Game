using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightPerception : EnemyPerception
{
    [SerializeField] private float sightDistance;
    [SerializeField] private float sightAngle;
    [SerializeField] private float eyeHight;
    public override bool AwarePlayer()
    {
        Vector3 origin = transform.forward;
        Vector3 dir = (playerCtrl.transform.position - transform.position).normalized;

        float currentAngle = Vector3.Angle(origin, dir);
        if(currentAngle > sightAngle)
        {
            return false;
        }

        if(Physics.Raycast(transform.position + Vector3.up*eyeHight , dir , out RaycastHit hitInfo , sightDistance)) 
        {
            if(hitInfo.collider.GetComponent<PlayerCtrl>() != playerCtrl) 
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        return true;
    }

    public override void DrawGizmos()
    {
        base.DrawGizmos();

        Vector3 center = transform.position + Vector3.up * eyeHight;
        Gizmos.DrawWireSphere(center, sightDistance);


        Vector3 leftSight = Quaternion.AngleAxis(sightAngle, Vector3.up) * transform.forward * sightDistance;
        Vector3 rightSight = Quaternion.AngleAxis(-sightAngle, Vector3.up) * transform.forward * sightDistance;

        Gizmos.DrawLine(center, center + leftSight);
        Gizmos.DrawLine(center, center + rightSight);
    }
}
