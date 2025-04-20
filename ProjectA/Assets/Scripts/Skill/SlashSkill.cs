using System.Collections;
using UnityEngine;

public class SlashSkill : SkillBase
{
    protected override bool SkillCondition()
    {
        return true;
    }

    protected override IEnumerator PlaySkillRoutine()
    {
        CreateEffect(.5f);

        yield return waitDelay;

        while(true)
        {
            DealDamageToTargets();

            if (isTick)
                yield return waitTickDelay;
            else
                break;
        }
    }
}
