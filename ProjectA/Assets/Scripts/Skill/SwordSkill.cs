using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SwordSkill : SkillBase
{
    

    protected override bool SkillCondition()
    {
        return true;
    }

    protected override IEnumerator PlaySkillRoutine()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        yield return null;
    }
}