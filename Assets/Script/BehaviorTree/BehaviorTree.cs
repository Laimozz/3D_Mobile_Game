using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BehaviorTree : MonoBehaviour
{
    BTNode Root;

    BlackBoard blackBoard = new BlackBoard();

    public bool isAttack;

    public bool isChase;

    public bool isWait;

    public BlackBoard Blackboard
    {
        get { return blackBoard; }
    }
    private void Start()
    {
        ContructorTree(out Root);
    }

    public void SetRoot()
    {
        ContructorTree(out Root);
    }
    public abstract void ContructorTree(out BTNode root);


    private void Update()
    {
        if (PlayerCtrl.Instance.isStopTime || transform.GetComponent<EnemyHeath>().EnemyCurrentHeath <= 0)
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            return;
        }

        Root.UpdateNode();
    }
}
