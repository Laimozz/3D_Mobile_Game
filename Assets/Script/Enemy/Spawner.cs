using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefap;
    [SerializeField] private float timeToSpawn;
    [SerializeField] private float timer;
    [SerializeField] private Animator animator;

    [Header("Audio")]
    [SerializeField] AudioClip spawnAudio;

    private void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        if (PlayerCtrl.Instance.isStopTime || transform.GetComponent<EnemyHeath>().EnemyCurrentHeath <= 0)
        {
            animator.enabled = false;
            return;
        }
        if(animator.enabled == false)
        {
            animator.enabled = true;
        }

        if(timer <= 0)
        {
            timer = timeToSpawn;
            animator.SetTrigger("Spawn");
            GameObject enemy = Instantiate(enemyPrefap , transform);
            enemy.transform.SetParent(transform.parent);

            AudioManager.Instance.PlayClipOneShot(spawnAudio);

            PlayerCtrl.Instance.enemyAmount++;
        }
        timer -= Time.deltaTime;
    }
}
