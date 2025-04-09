using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeath : MonoBehaviour
{
    //[SerializeField] private float currentHeath;
    public float CurrentHeath;

    [SerializeField] private float maxHeath;
    public float MaxHeath => maxHeath;

    [SerializeField] private ShakeCamera shakeCamera;
    [SerializeField] private DameVisualizer dameVisualizer;

    [SerializeField] private Animator animator;

    [Header("Audio")]
    [SerializeField] private AudioClip hitDamageSound;
    [SerializeField] private AudioClip playerDeadSound;
    private void Start()
    {
        CurrentHeath = maxHeath;
        dameVisualizer = GetComponent<DameVisualizer>();
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(float dame)
    {
        if(CurrentHeath <= 0 ) return;
 
        if(shakeCamera != null)
        {
            shakeCamera.StartShake();
        }
        CurrentHeath = Mathf.Max(0f , CurrentHeath - dame);
        dameVisualizer.BlinkColor();
        AudioManager.Instance.PlayClipOneShot(hitDamageSound);

        if(CurrentHeath <= 0)
        {
            animator.SetTrigger("Dead");
            AudioManager.Instance.PlayClipOneShot(playerDeadSound);

            StartCoroutine(CallDefeatUI());
        }
    }

    IEnumerator CallDefeatUI()
    {
        yield return new WaitForSeconds(5f);
        UIManager.Instance.Defeat();
    }
}
