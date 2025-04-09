using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject player;

    [Header("Player Heath")]
    [SerializeField] private PlayerHeath playerHeath;
    [SerializeField] private Image heathImage;

    [Header("Skill UI")]
    [SerializeField] private TextMeshProUGUI heathSkillQuantity;
    [SerializeField] private TextMeshProUGUI rocketSkillQuantity;
    [SerializeField] private TextMeshProUGUI fireSkillQuantity;
    [SerializeField] private TextMeshProUGUI timeSkillQuantity;

    [Header("Coin UI")]
    [SerializeField] private PlayerCoinSO playerCoinSO;
    [SerializeField] private TextMeshProUGUI coinText;

    [Header("Bullet")]
    [SerializeField] private TextMeshProUGUI bulletText;

    [Header("EndUI")]
    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject victory;
    [SerializeField] private GameObject defeat;

    [Header("EnemyAmount")]
    [SerializeField] private TextMeshProUGUI enemyAmountText;

    private bool isVictory;

    public override void Awake()
    {
        base.Awake();
        player = GameObject.Find("Player");
        playerHeath = player.transform.GetComponent<PlayerHeath>();
        UpdateCoin();
    }

    void Update()
    {
        //Heath Image
        heathImage.fillAmount = Mathf.Lerp(heathImage.fillAmount, 
            playerHeath.CurrentHeath / playerHeath.MaxHeath, 10f * Time.deltaTime);

        enemyAmountText.text = $"Enemy : {PlayerCtrl.Instance.enemyAmount}";

        if(PlayerCtrl.Instance.enemyAmount <= 0)
        {
            if (!isVictory)
            {
                isVictory = true;
                StartCoroutine(VictoryCoroutine());
            }
        }
    }

    IEnumerator VictoryCoroutine()
    {
        yield return new WaitForSeconds(5f);
        Victory();
    }
    public void UpdateHeathQuantity(HeathSkillSO heathSkillSO)
    {
        heathSkillQuantity.text = heathSkillSO.quantity.ToString();
    }
    public void UpdateRocketQuantity(RocketSkillSO rocketSkillSO)
    {
        rocketSkillQuantity.text = rocketSkillSO.quantity.ToString();
    }
    public void UpdateFireQuantity(FireSkillSO fireSkillSO)
    {
        fireSkillQuantity.text = fireSkillSO.quantity.ToString();
    }
    public void UpdateTimeQuantity(TimeSkillSO timeSkillSO)
    {
        timeSkillQuantity.text = timeSkillSO.quantity.ToString();
    }

    public void UpdateCoin()
    {
        coinText.text = playerCoinSO.coin.ToString();
    }

    public void UpdateBullet(int bulletAmt)
    {
        bulletText.text = bulletAmt.ToString();
    }

    public void Victory()
    {
        endPanel.SetActive(true);

        victory.SetActive(true);
        defeat.SetActive(false);
    }

    public void Defeat()
    {
        endPanel.SetActive(true);

        victory.SetActive(false);
        defeat.SetActive(true);
    }
}
