using UnityEngine;

public class Enemy_HealthBar : HealthBar
{
    private EnemyStats stat;
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
        stat = GetComponentInParent<EnemyStats>();
    }
    private void Start()
    {
        stat.OnHealthChanged += (maxHP, currentHP) => UpdateHealthUI(maxHP, currentHP);
    }

    private void OnDisable()
    {
        stat.OnDead -= () => Invoke(nameof(HealthBarDisable), 1f);
        stat.OnHealthChanged += (maxHP, currentHP) => UpdateHealthUI(maxHP, currentHP);
    }

    private void OnEnable()
    {
        stat.OnDead += () => Invoke(nameof(HealthBarDisable), 1f);
        stat.OnHealthChanged -= (maxHP, currentHP) => UpdateHealthUI(maxHP, currentHP);
    }

    private void HealthBarDisable() => this.gameObject.SetActive(false);


    private void Update()
    {
        Billboard();
    }

    private void Billboard()
    {
        if (mainCam != null)
        {
            Vector3 dir = (transform.position - mainCam.transform.position).normalized;
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
