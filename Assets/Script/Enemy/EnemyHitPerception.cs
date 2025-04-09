using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitPerception : EnemyPerception
{
    [SerializeField] private EnemyHeath enemyHeath;

    private void Start()
    {
        enemyHeath = GetComponent<EnemyHeath>();
    }
    public override bool AwarePlayer()
    {
        if(enemyHeath != null)
        {
            if (enemyHeath.couterAttack)
            {
                enemyHeath.couterAttack = false;
                return true;         
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
