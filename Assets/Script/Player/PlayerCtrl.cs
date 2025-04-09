using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : Singleton<PlayerCtrl>
{
    [SerializeField] private PlayerHeath playerHeath;
    public PlayerHeath PlayerHeath => playerHeath;

    [SerializeField] private PlayerSkill playerSkill;

    public PlayerSkill PlayerSkill => playerSkill;

    public bool isStopTime;

    public int enemyAmount;

    private void Start()
    {
        playerHeath = GetComponent<PlayerHeath>();
        playerSkill = GetComponent<PlayerSkill>();
    }

}
