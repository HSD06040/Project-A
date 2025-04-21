using UnityEditor.Playables;
using UnityEngine;

public class PlayerStatController : CharacterStatController
{
    protected override void Start()
    {
        stat = GameManager.Data.playerStat;
        ClearStat();
        base.Start();
    }

    private void ClearStat()
    {
        stat.maxHealth.baseValue    = 100;
        stat.damage.baseValue       = 5;
        stat.defense.baseValue      = 1;

        stat.critDamage.baseValue   = 0;
        stat.critChance.baseValue   = 0;

        stat.strength.baseValue     = 0;
        stat.agility.baseValue      = 0;
        stat.vitality.baseValue     = 0;
        stat.luck.baseValue         = 0;
    }
}
