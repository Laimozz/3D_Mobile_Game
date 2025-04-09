using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private GameObject attackColider;
    [SerializeField] private Animator animator;

    private readonly int runState = Animator.StringToHash("Run");
    private readonly int attackState = Animator.StringToHash("Attack");
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(PlayerCtrl.Instance.isStopTime || transform.GetComponent<EnemyHeath>().EnemyCurrentHeath <= 0)
        {
            animator.enabled = false;
            return;
        }
        if(animator.enabled == false)
        {
            animator.enabled = true;
        }
    }

    public void IdleState()
    {
        animator.SetBool(runState, false);
    }

    public void RunState()
    {
        animator.SetBool(runState, true);
    }

    public void AttackState()
    {
        animator.SetTrigger(attackState);
    }

    public void SetAttackColider()
    {
        attackColider.SetActive(!attackColider.activeSelf);
    }
}
