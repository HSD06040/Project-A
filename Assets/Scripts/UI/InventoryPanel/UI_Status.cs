using TMPro;
using UnityEngine;

public class UI_Status : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private TextMeshProUGUI critChance;
    [SerializeField] private TextMeshProUGUI critDamage;
    [SerializeField] private TextMeshProUGUI maxHealth;
    [SerializeField] private TextMeshProUGUI defense;
    [SerializeField] private TextMeshProUGUI strength;
    [SerializeField] private TextMeshProUGUI agility;
    [SerializeField] private TextMeshProUGUI vitality;
    [SerializeField] private TextMeshProUGUI luck;

    public void UpdateStatusUI()
    {
        PlayerStats stat    = GameManager.Data.playerStat;

        damage.text         = CombatStatCalculator.GetDamage    (stat).ToString();
        critChance.text     = CombatStatCalculator.GetCritChance(stat).ToString();
        critDamage.text     = CombatStatCalculator.GetCritDamage(stat).ToString();
        maxHealth.text      = CombatStatCalculator.GetMaxHealth (stat.maxHealth.GetValue(), stat.vitality.GetValue(), stat.strength.GetValue()).ToString();
        defense.text        = CombatStatCalculator.GetDefense   (stat).ToString();

        strength.text       = GameManager.Data.playerStat.strength.GetValue().ToString();
        agility.text        = GameManager.Data.playerStat.agility.GetValue().ToString();
        vitality.text       = GameManager.Data.playerStat.vitality.GetValue().ToString();
        luck.text           = GameManager.Data.playerStat.luck.GetValue().ToString();
    }
}
