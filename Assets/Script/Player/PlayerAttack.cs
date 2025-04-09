using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private JoyStickCtrl aimJoy;
    [SerializeField] private Vector2 aimInput;
    [SerializeField] private Animator animator;

    [Header("Ray Cast")]
    [SerializeField] private Transform muzzle;
    [SerializeField] private float aimRange;
    public LayerMask layerMask;

    [Header("SFX")]
    [SerializeField] private ParticleSystem bullet;

    [SerializeField] private float attackDame;

    [SerializeField] private float timer;
    [SerializeField] private float timerReset;

    [SerializeField] private AudioClip gunSound;

    [Header("Bullet")]
    private int bulletAmtCurrent;
    [SerializeField] private int bulletAmtMax;
    private bool isReload;

    public void Start()
    {
        aimJoy.inputData += GetAimInput; 
        animator = GetComponent<Animator>();

        bulletAmtCurrent = bulletAmtMax;
    }

    private void Update()
    {
        if (PlayerCtrl.Instance.PlayerHeath.CurrentHeath <= 0) return;

        if(bulletAmtCurrent <= 0)
        {
            if(!isReload)
            {
                isReload = true;
                animator.SetTrigger("Reload");
            }      
            return;
        }

        timer -= Time.deltaTime;
        if(aimInput.magnitude != 0 && timer <= 0)
        {
            GetTrajectory();
            bullet.Emit(bullet.emission.GetBurst(0).maxCount);

            animator.SetBool("Attack", true);
            timer = timerReset;

            bulletAmtCurrent -= 1;
            UIManager.Instance.UpdateBullet(bulletAmtCurrent);

            AudioManager.Instance.PlayClipOneShot(gunSound);

        }
        else if(aimInput.magnitude == 0)
        {
            animator.SetBool("Attack", false);
        }
    }
    public void GetTrajectory()
    {
        RaycastHit hit;
        if(Physics.Raycast(muzzle.position , muzzle.forward , out hit , aimRange , layerMask))
        {
            if(hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<EnemyHeath>().TakeDame(attackDame);
            }
        }
    }

    public void GetAimInput(Vector2 value)
    {
        aimInput = value;
    }

    public void ReloadBullet()
    {
        isReload = false;
        Debug.Log("duoc khong");
        bulletAmtCurrent = bulletAmtMax;
        UIManager.Instance.UpdateBullet(bulletAmtCurrent);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(muzzle.position, muzzle.position + muzzle.forward * aimRange);
    }
}
