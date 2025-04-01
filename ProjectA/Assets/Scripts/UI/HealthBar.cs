using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Slider easeSlider;
    [SerializeField] private float lerpSpeed = 0.05f;
    [SerializeField] private CharacterStats stat;

    protected virtual void OnEnable()
    {
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
        slider.maxValue = GameManager.Calculator.GetMaxHealth(stat);
        slider.value = currentHealth;

        easeSlider.maxValue = GameManager.Calculator.GetMaxHealth(stat);
    }

}
