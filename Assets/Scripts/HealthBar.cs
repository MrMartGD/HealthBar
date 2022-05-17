using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _delta = 0.01f;
    [SerializeField] private float _delay = 0.01f;

    private Coroutine _increase;
    private Coroutine _decrease;

    public void SetMaxHealth(float health) 
    {
        _slider.maxValue = health;
        _slider.value = health;
    }
    
    public void SetHealth(float currentHealth) 
    {
        if (currentHealth > _slider.value) 
        {
            Increase(currentHealth);
        }

        if (currentHealth < _slider.value) 
        {
            Decrease(currentHealth);
        }
    }

    private void Increase(float targetValue) 
    {
        TryStopCoroutine(ref _increase);
        TryStopCoroutine(ref _decrease);

        _increase = StartCoroutine(ChangeValue(targetValue));
    }

    private void Decrease(float targetValue) 
    {
        TryStopCoroutine(ref _increase);
        TryStopCoroutine(ref _decrease);

        _decrease = StartCoroutine(ChangeValue(targetValue));
    }

    private IEnumerator ChangeValue(float targetValue) 
    {
        var waitForSeconds = new WaitForSeconds(_delay);

        while (_slider.value != targetValue) 
        {
            _slider.value = Mathf.MoveTowards(_slider.value, targetValue, _delta);
            yield return waitForSeconds;
        }
    }

    private void TryStopCoroutine(ref Coroutine coroutine) 
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        else 
        {
            return;
        }
    }
}
