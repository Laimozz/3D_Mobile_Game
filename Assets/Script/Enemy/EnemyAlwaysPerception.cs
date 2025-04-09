using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlwaysPerception : EnemyPerception
{
    [SerializeField] private float distanceToAware;
    public override bool AwarePlayer()
    {
        float currenDistance = Vector3.Distance(transform.position , playerCtrl.transform.position);
        return currenDistance <= distanceToAware;
    }

    public override void DrawGizmos()
    {
        base.DrawGizmos();
        Gizmos.DrawWireSphere(transform.position, distanceToAware);
    }
}
