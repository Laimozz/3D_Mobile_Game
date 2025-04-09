using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyPerception : MonoBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    [SerializeField] List<PlayerCtrl> enemyPerceptions = new List<PlayerCtrl>();
    [SerializeField] BehaviorTree tree;

    private bool setCoroutine = true;
    Coroutine myCorou;

    private bool isPlayerDead;
    private void Awake()
    {
        playerCtrl = FindObjectOfType<PlayerCtrl>();
        tree = GetComponent<BehaviorTree>();
    }

    private void Update()
    {
        if(PlayerCtrl.Instance.isStopTime || transform.GetComponent<EnemyHeath>().EnemyCurrentHeath <= 0) return;

        if(PlayerCtrl.Instance.PlayerHeath.CurrentHeath <= 0)
        {
            if(!isPlayerDead)
            {
                isPlayerDead = true;
                enemyPerceptions.Remove(playerCtrl);
                tree.Blackboard.RemoveData("Player");
                tree.isChase = false;
                tree.isWait = true;
                setCoroutine = true;
            }
            return;
        }

        SetEnemyPerception();
    }

    public abstract bool AwarePlayer();

    public virtual void SetEnemyPerception()
    {
        if (AwarePlayer())
        {
            tree.Blackboard.SetOrAddData("Player", playerCtrl.gameObject);
            tree.isChase = true;
         
            if (!enemyPerceptions.Contains(playerCtrl))
            {
                enemyPerceptions.Add(playerCtrl);
            }
            else
            {
                if (!setCoroutine)
                {
                    StopCoroutine(myCorou);
                    setCoroutine = true;
                    tree.isWait = false;
                }              
            }
        }
        else
        {
            if (enemyPerceptions.Contains(playerCtrl))
            {
                if (setCoroutine)
                {                
                   myCorou = StartCoroutine(RemoveAware(playerCtrl));                    
                }
                             
            }
        }
    }

    IEnumerator RemoveAware(PlayerCtrl player)
    {
        setCoroutine = false;
        yield return new WaitForSeconds(8f);

        enemyPerceptions.Remove(playerCtrl);
        setCoroutine = true;

        tree.Blackboard.RemoveData("Player");
        tree.isChase = false;
        tree.isWait = true;
    }

    public virtual void DrawGizmos()
    {
        if (enemyPerceptions.Contains(playerCtrl))
        {
            Gizmos.DrawWireSphere(playerCtrl.transform.position + Vector3.up * 0.3f, 0.5f);
        }
    }

    private void OnDrawGizmos()
    {
        DrawGizmos();
    }
}
