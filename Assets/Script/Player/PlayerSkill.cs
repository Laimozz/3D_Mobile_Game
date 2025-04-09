using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    [Header("Heath Skill")]
    [SerializeField] private HeathSkillSO heathSkillSO;
    [SerializeField] private Image loadHeath;
    [SerializeField] private float timeHeathSkill;
    [SerializeField] private TextMeshProUGUI textHeath;
    [SerializeField] private AudioClip heathSound;

    [Header("Rocket Skill")]
    [SerializeField] private RocketSkillSO rocketSkillSO;
    [SerializeField] private Image loadRocket;
    [SerializeField] private float timeRocketSkill;
    [SerializeField] private TextMeshProUGUI textRocket;
    [SerializeField] public Transform rocketPoint;
    public bool isExplode;
    public Vector3 collisionPoint;
    [SerializeField] private float radiusRocket;
    [SerializeField] private LayerMask enemyMask;

    [SerializeField] private AudioClip rocketSkillSound;


    [Header("Time Skill")]
    [SerializeField] private TimeSkillSO timeSkillSO;
    [SerializeField] private Image loadTime;
    [SerializeField] private float timeStopSkill;
    [SerializeField] private TextMeshProUGUI textTime;

    [SerializeField] private Image loadEffect;
    [SerializeField] private float timeLoadEffect;

    [SerializeField] private AudioClip timeSkillSound;

    [Header("Fire Skill")]
    [SerializeField] private FireSkillSO fireSkillSO;
    [SerializeField] private Image loadFire;
    [SerializeField] private float timeFireSkill;
    [SerializeField] private TextMeshProUGUI textFire;

    [SerializeField] private float radiusFire;

    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip fireEffectSound;


    private void Start()
    {
        UIManager.Instance.UpdateHeathQuantity(heathSkillSO);
        UIManager.Instance.UpdateRocketQuantity(rocketSkillSO);
        UIManager.Instance.UpdateFireQuantity(fireSkillSO);
        UIManager.Instance.UpdateTimeQuantity(timeSkillSO);
    }

    private void Update()
    {
        if (PlayerCtrl.Instance.PlayerHeath.CurrentHeath <= 0) return;
       
        TimerSkill();
        ExplodeRocket();
    }

    //Heath Skill
    public void UseSkillHeath()
    {
        if (PlayerCtrl.Instance.PlayerHeath.CurrentHeath <= 0) return;

        if (heathSkillSO.quantity <= 0) return;

        if (timeHeathSkill > 0) return;

        heathSkillSO.quantity -= 1;
        UIManager.Instance.UpdateHeathQuantity(heathSkillSO);
        
        PlayerCtrl.Instance.PlayerHeath.CurrentHeath += heathSkillSO.heathAmount;
        if(PlayerCtrl.Instance.PlayerHeath.CurrentHeath > PlayerCtrl.Instance.PlayerHeath.MaxHeath)
        {
            PlayerCtrl.Instance.PlayerHeath.CurrentHeath = PlayerCtrl.Instance.PlayerHeath.MaxHeath;
        }
        timeHeathSkill = heathSkillSO.timeLoad;

        AudioManager.Instance.PlayClipOneShot(heathSound);
    }

    //Rocket Skill
    public void UseSkillRocket()
    {
        if (PlayerCtrl.Instance.PlayerHeath.CurrentHeath <= 0) return;

        if(rocketSkillSO.quantity <= 0) return;

        if(timeRocketSkill > 0) return ;

        rocketSkillSO.quantity -= 1;
        UIManager.Instance.UpdateRocketQuantity(rocketSkillSO);


        GameObject rocket = Instantiate(rocketSkillSO.rocketPrefap , rocketPoint);
        rocket.transform.parent = null;

        timeRocketSkill = rocketSkillSO.timeLoad; 

        AudioManager.Instance.PlayClipOneShot(rocketSkillSound);
    }

    public void ExplodeRocket()
    {
        if (isExplode)
        {
            isExplode = false;
            Collider[] hit = Physics.OverlapSphere(collisionPoint, radiusRocket, enemyMask);
            if (hit.Length > 0)
            {
                foreach (Collider enemy in hit)
                {
                    enemy.GetComponent<EnemyHeath>().TakeDame(rocketSkillSO.dame);
                }
            }
        }
    }

    //Time Skill
    public void UseSkillTime()
    {
        if(PlayerCtrl.Instance.PlayerHeath.CurrentHeath <=0) return;

        if(timeSkillSO.quantity <= 0) return;

        if(timeStopSkill > 0) return;

        timeSkillSO.quantity -= 1;
        UIManager.Instance.UpdateTimeQuantity(timeSkillSO);

        PlayerCtrl.Instance.isStopTime = true;
        timeStopSkill = timeSkillSO.timeLoad;

        timeLoadEffect = timeSkillSO.timeEffect;

        Camera.main.GetComponent<PostProcessLayer>().enabled = true;
        Camera.main.GetComponent<PostProcessVolume>().enabled = true;

        StartCoroutine(EffectSkillTime());

        AudioManager.Instance.PlayClipOneShot(timeSkillSound);
    }

    IEnumerator EffectSkillTime()
    {
        yield return new WaitForSeconds(timeSkillSO.timeEffect);
        PlayerCtrl.Instance.isStopTime = false;
    }

    //Fire Skill
    public void UseSkillFire()
    {
        if(PlayerCtrl.Instance.PlayerHeath.CurrentHeath <= 0) return;

        if(fireSkillSO.quantity <= 0) return;

        if(timeFireSkill > 0) return;

        fireSkillSO.quantity -= 1;
        UIManager.Instance.UpdateFireQuantity(fireSkillSO);

        Instantiate(fireSkillSO.firePrefap , transform.position + new Vector3(0 , 0.5f ,0), Quaternion.Euler(90 , 0 , 0));
        timeFireSkill = fireSkillSO.timeLoad;

        AudioManager.Instance.PlayClipOneShot(fireSound);
    }

    public void EffectSkillFire()
    {
        if (PlayerCtrl.Instance.PlayerHeath.CurrentHeath <= 0) return;

        Collider[] hit = Physics.OverlapSphere(transform.position, radiusFire, enemyMask);
        if(hit.Length > 0)
        {
            foreach(Collider enemy in hit)
            {
                enemy.GetComponent<EnemyHeath>().TakeDame(fireSkillSO.dame);
                Instantiate(fireSkillSO.effectPrefap, enemy.transform);
                enemy.GetComponent<EnemyHeath>().timeIgnite = fireSkillSO.timeEffect;
                enemy.GetComponent<AudioSource>().PlayOneShot(fireEffectSound);
            }
        }
    }

    public void TimerSkill()
    {

        //Skill Heath
        loadHeath.fillAmount = Mathf.Lerp(loadHeath.fillAmount, 
            timeHeathSkill / heathSkillSO.timeLoad, 10f * Time.deltaTime);

        textHeath.text = $"{Mathf.Ceil(timeHeathSkill)} s";

        if(timeHeathSkill > 0)
        {
            timeHeathSkill -= Time.deltaTime;
            loadHeath.gameObject.SetActive(true);
            textHeath.gameObject.SetActive(true);
        }
        else
        {
            timeHeathSkill = 0f;
            loadHeath.gameObject.SetActive(false);
            textHeath.gameObject.SetActive(false);
        }

        //Skill Rocket
        loadRocket.fillAmount = Mathf.Lerp(loadRocket.fillAmount,
            timeRocketSkill / rocketSkillSO.timeLoad, 10f * Time.deltaTime);

        textRocket.text = $"{Mathf.Ceil(timeRocketSkill)} s";
        if(timeRocketSkill > 0)
        {
            timeRocketSkill -= Time.deltaTime;
            loadRocket.gameObject.SetActive(true);
            textRocket.gameObject.SetActive(true);
        }
        else
        {
            timeRocketSkill = 0f;
            loadRocket.gameObject.SetActive(false);
            textRocket.gameObject.SetActive(false);
        }

        //Time Skill
        loadTime.fillAmount = Mathf.Lerp(loadTime.fillAmount, 
            timeStopSkill / timeSkillSO.timeLoad, 10f * Time.deltaTime);
        textTime.text = $"{Mathf.Ceil(timeStopSkill)}s";

        loadEffect.fillAmount = Mathf.Lerp(loadEffect.fillAmount ,
            timeLoadEffect / timeSkillSO.timeEffect , 10f *Time.deltaTime);

        if(timeStopSkill > 0)
        {
            timeStopSkill -= Time.deltaTime;
            loadTime.gameObject.SetActive(true);
            textTime.gameObject.SetActive(true);
        }
        else
        {
            timeStopSkill = 0f;
            loadTime.gameObject.SetActive(false);
            textTime.gameObject.SetActive(false);
        }

        if(timeLoadEffect > 0)
        {
            timeLoadEffect -= Time.deltaTime;
            loadEffect.gameObject.SetActive(true);

        }
        else
        {
            timeLoadEffect = 0f;
            loadEffect.gameObject.SetActive(false);

            Camera.main.GetComponent<PostProcessLayer>().enabled = false;
            Camera.main.GetComponent<PostProcessVolume>().enabled = false;
        }

        //Fire Skill
        loadFire.fillAmount = Mathf.Lerp(loadFire.fillAmount, 
            timeFireSkill / fireSkillSO.timeLoad, 10f * Time.deltaTime);
        textFire.text = $"{Mathf.Ceil(timeFireSkill)}s";

        if (timeFireSkill > 0)
        {
            timeFireSkill -= Time.deltaTime;
            loadFire.gameObject.SetActive(true);
            textFire.gameObject.SetActive(true);
        }
        else
        {
            timeFireSkill = 0f;
            loadFire.gameObject.SetActive(false);
            textFire.gameObject.SetActive(false);
        }
    }
}
