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
    private Coroutine easeRoutine;

    public void UpdateHealthUI(int maxHP,int currentHealth)
    {
        slider.maxValue     = maxHP;
        slider.value        = currentHealth;

        easeSlider.maxValue = maxHP;

        if(easeRoutine != null)
        {
            StopCoroutine(easeRoutine);
            easeRoutine = null;
        }

        easeRoutine = StartCoroutine(EaseSliderRoutine(currentHealth));
    }

    IEnumerator EaseSliderRoutine(int currentHealth)
    {
        while(easeSlider.value != currentHealth)
        {
            easeSlider.value = Mathf.Lerp(easeSlider.value, slider.value, lerpSpeed);
            yield return null;
        }

        yield break;
    }
}
