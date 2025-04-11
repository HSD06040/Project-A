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

        stat.OnDead -= () => Invoke(nameof(HealthBarDisable), 1f);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        stat.OnDead += () => Invoke(nameof(HealthBarDisable), 1f);
    }

    private void HealthBarDisable() => this.gameObject.SetActive(false);


    protected override void Update()
    {
        base.Update();
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

    protected override void UpdateHealthUI(int currentHealth)
    {
        base.UpdateHealthUI(currentHealth);
    }
}
