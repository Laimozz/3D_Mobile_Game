using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private HeathSkillSO heathSkillSO;
    [SerializeField] private RocketSkillSO rocketSkillSO;
    [SerializeField] private FireSkillSO fireSkillSO;
    [SerializeField] private TimeSkillSO timeSkillSO;

    [SerializeField] private PlayerCoinSO playerCoinSO;
    [SerializeField] private TextMeshProUGUI coinText;

    [SerializeField] private AudioClip buttonSound;

    private void Start()
    {
        UpdateCoinText();
    }
    public void BuyHeath()
    {
        if(playerCoinSO.coin >= heathSkillSO.price)
        {
            playerCoinSO.coin -= heathSkillSO.price;
            heathSkillSO.quantity++;
            UpdateCoinText();
        }
        AudioManager.Instance.PlayClipOneShot(buttonSound);
    }

    public void BuyRocket()
    {
        if(playerCoinSO.coin >= rocketSkillSO.price)
        {
            playerCoinSO.coin -= rocketSkillSO.price;
            rocketSkillSO.quantity++;
            UpdateCoinText();
        }
        AudioManager.Instance.PlayClipOneShot(buttonSound);
    }

    public void BuyFire()
    {
        if(playerCoinSO.coin >= fireSkillSO.price)
        {
            playerCoinSO.coin -= fireSkillSO.price;
            fireSkillSO.quantity++;
            UpdateCoinText();
        }
        AudioManager.Instance.PlayClipOneShot(buttonSound);
    }

    public void BuyTime() 
    {
        if(playerCoinSO.coin >= timeSkillSO.price)
        {
            playerCoinSO.coin -= timeSkillSO.price;
            timeSkillSO.quantity++;
            UpdateCoinText();
        }
        AudioManager.Instance.PlayClipOneShot(buttonSound);
    }

    public void UpdateCoinText()
    {
        if(coinText != null)
        {
            coinText.text = playerCoinSO.coin.ToString();
        }
    }
}
