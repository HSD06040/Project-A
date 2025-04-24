using UnityEngine;

public class InGamePanel : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    private PlayerStats stat;

    private void Start()
    {
        stat = GameManager.Data.playerStat;
        stat.OnHealthChanged += (maxHP, currentHP) => healthBar.UpdateHealthUI(maxHP, currentHP);
    }

    private void OnEnable()
    {
        if(stat != null)
            stat.OnHealthChanged += (maxHP,currentHP) => healthBar.UpdateHealthUI(maxHP,currentHP);
    }

    private void OnDisable()
    {
        stat.OnHealthChanged -= (maxHP, currentHP) => healthBar.UpdateHealthUI(maxHP, currentHP);
    }
}
