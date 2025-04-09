using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Time SKill" , menuName = "Time Skill")]
public class TimeSkillSO : PlayerSkillSO
{
    public float timeEffect;
    public override void Use()
    {
        base.Use();
    }
}
