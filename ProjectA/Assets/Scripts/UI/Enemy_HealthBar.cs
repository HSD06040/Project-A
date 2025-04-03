using UnityEngine;

public class Enemy_HealthBar : HealthBar
{
    private Camera mainCam;
    private void Start()
    {
        mainCam = Camera.main;
    }
    protected override void OnDisable()
    {
        base.OnDisable();

        stat.OnPlayerDead -= () => Invoke(nameof(HealthBarDisable), 1f);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        stat.OnPlayerDead += () => Invoke(nameof(HealthBarDisable), 1f);
    }

    private void HealthBarDisable() => this.gameObject.SetActive(false);


    protected override void Update()
    {
        base.Update();

        if (mainCam != null)
        {
            Vector3 dir = (transform.position - mainCam.transform.position).normalized;
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    protected override void UpdateHealthUI(int currentHealth)
    {
        base.UpdateHealthUI(currentHealth);
    }
}
