using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTTask_Chase : BTNode 
{
    private NavMeshAgent agent;
    private GameObject target;
    private string targetKey;
    private float acceptableDistance;

    private EnemyAnimation enemyAnimation;
    BehaviorTree treeRoot;
    
    public BTTask_Chase(BehaviorTree tree, string targetKey, float acceptableDistance = 1f)
    {
        this.treeRoot = tree;
        this.targetKey = targetKey;
        this.acceptableDistance = acceptableDistance;
        enemyAnimation = tree.GetComponent<EnemyAnimation>();
    }

    protected override NodeResult Execute()
    {
        BlackBoard blackBoard = treeRoot.Blackboard;
        if (!treeRoot.isChase || treeRoot.isAttack)
        {
            return NodeResult.Failure;
        }

        if (blackBoard == null || !blackBoard.GetBlackBoardData(targetKey, out target))
        {
            return NodeResult.Failure;
        }
        agent = treeRoot.GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            return NodeResult.Failure;
        }

        if (IsTargetAcceptDistance())
        {
            treeRoot.isAttack = true;
            agent.isStopped = true;
            enemyAnimation.IdleState();
            return NodeResult.Success;
        }
        blackBoard.onBlackBoardValueChange += GetData;

        agent.SetDestination(target.transform.position);
        agent.isStopped = false;

        Vector3 dir = target.transform.position - treeRoot.transform.position;
        treeRoot.transform.rotation = Quaternion.RotateTowards(treeRoot.transform.rotation
            , Quaternion.LookRotation(dir), 100f * Time.deltaTime);

        enemyAnimation.RunState();

        return NodeResult.Inprogress;
    }

    public void GetData(string key, object vla)
    {
        if (key == targetKey)
        {
            target = (GameObject)vla;
        }
    }

    protected override NodeResult Update()
    {
        if (!treeRoot.isChase || treeRoot.isAttack)
        {
            return NodeResult.Failure;
        }

        if (target == null)
        {
            agent.isStopped = true;
            return NodeResult.Failure;
        }

        agent.SetDestination(target.transform.position);
        agent.isStopped = false;

        Vector3 dir = target.transform.position - treeRoot.transform.position;
        treeRoot.transform.rotation = Quaternion.RotateTowards(treeRoot.transform.rotation
            , Quaternion.LookRotation(dir), 100f * Time.deltaTime);

        enemyAnimation.RunState();

        if (IsTargetAcceptDistance())
        {
            treeRoot.isAttack = true; 
            agent.isStopped = true;
            enemyAnimation.IdleState();
            return NodeResult.Success;
        }
        return NodeResult.Inprogress;
    }
    public bool IsTargetAcceptDistance()
    {
        return Vector3.Distance(treeRoot.transform.position, target.transform.position) <= acceptableDistance;
    }
}
