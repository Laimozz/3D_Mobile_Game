using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterBehavior : BehaviorTree
{
    EnemyPatrol enemyPatrol;
    [SerializeField] private Transform lauchPoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform player;
    private void Awake()
    {
        enemyPatrol = GetComponent<EnemyPatrol>();
    }

    public override void ContructorTree(out BTNode root)
    {
        Selector treeRoot = new Selector();
        enemyPatrol.GetRandomPoint();
        BTTask_Patrol patrol = new BTTask_Patrol(this, enemyPatrol.PointKey, 1f);

        BTTask_Chase chase = new BTTask_Chase(this, "Player", 4f);

        BTTask_Wait wait = new BTTask_Wait(this, 3f);

        BTTask_Attack attack = new BTTask_Attack(this, "Player", 4.1f, 2f);

        Sequencer WaitAndPatrol = new Sequencer();
        WaitAndPatrol.AddChild(wait);
        WaitAndPatrol.AddChild(patrol);

        treeRoot.AddChild(attack);
        treeRoot.AddChild(chase);
        treeRoot.AddChild(WaitAndPatrol);

        root = treeRoot;
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(projectile , lauchPoint);
        bullet.GetComponent<ProjectileShoot>().ShootToPlayer(player.transform.position);
    }
}
