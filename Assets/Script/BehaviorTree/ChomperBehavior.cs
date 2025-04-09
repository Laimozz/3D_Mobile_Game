using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBehavior : BehaviorTree
{
    EnemyPatrol enemyPatrol;

    private void Awake()
    {
        enemyPatrol = GetComponent<EnemyPatrol>();  
    }
    public override void ContructorTree(out BTNode root)
    {
        Selector treeRoot = new Selector();
        enemyPatrol.GetRandomPoint();
        BTTask_Patrol patrol = new BTTask_Patrol(this, enemyPatrol.PointKey, 1f);
        BTTask_Chase chase = new BTTask_Chase(this, "Player", 1f);
        BTTask_Wait wait = new BTTask_Wait(this, 4f);

        BTTask_Attack attack = new BTTask_Attack(this, "Player", 1f, 2f);

        Sequencer WaitAndPatrol = new Sequencer();
        WaitAndPatrol.AddChild(wait);
        WaitAndPatrol.AddChild(patrol);

        // treeRoot
        treeRoot.AddChild(attack);
        treeRoot.AddChild(chase);
        treeRoot.AddChild(WaitAndPatrol);

        root = treeRoot;

    }
}
