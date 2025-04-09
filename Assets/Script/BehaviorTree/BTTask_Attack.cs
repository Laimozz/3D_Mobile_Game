using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_Attack : BTNode
{
    private string targetKey;
    private GameObject target;

    private float acceptableDistance;

    private BehaviorTree treeRoot;

    private float timeBwAttack;
    private float timer = 0f;
    private EnemyAnimation enemyAnimation;

    public BTTask_Attack(BehaviorTree treeRoot , string targetKey = "Player" , float acceptableDistance = 1f , float timeBwAttack = 2f)
    {
        this.treeRoot = treeRoot;
        this.targetKey = targetKey;
        this.acceptableDistance = acceptableDistance;
        this.timeBwAttack = timeBwAttack;
        enemyAnimation = treeRoot.GetComponent<EnemyAnimation>();
    }

    protected override NodeResult Execute()
    {
        if (!treeRoot.isAttack)
        {
            return NodeResult.Failure;
        }

        BlackBoard blackBoard = treeRoot.Blackboard;
        if(blackBoard == null || !blackBoard.GetBlackBoardData(targetKey , out target) )
        {
            treeRoot.isAttack = false;
            return NodeResult.Failure;
        }

        blackBoard.onBlackBoardValueChange += GetData;

        if(target == null)
        {
            treeRoot.isAttack = false;
            return NodeResult.Failure;
        }

        if (!InRangeToAttack())
        {
            treeRoot.isAttack = false;
            return NodeResult.Failure;
        }
        
        treeRoot.isAttack =true;

        Vector3 dir = target.transform.position - treeRoot.transform.position;

        treeRoot.transform.rotation = Quaternion.RotateTowards(treeRoot.transform.rotation
            , Quaternion.LookRotation(dir), 100f * Time.deltaTime);

        Attack();
        return NodeResult.Success;
    }
    public void GetData(string key , object value)
    {
        if(targetKey == key)
        {
            target = (GameObject)value;
        }
    }

    protected override NodeResult Update()
    {
        if (!treeRoot.isAttack)
        {
            return NodeResult.Failure;
        }

        if (target == null)
        {
            treeRoot.isAttack = false;
            return NodeResult.Failure;
        }

        if (!InRangeToAttack())
        {
            treeRoot.isAttack =false;
            return NodeResult.Failure;
        }

        treeRoot.isAttack = true;

        Vector3 dir = target.transform.position - treeRoot.transform.position;

        treeRoot.transform.rotation = Quaternion.RotateTowards(treeRoot.transform.rotation
            , Quaternion.LookRotation(dir), 100f * Time.deltaTime);

        Attack();
        return NodeResult.Success;
    }

    public bool InRangeToAttack()
    {
        return Vector3.Distance(treeRoot.transform.position , target.transform.position) <= acceptableDistance;
    }

    public void Attack()
    {
        timer += Time.deltaTime;

        if(timer >= timeBwAttack)
        {
            Debug.Log("Attack");

            enemyAnimation.IdleState();
            enemyAnimation.AttackState();
            timer = 0;
        } 
    }
}
