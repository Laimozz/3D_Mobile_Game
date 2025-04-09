using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BTTask_Wait : BTNode
{
    private float waitTime = 3f;
    private float timeElapse = 0f;
    private EnemyAnimation enemyAnimation;

    private BehaviorTree treeRoot;
    NavMeshAgent agent;

    public BTTask_Wait(BehaviorTree treeRoot ,float waitTime) 
    {
        this.treeRoot = treeRoot;
        this.waitTime = waitTime;
        agent = treeRoot.GetComponent<NavMeshAgent>();
        enemyAnimation = treeRoot.GetComponent<EnemyAnimation>();
    }
    

    protected override NodeResult Execute()
    {
        if (!treeRoot.isWait)
        {
            return NodeResult.Success;
        }
        agent.isStopped = true;
        if(waitTime <= 0f)
        {
            return NodeResult.Success;
        }
        Debug.Log($"Wait to start {waitTime}");
        enemyAnimation.IdleState();

        timeElapse = 0f;
        return NodeResult.Inprogress;
    }

    protected override NodeResult Update()
    {
        agent.isStopped = true;
        enemyAnimation.IdleState();

        if (!treeRoot.isWait)
        {
            return NodeResult.Success;
        }

        timeElapse += Time.deltaTime;
        if(timeElapse >= waitTime)
        {
            Debug.Log("Finish");
            treeRoot.isWait = false;
            return NodeResult.Success;
        }
       //Debug.Log($"Finish in {waitTime - timeElapse}");
        return NodeResult.Inprogress;
    }
}
