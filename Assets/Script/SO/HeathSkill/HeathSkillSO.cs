using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HeathSkill")]
public class HeathSkillSO : PlayerSkillSO
{
    public float heathAmount;
    public override void Use()
    {
        base.Use();
    }
}
