using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHeath : MonoBehaviour
{
    [SerializeField] private float enemyHeath;
    public float EnemyCurrentHeath => enemyHeath;
    
    [SerializeField] private float maxEnemyHeath;
    [SerializeField] private GameObject enemyHeathBarPrefaps;
    [SerializeField] private GameObject heathBar;
    [SerializeField] private Transform attachPoint;
    [SerializeField] private Canvas canvas;

    [SerializeField] public bool couterAttack;
    [SerializeField] private Animator animator;

    [SerializeField] private Transform effectPoint;
    [SerializeField] private GameObject[] deathEffect;
    [SerializeField] private float timeToDestroy;

    public float timeIgnite;
    [SerializeField] private float timePerSecondIgnite;
    [SerializeField] private float dameIgnite;

    [Header("Coin")]
    [SerializeField] private float enemyCoin;
    [SerializeField] private PlayerCoinSO playerCoinSO;

    [Header("Sound")]
    [SerializeField] private AudioClip deathSound;
    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyHeath = maxEnemyHeath;
        canvas = FindObjectOfType<Canvas>();
        heathBar = Instantiate(enemyHeathBarPrefaps, canvas.transform);
    }

    private void Update()
    {
        Vector3 attach = Camera.main.WorldToScreenPoint(attachPoint.position);
        heathBar.transform.position = attach;

        heathBar.GetComponentInChildren<Slider>().value = enemyHeath / maxEnemyHeath;

        Ignite();
    }

    public void TakeDame(float value)
    {
        if(enemyHeath > 0)
        {
            couterAttack = true;
            enemyHeath -= value;
            if (enemyHeath <= 0)
            {
                playerCoinSO.coin += enemyCoin;
                UIManager.Instance.UpdateCoin();

                animator.SetTrigger("Dead");
                PlayerCtrl.Instance.enemyAmount--;

                StartCoroutine(Dead());
            }
        }    
    }

    public void Ignite()
    {
        if(enemyHeath <= 0) return;

        if (timeIgnite <= 0) return;

        if(timePerSecondIgnite <= 0)
        {
            enemyHeath -= dameIgnite;
            if(enemyHeath <= 0)
            {
                animator.SetTrigger("Dead");
                StartCoroutine(Dead());
            }
            timePerSecondIgnite = 2f;
        }
        timePerSecondIgnite -= Time.deltaTime;
        timeIgnite -= Time.deltaTime;
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(timeToDestroy);

        AudioManager.Instance.PlayClipOneShot(deathSound);
        for (int i = 0; i < deathEffect.Length; i++)
        {
            GameObject effect = Instantiate(deathEffect[i], effectPoint);
            effect.transform.SetParent(transform.parent);
        }

        //yield return new WaitForSeconds(0.3f);
        Destroy(heathBar);
        Destroy(gameObject);
    }
}
