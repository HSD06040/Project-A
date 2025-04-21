using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider easeSlider;
    [SerializeField] private Slider slider;
    [SerializeField] private float lerpSpeed = 0.05f;
    [SerializeField] protected CharacterStats stat;

    private void Start()
    {
        stat = GameManager.Data.playerStat;
        stat.OnHealthChanged += UpdateHealthUI;
        StartCoroutine(DelayUpdateUI(.1f));
    }

    protected virtual void OnEnable()
    {
        if(stat != null)
            stat.OnHealthChanged += UpdateHealthUI;
    }

    protected virtual void OnDisable()
    {
        stat.OnHealthChanged -= UpdateHealthUI;
    }

    protected virtual void Update()
    {
        if (slider.value != easeSlider.value)
            easeSlider.value = Mathf.Lerp(easeSlider.value, stat.CurrentHealth, lerpSpeed);
    }

    protected virtual void UpdateHealthUI(int currentHealth)
    {
        slider.maxValue     = stat.maxHP;
        slider.value        = currentHealth;

        easeSlider.maxValue = stat.maxHP;
    }
    
    IEnumerator DelayUpdateUI(float delay)
    {
        yield return new WaitForSeconds(delay);
        UpdateHealthUI(stat.CurrentHealth);
    }
}
