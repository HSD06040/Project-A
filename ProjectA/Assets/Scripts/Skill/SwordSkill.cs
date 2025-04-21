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
        CreateEffect();

        yield return waitDelay;

        while (true)
        {
            DealDamageToTargets();

            if (isTick)
                yield return waitTickDelay;
            else
                break;
        }

        yield return null;
    }
}