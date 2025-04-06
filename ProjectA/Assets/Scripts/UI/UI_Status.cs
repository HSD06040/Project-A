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
        PlayerStats stat    = GameManager.Data.playerStatData;
        damage.text         = GameManager.Calculator.GetDamage(GameManager.Data.playerStatData).ToString();
        critChance.text     = GameManager.Calculator.GetCritChance(GameManager.Data.playerStatData).ToString();
        critDamage.text     = GameManager.Calculator.GetCritDamage(GameManager.Data.playerStatData).ToString();
        maxHealth.text      = GameManager.Calculator.GetMaxHealth(stat.maxHealth.GetValue(), stat.vitality.GetValue(), stat.strength.GetValue()).ToString();
        defense.text        = GameManager.Calculator.GetDefense(GameManager.Data.playerStatData).ToString();
        strength.text       = GameManager.Data.playerStatData.strength.GetValue().ToString();
        agility.text        = GameManager.Data.playerStatData.agility.GetValue().ToString();
        vitality.text       = GameManager.Data.playerStatData.vitality.GetValue().ToString();
        luck.text           = GameManager.Data.playerStatData.luck.GetValue().ToString();
    }
}
