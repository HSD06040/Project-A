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
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

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
