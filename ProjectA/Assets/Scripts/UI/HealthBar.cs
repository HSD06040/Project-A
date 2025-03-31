using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Slider easeSlider;
    [SerializeField] private float lerpSpeed = 2;
    [SerializeField] private CharacterStats stat;

    private void Start()
    {

    }

    private void OnEnable()
    {
        stat.OnHealthChanged += UpdateHealthUI;
    }

    private void OnDisable()
    {
        stat.OnHealthChanged -= UpdateHealthUI;
    }

    private void Update()
    {
        if (slider.value != easeSlider.value)
            easeSlider.value = Mathf.Lerp(easeSlider.value, stat.CurrentHealth, lerpSpeed);
    }

    private void UpdateHealthUI(int currentHealth)
    {
        slider.maxValue = CombatStatCalculator.Instacne.GetMaxHealth(stat);
        slider.value = currentHealth;

        easeSlider.maxValue = CombatStatCalculator.Instacne.GetMaxHealth(stat);
    }

}
