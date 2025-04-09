using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class BTTask_MoveToTarget : BTNode
{
    private NavMeshAgent agent;
    private GameObject target;
    private string targetKey;
    private float acceptableDistance;

    BehaviorTree tree;
    public BTTask_MoveToTarget(BehaviorTree tree,  string targetKey, float acceptableDistance = 1f)
    {
        this.tree = tree;
        this.targetKey = targetKey;
        this.acceptableDistance = acceptableDistance;
    }

    protected override NodeResult Execute()
    {
        BlackBoard blackBoard = tree.Blackboard;
        if (blackBoard == null || !blackBoard.GetBlackBoardData(targetKey , out target))
        {
            return NodeResult.Failure;
        }
        agent = tree.GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            return NodeResult.Failure;
        }

        if (IsTargetAcceptDistance())
        {
            return NodeResult.Success;
        }
        blackBoard.onBlackBoardValueChange += GetData;

        agent.SetDestination(target.transform.position);
        agent.isStopped = false;

        return NodeResult.Inprogress;
    }

    public void GetData(string key , object vla)
    {
        if(key == targetKey)
        {
            target = (GameObject)vla;
        }
    }

    protected override NodeResult Update()
    {
        if (target == null)
        {
            agent.isStopped = true;
            return NodeResult.Failure;
        }

        agent.SetDestination(target.transform.position);
        agent.isStopped = false;
        if(IsTargetAcceptDistance())
        {
            agent.isStopped = true;
            return NodeResult.Success;
        }
        return NodeResult.Inprogress;
    }
    public bool IsTargetAcceptDistance()
    {
        return Vector3.Distance(tree.transform.position, target.transform.position) <= acceptableDistance;
    }
}
