using System.Collections;
using UnityEngine;

public class SlashSkill : SkillBase
{
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

    protected override bool SkillCondition()
    {
        return true;
    }
}
